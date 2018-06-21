using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class ContractCallResponse
    {
        [JsonProperty("data")]
        public string Data { get; set; }
        
        [JsonProperty("events")]
        public Event[] Events { get; set; }
        
        [JsonProperty("gasUsed")]
        public ulong GasUsed { get; set; }

        [JsonProperty("reverted")]
        public bool Reverted { get; set; }
        
        [JsonProperty("transfers")]
        public Transfer[] Transfers { get; set; }
        
        [JsonProperty("vmError")]
        public string VMError { get; set; }
        

        public class Event
        {
            [JsonProperty("address")]
            public string Address { get; set; }
            
            [JsonProperty("data")]
            public string Data { get; set; }
            
            [JsonProperty("topics")]
            public string[] Topics { get; set; }
        }

        public class Transfer
        {
            [JsonProperty("amount")]
            public string Amount { get; set; }
            
            [JsonProperty("recipient")]
            public string Recipient { get; set; }
            
            [JsonProperty("sender")]
            public string Sender { get; set; }
        }
    }
}