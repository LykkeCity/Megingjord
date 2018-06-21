using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class ContractCallRequest
    {
        [JsonProperty("data")]
        public string Data { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}