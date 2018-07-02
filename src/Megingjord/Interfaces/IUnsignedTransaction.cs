using System.Threading.Tasks;

namespace Megingjord.Interfaces
{
    public interface IUnsignedTransaction
    {
        string AndEncode();

        Task<string> AndEncodeAsync();
        
        ISignedTransaction ThenSignWith(byte[] privateKey);
        
        ISignedTransaction ThenSignWith(string privateKey);
    }
}