namespace Megingjord.Interfaces
{
    public interface IUnsignedTransactionRestorer
    {
        IUnsignedTransaction From(byte[] txData);
        
        IUnsignedTransaction From(string txData);
    }
}