using Newtonsoft.Json.Linq;

#pragma warning disable 1591

namespace Frends.JSON.Transform.Definitions
{
    /// <summary>
    /// Return object with private setters
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Transformation result as string
        /// </summary>
        /// <example>"{\"firstName\":\"veijo\"}"</example>
        public string Transformation { get; private set; }

        /// <summary>
        /// Transformation result as JToken
        /// </summary>
        /// <example>{"firstName": "veijo"}</example>
        public dynamic JToken { get; private set; }

        public Result(string transformationResult, JToken jToken)
        {
            Transformation = transformationResult;

            JToken = jToken;
        }
    }
}
