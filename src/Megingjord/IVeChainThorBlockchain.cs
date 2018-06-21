using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public interface IVeChainThorBlockchain
    {
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync([NotNull] string address);
        
        [ItemCanBeNull]
        Task<AccountState> TryGetAccountStateAsync([NotNull] string address, [NotNull] BlockRevision revision);
    }
}