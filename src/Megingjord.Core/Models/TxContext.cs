using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class TxContext
    {
        [JsonProperty("id")]
        public string Id { get; set; }
                
        [JsonProperty("origin")]
        public string Origin { get; set; }
    }
}