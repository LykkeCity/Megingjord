using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class BlockContext
    {
        [JsonProperty("id")]
        public string Id { get; set; }
                
        [JsonProperty("number")]
        public uint Number { get; set; }
                
        [JsonProperty("timestamp")]
        public ulong Timestamp { get; set; }
    }
}