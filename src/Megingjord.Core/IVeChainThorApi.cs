using System.Threading.Tasks;
using Megingjord.Core.Models;
using Refit;

namespace Megingjord.Core
{
    public interface IVeChainThorApi
    {
        [Post("/accounts/{address}")]
        Task<ContractCallResponse> CallContract(string address, string revision, [Body] ContractCallRequest request);
        
        [Post("/accounts")]
        Task<ContractCallResponse> CreateContract(string revision, [Body] ContractCallRequest request);
        
        [Get("/accounts/{address}")]
        Task<AccountResponse> GetAccountAsync(string address, string revision);
        
        [Get("/accounts/{address}/code")]
        Task<CodeResponse> GetAccountCode(string address, string revision);
        
        [Get("/accounts/{address}/storage/{key}")]
        Task<StorageValueResponse> GetAccountStorageValue(string address, string key, string revision);
        
        [Get("/blocks/{revision}")]
        Task<BlockResponse> GetBlock(string revision);
        
        [Post("/events")]
        Task<EventsResponse> GetEvents(string address, string order, [Body] EventsRequest request);

        [Post("/node/network/peers")]
        Task<NetworkPeersResponse> GetNetworkPeers();
        
        [Post("/transfers")]
        Task<TransfersResponse> GetTransfers([Body] TransfersRequest request);
        
        [Get("/transactions/{id}")]
        Task<TransactionResponse> GetTransaction(string id, bool raw, string revision);
        
        [Get("/transactions/{id}/receipt")]
        Task<TransactionReceiptResponse> GetTransactionReceipt(string id, string revision);
        
        [Post("/transactions")]
        Task<SendTransactionResponse> SendTransactionAsync([Body] SendTransactionRequest request);
    }
}