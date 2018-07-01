using System.Threading.Tasks;

namespace Megingjord.Interfaces
{
    public interface ISignedTransaction
    {
        string AndEncode();

        string AndSendTo(IVeChainThorBlockchain blockchain);
        
        Task<string> AndSendToAsync(IVeChainThorBlockchain blockchain);
    }
}