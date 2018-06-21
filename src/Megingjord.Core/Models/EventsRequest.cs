using JetBrains.Annotations;
using Newtonsoft.Json;


namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class EventsRequest
    {
        [JsonProperty("options")]
        public RequestOptions Options { get; set; }
        
        [JsonProperty("range")]
        public RequestRange Range { get; set; }
        
        [JsonProperty("topicSets")]
        public TopicSet[] TopicsSets { get; set; }

        
        public class TopicSet
        {
            [JsonProperty("topic0")]
            public string Topic0 { get; set; }
            
            [JsonProperty("topic1")]
            public string Topic1 { get; set; }
            
            [JsonProperty("topic2")]
            public string Topic2 { get; set; }
            
            [JsonProperty("topic3")]
            public string Topic3 { get; set; }
            
            [JsonProperty("topic4")]
            public string Topic4 { get; set; }
        }
    }
}