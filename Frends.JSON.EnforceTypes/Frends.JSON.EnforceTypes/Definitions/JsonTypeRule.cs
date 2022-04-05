#pragma warning disable 1591

namespace Frends.JSON.EnforceTypes.Definitions
{
    /// <summary>
    /// Json data type enforcing rule
    /// </summary>
    public class JsonTypeRule
    {
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
