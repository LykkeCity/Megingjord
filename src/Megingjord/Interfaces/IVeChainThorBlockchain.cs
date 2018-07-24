using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;

namespace Megingjord.Interfaces
{
    [PublicAPI]
    public interface IVeChainThorBlockchain
    {
        [NotNull]
        IVeChainThorApi Api { get; }
        
        
        [ItemNotNull]
        Task<string> GetBlockRefAsync(
            [NotNull] BlockRevision revision);
        
        [ItemNotNull]
        Task<string> GetChainTagAsync();

        [ItemNotNull]
        Task<string> SendRawTransactionAsync(
            [NotNull] string signedTransaction);
        
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync(
            [NotNull] string address);
        
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync(
            [NotNull] string address,
            [NotNull] BlockRevision revision);
        
        [ItemCanBeNull]
        Task<BlockInfo> TryGetBlockAsync(
            [NotNull] BlockRevision revision);
    }
}