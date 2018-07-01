namespace Megingjord.Interfaces
{
    public interface ISignedTransactionRestorer
    {
        ISignedTransaction From(string hexTxData);
        
        ISignedTransaction From(byte[] txData);
    }
}