using System.Threading.Tasks;

namespace Megingjord.Interfaces
{
    public interface ISignedTransaction
    {
        string AndEncode();

        Task<string> AndEncodeAsync();
        
        string AndSendTo(IVeChainThorBlockchain blockchain);
        
        Task<string> AndSendToAsync(IVeChainThorBlockchain blockchain);
    }
}