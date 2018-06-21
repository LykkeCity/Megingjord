using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class CodeResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}