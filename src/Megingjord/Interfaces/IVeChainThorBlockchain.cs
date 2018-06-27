using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public interface IVeChainThorBlockchain
    {
        [ItemNotNull]
        Task<string> SendRawTransactionAsync([NotNull] string signedTransaction);
        
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync([NotNull] string address);
        
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync([NotNull] string address, [NotNull] BlockRevision revision);
    }
}