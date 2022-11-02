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
        /// <example>{\"hello\": \"123\",\"hello_2\": \"123.5\",\"world\": \"true\",\"bad_arr\": \"hello, world\",\"bad_arr_2\": { \"prop1\": 123 },\"good_arr\": [ \"hello, world\" ],\"good_arr_2\": [ { \"prop1\": 123 } ]}</example>
        public string Json { get; set; }

        /// <summary>
        /// JSON data type rules to enforce.
        /// </summary>
        /// <example>[{new JsonTypeRule{JsonPath = "hello", DataType = JsonDataType.Number }}, {new JsonTypeRule{JsonPath = "hello_2", DataType = JsonDataType.Number }}]</example>
        public JsonTypeRule[] Rules { get; set; }
    }
}
