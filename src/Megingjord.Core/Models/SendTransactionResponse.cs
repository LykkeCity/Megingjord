using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    public class SendTransactionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}