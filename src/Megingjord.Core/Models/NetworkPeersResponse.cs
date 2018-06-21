using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Megingjord.Core.Models
{
    [PublicAPI]
    public class NetworkPeersResponse : IEnumerable<NetworkPeersResponse.NetworkPeer>
    {
        private readonly IEnumerable<NetworkPeer> _peers;
        
        
        public NetworkPeersResponse()
        {
            _peers = Enumerable.Empty<NetworkPeer>();
        }
        
        [JsonConstructor]
        public NetworkPeersResponse(IEnumerable<NetworkPeer> peers)
        {
            _peers = peers;
        }
        
        
        public IEnumerator<NetworkPeer> GetEnumerator()
        {
            return _peers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        
        public class NetworkPeer
        {
            [JsonProperty("bestBlockID")]
            public string BestBlockId { get; set; }
            
            [JsonProperty("name")]
            public string Name { get; set; }
            
            [JsonProperty("totalScore")]
            public string TotalScore { get; set; }
        }
    }
}