using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Frends.JSON.EnforceTypes.Definitions;

namespace Frends.JSON.EnforceTypes.Tests
{
    [TestFixture]
    class TestClass
    {
        [Test]
        public void EnforceJsonTypesTest()
        {
            var json = "{\"hello\": \"123\",\"hello_2\": \"123.5\",\"world\": \"true\",\"bad_arr\": \"hello, world\",\"bad_arr_2\": { \"prop1\": 123 },\"good_arr\": [ \"hello, world\" ],\"good_arr_2\": [ { \"prop1\": 123 } ]}";
            var result = JSON.EnforceJsonTypes(
                new EnforceJsonTypesInput
                {
                    Json = json,
                    Rules = new[]
                    {
                        new JsonTypeRule{JsonPath = "$.hello", DataType = JsonDataType.Number },
                        new JsonTypeRule{JsonPath = "$.hello_2", DataType = JsonDataType.Number },
                        new JsonTypeRule{JsonPath = "$.world", DataType = JsonDataType.Boolean },
                        new JsonTypeRule{JsonPath = "$.bad_arr", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.bad_arr_2", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.good_arr", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.good_arr_2", DataType = JsonDataType.Array },
                    }
                }, new CancellationToken());
            var expected = JObject.Parse("{\"hello\": 123,\"hello_2\": 123.5,\"world\": true,\"bad_arr\": [\"hello, world\"],\"bad_arr_2\": [{\"prop1\": 123}],\"good_arr\": [\"hello, world\"],\"good_arr_2\": [{\"prop1\": 123}]}");
            Console.WriteLine(expected);
            Console.WriteLine(result);
            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void ChangeDataTypeTest_Number()
        {
            JValue jValue;

            // Valid number
            jValue = new JValue("1.23");
            JSON.ChangeDataType(jValue, JsonDataType.Number);
            Assert.AreEqual(1.23, jValue.Value);

            // Invalid number - do nothing
            jValue = new JValue("foo");
            JSON.ChangeDataType(jValue, JsonDataType.Number);
            Assert.AreEqual("foo", jValue.Value);

            // Source is number - do nothing
            jValue = new JValue(1.23);
            JSON.ChangeDataType(jValue, JsonDataType.Number);
            Assert.AreEqual(1.23, jValue.Value);
        }

        [Test]
        public void ChangeDataTypeTest_Empty()
        {
            JValue jValue;

            // Empty - null
            jValue = new JValue("");
            JSON.ChangeDataType(jValue, JsonDataType.Number);
            Assert.AreEqual(null, jValue.Value);

            // Empty - null
            jValue = new JValue((string)null);
            JSON.ChangeDataType(jValue, JsonDataType.Number);
            Assert.AreEqual(null, jValue.Value);
        }

        [Test]
        public void ChangeDataTypeTest_Booleans()
        {
            JValue jValue;

            // Valid bool
            jValue = new JValue("true");
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(true, jValue.Value);

            jValue = new JValue("TRUE");
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(true, jValue.Value);

            jValue = new JValue("True");
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(true, jValue.Value);

            jValue = new JValue("FaLsE");
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(false, jValue.Value);
            // Null bool

            jValue = new JValue((bool?)null);
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(null, jValue.Value);

            // Bool source
            jValue = new JValue(true);
            JSON.ChangeDataType(jValue, JsonDataType.Boolean);
            Assert.AreEqual(true, jValue.Value);
        }

        [Test]
        public void ChangeDataTypeTest_Arrays()
        {
            // Array
            var jObject = JObject.Parse(@"{
  ""arr"": 111
}");
            var jValue = (JValue)jObject.SelectTokens("$.arr").First();
            JSON.ChangeDataType(jValue, JsonDataType.Array);
            var jArray = (JArray)jObject.SelectToken("$.arr");
            Assert.AreEqual(1, jArray.Count);
            Assert.AreEqual(111, jArray[0].Value<int>());
        }

        [Test]
        public void ChangeDataTypeTest_ArraysWithComplexObjects()
        {
            // Array
            var jObject = JObject.Parse(@"{
  ""arr"": { ""prop1"": 111 }
}");
            var jToken = jObject.SelectTokens("$.arr").First();
            JSON.ChangeDataType(jToken, JsonDataType.Array);
            var jArray = (JArray)jObject.SelectToken("$.arr");
            Assert.AreEqual(1, jArray.Count);
            Assert.AreEqual(111, jArray[0]["prop1"].Value<int>());
        }


        [Test]
        public void TestArrays()
        {
            var jObject = JObject.Parse(@"{
  ""hello"": ""123"",
  ""world"": ""true"",
  ""arr"": [1,2,3,4]
}");
            var tokens = jObject.SelectTokens("$.arr");
            tokens.Count();
        }

        [Test]
        public void TestEmptyArray()
        {
            var json = @"{
  ""arr1"": null,
  ""arr2"": ""null"",
  ""arr3"": 
    {
      ""prop"": ""null""
    }
}";
            var result = JSON.EnforceJsonTypes(
                new EnforceJsonTypesInput
                {
                    Json = json,
                    Rules = new[]
                    {
                        new JsonTypeRule{JsonPath = "$.arr1", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.arr2", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.arr3", DataType = JsonDataType.Array },
                    }
                }, new CancellationToken());
            var expected = JObject.Parse(@"{
  ""arr1"": null, 
  ""arr2"": ""null"",
  ""arr3"": [
    {
      ""prop"": ""null""
    }
  ]
}");
            Assert.AreEqual(expected.ToString(), result);
        }

        [Test]
        public void TestEmptyArrayOfArrays()
        {
            var json = @"{
  ""arrays"": [
    {
      ""arr1"": null,
    },
    {
      ""arr2"": ""null"",
    },
    {
      ""arr3"": 
        {
          ""prop"": ""null""
        }
    }
  ]
}";
            var result = JSON.EnforceJsonTypes(
                new EnforceJsonTypesInput
                {
                    Json = json,
                    Rules = new[]
                    {
                        new JsonTypeRule{JsonPath = "$.arrays[0].arr1", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.arrays[1].arr2", DataType = JsonDataType.Array },
                        new JsonTypeRule{JsonPath = "$.arrays[2].arr3", DataType = JsonDataType.Array },
                    }
                }, new CancellationToken());
            var expected = JObject.Parse(@"{
  ""arrays"": [
    {
      ""arr1"": null, 
    },
    {
      ""arr2"": ""null"",
    },
    {
      ""arr3"": [
        {
          ""prop"": ""null""
        }
      ]
    }
  ]
}");
            Assert.AreEqual(expected.ToString(), result);
        }
    }
}
