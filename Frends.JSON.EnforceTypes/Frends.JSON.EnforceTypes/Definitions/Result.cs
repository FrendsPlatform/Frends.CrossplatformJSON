using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591

namespace Frends.JSON.EnforceTypes.Definitions
{
    /// <summary>
    /// Return object with private setters
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The input json as a string.
        /// </summary>
        /// <example>"{\"hello\": 123,\"hello_2\": 123.5,\"world\": true,\"bad_arr\": [\"hello, world\"],\"bad_arr_2\": [{\"prop1\": 123}],\"good_arr\": [\"hello, world\"],\"good_arr_2\": [{\"prop1\": 123}]}"</example>
        public string JsonAsString { get; private set; }

        public Result(string json)
        {
            JsonAsString = json;
        }
    }
}
