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
        public string JsonAsString { get; private set; }

        public Result(string json)
        {
            JsonAsString = json;
        }
    }
}
