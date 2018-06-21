using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class TransfersResponse : IEnumerable<TransfersResponse.Transfer>
    {
        private readonly IEnumerable<Transfer> _transfers;
        
        
        public TransfersResponse()
        {
            _transfers = Enumerable.Empty<Transfer>();
        }
        
        [JsonConstructor]
        public TransfersResponse(IEnumerable<Transfer> transfers)
        {
            _transfers = transfers;
        }
        
        
        public IEnumerator<Transfer> GetEnumerator()
        {
            return _transfers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        
        public class Transfer
        {
            [JsonProperty("block")]
            public BlockContext Block { get; set; }
            
            [JsonProperty("recipient")]
            public string Recipient { get; set; }
            
            [JsonProperty("sender")]
            public string Sender { get; set; }
            
            [JsonProperty("tx")]
            public TxContext Tx { get; set; }
            
            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
}