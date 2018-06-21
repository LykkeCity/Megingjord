using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class EventsResponse : IEnumerable<EventsResponse.Event>
    {
        private readonly IEnumerable<Event> _events;

        public EventsResponse()
        {
            _events = Enumerable.Empty<Event>();
        }
        
        [JsonConstructor]
        public EventsResponse(IEnumerable<Event> events)
        {
            _events = events;
        }
        
        
        public IEnumerator<Event> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        
        public class Event
        {
            [JsonProperty("block")]
            public BlockContext Block { get; set; }
            
            [JsonProperty("data")]
            public string Data { get; set; }

            [JsonProperty("tx")]
            public TxContext Tx { get; set; }
        }
    }
}