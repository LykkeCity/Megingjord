using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class RequestRange
    {
        [JsonProperty("from")]
        public ulong From { get; set; }

        [JsonProperty("to")]
        public ulong To { get; set; }
            
        [JsonProperty("unit"), JsonConverter(typeof(StringEnumConverter))]
        public RangeUnit Unit { get; set; }
    }
}