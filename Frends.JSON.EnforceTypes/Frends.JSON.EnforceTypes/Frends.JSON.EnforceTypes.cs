﻿using System.ComponentModel;
using Newtonsoft.Json.Linq;
using Frends.JSON.EnforceTypes.Definitions;

#pragma warning disable 1591

namespace Frends.JSON.EnforceTypes
{
    public class JSON
    {
        /// <summary>
        /// This task allows enforcing types in Json documents by giving an array of
        /// Json paths and corresponding Json types.
        /// [Documentation](https://tasks.frends.com/tasks#frends-tasks/Frends.JSON.EnforceTypes)
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result object { string JsonAsString }</returns>
        public static Result EnforceTypes([PropertyTab]EnforceTypesInput parameters, CancellationToken cancellationToken)
        {
            var jObject = JObject.Parse(parameters.Json);

            foreach (var rule in parameters.Rules)
            {
                // Checks if user has used the $. positional operator in JsonPath input
                if (!rule.JsonPath.StartsWith("$."))
                    rule.JsonPath = "$." + rule.JsonPath;
                foreach (var jToken in jObject.SelectTokens(rule.JsonPath))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ChangeDataType(jToken, rule.DataType);
                }
            }

            return new Result(jObject.ToString());
        }

        /// <summary>
        /// Changes value of JValue object to the desired Json data type.
        /// This method is used in unit tests.
        /// </summary>
        /// <returns></returns>
        public static void ChangeDataType(JToken value, JsonDataType dataType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (dataType == JsonDataType.Array)
            {
                ChangeJTokenIntoArray(value);
                return;
            }

            if (!(value is JValue jValue))
            {
                throw new Exception($"This task can only convert JValue nodes' types and turn JTokens into JArrays, but the node type provided is {value.GetType().Name}");
            }

            ChangeDataTypeSimple(jValue, dataType);
        }

        private static void ChangeJTokenIntoArray(JToken jToken)
        {
            if ((jToken.Parent is JProperty jProperty) && 
                !(jToken.ToString().Equals("null")) && 
                !(jToken.Type == JTokenType.Null) && 
                !(jToken is JArray))
            {
                var jArray = new JArray();
                jArray.Add(jToken);
                jProperty.Value = jArray;
                return;
            }
        }

        private static void ChangeDataTypeSimple(JValue value, JsonDataType dataType)
        {
            object newValue = value.Value;
            try
            {
                switch (dataType)
                {
                    case JsonDataType.String:
                        newValue = value.Value<string>();
                        break;

                    case JsonDataType.Number:
                        if (value.Value == null || (value.Value as string) == "") newValue = null;
                        else
                        {
                            var stringValue = value.Value<string>();
                            if (stringValue.Contains(".")) newValue = value.Value<double>();
                            else newValue = value.Value<int>();
                        }
                        break;

                    case JsonDataType.Boolean:
                        if (value.Value == null || (value.Value as string) == "") newValue = null;
                        else newValue = value.Value<bool>();
                        break;

                    default:
                        throw new Exception($"Unknown JSON data type {dataType}");
                }

                if (dataType != JsonDataType.Array) value.Value = newValue;
            }
            catch
            {
                value.Value = newValue;
            }
        }
    }
}
