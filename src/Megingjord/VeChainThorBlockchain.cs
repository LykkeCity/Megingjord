using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;
using Megingjord.Core.Models;
using Megingjord.Interfaces;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;


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

        public async Task<string> GetBlockRefAsync()
        {
            var response = await _apiClient.GetBlockAsync(BlockRevision.Best);
            var id = response.Id.HexToByteArray();
            var blockRef = id.Slice(0, 8);

            return blockRef.ToHex(true);
        }

        public async Task<string> GetChainTagAsync()
        {
            var response = await _apiClient.GetBlockAsync(BlockRevision.Genesis);
            var id = response.Id.HexToByteArray();
            var chainTag = id.Slice(31);

            return chainTag.ToHex(true);
        }

        public async Task<string> SendRawTransactionAsync(
            string signedTransaction)
        {
            var response = await _apiClient.SendTransactionAsync(new SendTransactionRequest
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