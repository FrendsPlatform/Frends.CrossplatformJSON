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
        /// <returns>string</returns>
        public static string EnforceJsonTypes(EnforceJsonTypesInput input, CancellationToken cancellationToken)
        {
            var jObject = JObject.Parse(input.Json);

            foreach (var rule in input.Rules)
            {
                cancellationToken.ThrowIfCancellationRequested();

                foreach (var jToken in jObject.SelectTokens(rule.JsonPath))
                {
                    ChangeDataType(jToken, rule.DataType);
                }
            }

            return jObject.ToString();
        }

        /// <summary>
        /// Changes value of JValue object to the desired Json data type
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
            if (jToken.ToString().Equals("null")) return;
            if (jToken.Type == JTokenType.Null) return;
            if (jToken is JArray) return;
            if (jToken.Parent is JProperty jProperty)
            {
                var jArray = new JArray {  jToken };
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
                    case JsonDataType.Array:
                        // Here we actually need to replace the JValue with a JArray that would contain the current JValue
                        if (value.Parent is JProperty jProperty)
                        {
                            var jArray = new JArray { value };
                            jProperty.Value = jArray;
                        }

                        break;
                    default:
                        throw new Exception($"Unknown Json data type {dataType}");
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
