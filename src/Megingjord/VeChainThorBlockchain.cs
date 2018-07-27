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
        internal VeChainThorBlockchain(
            IVeChainThorApi api)
        {
            Api = api;
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
        
        
        public IVeChainThorApi Api { get; }
        

        public async Task<BlockInfo> TryGetBlockAsync(
            BlockRevision revision)
        {
            var response = await Api.GetBlockAsync(revision);

            if (response != null)
            {
                return new BlockInfo
                (
                    beneficiary: response.Beneficiary,
                    gasLimit: response.GasLimit,
                    gasUsed: response.GasUsed,
                    id: response.Id,
                    isTrunk: response.IsTrunk,
                    number: response.Number,
                    parentId: response.ParentId,
                    receiptsRoot: response.ReceiptsRoot,
                    signer: response.Signer,
                    size: response.Size,
                    stateRoot: response.StateRoot,
                    timestamp: response.Timestamp,
                    totalScore: response.TotalScore,
                    transactions: response.Transactions,
                    txsRoot: response.TxsRoot
                );
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetBlockRefAsync(
            BlockRevision revision)
        {
            var response = await Api.GetBlockAsync(revision);
            var id = response.Id.HexToByteArray();
            var blockRef = id.Slice(0, 8);

            return blockRef.ToHex(true);
        }

        public async Task<string> GetChainTagAsync()
        {
            var response = await Api.GetBlockAsync(BlockRevision.Genesis);
            var id = response.Id.HexToByteArray();
            var chainTag = id.Slice(31);

            return chainTag.ToHex(true);
        }

        public async Task<string> SendRawTransactionAsync(
            string signedTransaction)
        {
            var response = await Api.SendTransactionAsync(new SendTransactionRequest
            {
                Raw = signedTransaction
            });

            return response.Id;
        }

        public Task<AccountState> TryGetAccountStateAsync(
            Address address)
        {
            return TryGetAccountStateAsync(address, BlockRevision.Best);
        }
        
        public async Task<AccountState> TryGetAccountStateAsync(
            Address address,
            BlockRevision revision)
        {
            var response = await Api.GetAccountAsync(address.ToString("0xLC"), revision);

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