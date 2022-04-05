namespace Frends.JSON.EnforceTypes.Definitions
{
    /// <summary>
    /// Parameters class usually contains parameters that are required.
    /// </summary>
    public class EnforceTypesInput
    {
        /// <summary>
        /// The JSON string to be processed.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// JSON data type rules to enforce.
        /// </summary>
        public JsonTypeRule[] Rules { get; set; }
    }
}
