#pragma warning disable 1591

namespace Frends.JSON.EnforceTypes.Definitions
{
    /// <summary>
    /// Json data type enforcing rule
    /// </summary>
    public class JsonTypeRule
    {
        public JsonTypeRule()
        {
        }

        public JsonTypeRule(string jsonPath, JsonDataType dataType)
        {
            this.JsonPath = jsonPath;
            this.DataType = dataType;
        }

        /// <summary>
        /// JSON path for the rule
        /// </summary>
        public string JsonPath { get; set; }

        /// <summary>
        /// Data type to enforce
        /// </summary>
        public JsonDataType DataType { get; set; }
    }
}
