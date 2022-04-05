using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591

namespace Frends.JSON.EnforceTypes.Definitions
{
    public class Result
    {
        public string JsonAsString { get; private set; }

        public Result(string json)
        {
            JsonAsString = json;
        }
    }
}
