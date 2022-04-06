using NUnit.Framework;
using System;
using Newtonsoft.Json.Linq;
using Frends.JSON.Transform.Definitions;

namespace Frends.JSON.Transform.Tests
{
    [TestFixture]
    class TestClass
    {
        Input _testInput;
        private const string _testJson =
@"
{
    ""firstName"": ""Veijo"",
    ""lastName"": ""Frends"",
    ""age"": 30,
    ""retired"": false
}
";
        private const string _testMap =
@"
{
    ""FullName"": ""#xconcat(#valueof($.firstName), ,#valueof($.lastName))"",
    ""Age"" : ""#valueof($.age)"",
    ""StillBreething"": ""#valueof($.retired)""
}
";
        [SetUp]
        public void TestSetup()
        {
            _testInput = new Input
            {
                InputJson = _testJson,
                JsonMap = _testMap
            };
        }

        [Test]
        public void TransformShouldAllowJTokenAsInput()
        {
            _testInput.InputJson = JToken.Parse(_testJson);
            var result = JSON.Transform(_testInput);
        }

        [Test]
        public void TransformShouldAllowStringAsInput()
        {
            var result = JSON.Transform(_testInput);
        }

        [Test]
        public void TransformMapsStringData()
        {
            var result = JSON.Transform(_testInput);

            var fullName = result.JToken["FullName"].Value<string>();

            Assert.AreEqual("Veijo Frends", fullName);
        }

        [Test]
        public void TransformMapsNumbersCorrectly()
        {
            var result = JSON.Transform(_testInput);

            var age = result.JToken["Age"];

            Assert.AreEqual(JTokenType.Integer, age.Type);
            Assert.AreEqual(30, age.Value<int>());
        }

        [Test]
        public void TransformationMapsBoolValueCorrectly()
        {
            var result = JSON.Transform(_testInput);

            var breething = result.JToken["StillBreething"];

            Assert.AreEqual(JTokenType.Boolean, breething.Type);
            Assert.AreEqual(false, breething.Value<bool>());
        }

        [Test]
        public void TransformWorksWithArray()
        {
            _testInput.InputJson = @"{ ""array"": [{""key"":""first element""},{""key"":""second element""}]}";
            _testInput.JsonMap = @"{""firstElement"":""#valueof($.array[0].key)""}";

            var result = JSON.Transform(_testInput);
            var firstElement = result.JToken["firstElement"].Value<string>();

            Assert.AreEqual("first element", firstElement);
            
        }

        [Test]
        public void TransformThrowsWithArrayRootElement()
        {
            _testInput.InputJson = @"[{""key"": ""first element""}, {""key"": ""second element""}]";
            _testInput.JsonMap = @"{""firstElement"": ""#valueof($.[0].key)""}";

            var ex = Assert.Throws<Exception>(() => JSON.Transform(_testInput));
            Assert.That(ex.Message.Equals("Json transformation failed: Input Json is not valid: Array is not supported as root element."));
        }


        [Test]
        public void InvalidJsonInputThrowsException()
        {
            _testInput.InputJson = @"{ foo baar";

            Assert.Throws<FormatException>(() => JSON.Transform(_testInput));
        }

        [Test]
        public void InvalidJsonMapThrowsException()
        {
            _testInput.JsonMap = @"{""age"":""#valuof($.age)"", ""foo}";

            Assert.Throws<Exception>(() => JSON.Transform(_testInput));
        }
    }
}
