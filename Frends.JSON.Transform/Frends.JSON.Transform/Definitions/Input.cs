using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.JSON.Transform.Definitions
{
    /// <summary>
    /// Input consist of parameters used in this Task.
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Map input json in String or JToken type
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("{\"name\":\"veijo\"}")]
        public object InputJson { get; set; }

        /// <summary>
        /// JUST json map. See: https://github.com/WorkMaze/JUST.net#just
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("{\"firstName\":\"#valueof($.name)\"}")]
        public string JsonMap { get; set; }
    }
}
