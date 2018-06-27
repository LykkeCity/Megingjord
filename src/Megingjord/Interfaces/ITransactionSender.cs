using System.Threading.Tasks;

namespace Megingjord
{
    public interface ITransactionSender
    {
        string Result { get; }
        
        Task<string> Async();
    }
}