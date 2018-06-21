using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    public class RequestOptions
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }
            
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}