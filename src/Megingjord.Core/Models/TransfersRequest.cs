using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class TransfersRequest
    {
        [JsonProperty("addressSets")]
        public AddressSet[] AddressSets { get; set; }
        
        [JsonProperty("options")]
        public RequestOptions Options { get; set; }
        
        [JsonProperty("range")]
        public RequestRange Range { get; set; }


        public class AddressSet
        {
            [JsonProperty("recipient")]
            public string Recipient { get; set; }
            
            [JsonProperty("sender")]
            public string Sender { get; set; }
            
            [JsonProperty("txOrigin")]
            public string TxOrigin { get; set; }
        }
    }
}