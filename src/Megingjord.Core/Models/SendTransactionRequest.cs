using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    public class SendTransactionRequest
    {
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}