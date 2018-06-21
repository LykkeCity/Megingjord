using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class AccountResponse
    {
        [JsonProperty("balance")]
        public string Balance { get; set; }
        
        [JsonProperty("energy")]
        public string Energy { get; set; }
        
        [JsonProperty("hasCode")]
        public bool HasCode { get; set; }
    }
}