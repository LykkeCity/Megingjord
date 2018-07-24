using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class BlockResponse
    {
        [JsonProperty("beneficiary")]
        public string Beneficiary { get; set; }
        
        [JsonProperty("gasLimit")]
        public ulong GasLimit { get; set; }
        
        [JsonProperty("gasUsed")]
        public ulong GasUsed { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("isTrunk")]
        public bool IsTrunk { get; set; }
        
        [JsonProperty("number")]
        public uint Number { get; set; }

        [JsonProperty("parentID")]
        public string ParentId { get; set; }
        
        [JsonProperty("receiptsRoot")]
        public string ReceiptsRoot { get; set; }
        
        [JsonProperty("signer")]
        public string Signer { get; set; }
        
        [JsonProperty("size")]
        public uint Size { get; set; }
        
        [JsonProperty("stateRoot")]
        public string StateRoot { get; set; }

        [JsonProperty("timestamp")]
        public ulong Timestamp { get; set; }

        [JsonProperty("totalScore")]
        public ulong TotalScore { get; set; }

        [JsonProperty("transactions")]
        public string[] Transactions { get; set; }

        [JsonProperty("txsRoot")]
        public string TxsRoot { get; set; }
    }
}