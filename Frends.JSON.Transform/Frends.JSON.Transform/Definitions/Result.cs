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
        public string Transformation { get; private set; }

        private readonly Lazy<JToken> _jToken;

        public Result(string transformationResult)
        {
            Transformation = transformationResult;

            _jToken = new Lazy<JToken>(() => ParseJson(Transformation));
        }

        /// <summary>
        /// Get transformation result as JToken
        /// </summary>
        public JToken ToJson() { return _jToken.Value; }


        private static JToken ParseJson(string jsonString)
        {
            return JToken.Parse(jsonString);
        }
    }
}
