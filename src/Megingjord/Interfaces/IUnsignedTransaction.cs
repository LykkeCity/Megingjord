namespace Megingjord.Interfaces
{
    public interface IUnsignedTransaction
    {
        string AndEncode();

        ISignedTransaction ThenSignWith(byte[] privateKey);
        
        ISignedTransaction ThenSignWith(string privateKey);
    }
}