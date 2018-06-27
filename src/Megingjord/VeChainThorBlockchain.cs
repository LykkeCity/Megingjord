using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;
using Megingjord.Core.Models;
using Megingjord.Interfaces;
using Nethereum.Hex.HexConvertors.Extensions;


namespace Megingjord
{
    [PublicAPI]
    public class VeChainThorBlockchain : IVeChainThorBlockchain
    {
        private readonly IVeChainThorApi _apiClient;
        
        
        internal VeChainThorBlockchain(
            IVeChainThorApi apiClient)
        {
            _apiClient = apiClient;
        }
        
        public static IVeChainThorBlockchain Create(
            string hostUrl)
        {
            return new VeChainThorBlockchain
            (
                VeChainThorApiClientFactory.CreateClient(hostUrl)
            );
        }
        
        public static IVeChainThorBlockchain Create(
            HttpClient httpClient)
        {
            return new VeChainThorBlockchain
            (
                VeChainThorApiClientFactory.CreateClient(httpClient)
            );
        }

        public async Task<string> SendRawTransactionAsync(
            string signedTransaction)
        {
            var response = await _apiClient.SendTransaction(new SendTransactionRequest
            {
                Raw = signedTransaction
            });

            return response.Id;
        }

        public Task<AccountState> TryGetAccountStateAsync(
            string address)
        {
            return TryGetAccountStateAsync(address, BlockRevision.Best);
        }
        
        public async Task<AccountState> TryGetAccountStateAsync(
            string address,
            BlockRevision revision)
        {
            var response = await _apiClient.GetAccountAsync(address, revision);

            if (response != null)
            {
                return new AccountState
                (
                    balance: response.Balance.HexToBigInteger(false),
                    energy: response.Balance.HexToBigInteger(false),
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