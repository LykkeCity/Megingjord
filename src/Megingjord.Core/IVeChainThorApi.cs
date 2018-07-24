using System.Threading.Tasks;
using Megingjord.Core.Models;
using Refit;

namespace Megingjord.Core
{
    public interface IVeChainThorApi
    {
        [Post("/accounts/{address}")]
        Task<ContractCallResponse> CallContractAsync(
            string address,
            string revision,
            [Body] ContractCallRequest request);
        
        [Post("/accounts")]
        Task<ContractCallResponse> CreateContractAsync(
            string revision,
            [Body] ContractCallRequest request);
        
        [Get("/accounts/{address}")]
        Task<AccountResponse> GetAccountAsync(
            string address,
            string revision);
        
        [Get("/accounts/{address}/code")]
        Task<CodeResponse> GetAccountCodeAsync(
            string address,
            string revision);
        
        [Get("/accounts/{address}/storage/{key}")]
        Task<StorageValueResponse> GetAccountStorageValueAsync(
            string address,
            string key,
            string revision);
        
        [Get("/blocks/{revision}")]
        Task<BlockResponse> GetBlockAsync(
            string revision);
        
        [Post("/events")]
        Task<EventsResponse> GetEventsAsync(
            string address,
            string order,
            [Body] EventsRequest request);

        [Post("/node/network/peers")]
        Task<NetworkPeersResponse> GetNetworkPeersAsync();
        
        [Post("/transfers")]
        Task<TransfersResponse> GetTransfersAsync(
            [Body] TransfersRequest request);
        
        [Get("/transactions/{id}")]
        Task<TransactionResponse> GetTransactionAsync(
            string id,
            bool raw,
            string revision);
        
        [Get("/transactions/{id}/receipt")]
        Task<TransactionReceiptResponse> GetTransactionReceiptAsync(
            string id, 
            string revision);
        
        [Post("/transactions")]
        Task<SendTransactionResponse> SendTransactionAsync(
            [Body] SendTransactionRequest request);
    }
}