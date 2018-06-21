using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;
using Megingjord.Utils;


namespace Megingjord
{
    [PublicAPI]
    public class VeChainThorBlockchain : IVeChainThorBlockchain
    {
        private readonly IVeChainThorApi _apiClient;
        
        
        internal VeChainThorBlockchain(IVeChainThorApi apiClient)
        {
            _apiClient = apiClient;
        }
        
        public static IVeChainThorBlockchain Create(string hostUrl)
        {
            return new VeChainThorBlockchain
            (
                VeChainThorApiClientFactory.CreateClient(hostUrl)
            );
        }
        
        public static IVeChainThorBlockchain Create(HttpClient httpClient)
        {
            return new VeChainThorBlockchain
            (
                VeChainThorApiClientFactory.CreateClient(httpClient)
            );
        }

        public Task<AccountState> TryGetAccountStateAsync(string address)
        {
            return TryGetAccountStateAsync(address, BlockRevision.Best);
        }
        
        public async Task<AccountState> TryGetAccountStateAsync(string address, BlockRevision revision)
        {
            var response = await _apiClient.GetAccountAsync(address, revision);

            if (response != null)
            {
                return new AccountState
                (
                    balance: HexStringToBigIntegerConverter.Convert(response.Balance),
                    energy: HexStringToBigIntegerConverter.Convert(response.Energy),
                    hasCode: response.HasCode
                );
            }
            else
            {
                return null;
            }
        }
    }
}