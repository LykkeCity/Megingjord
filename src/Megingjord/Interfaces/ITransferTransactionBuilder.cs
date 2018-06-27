namespace Megingjord
{
    public interface ITransferTransactionBuilder
    {
        string AsSignedTransaction();

        ITransactionSender OnBlockchain(IVeChainThorBlockchain blockchain);
        
        ITransferTransactionBuilder To(string address);
    }
}