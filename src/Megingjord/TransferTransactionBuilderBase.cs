namespace Megingjord
{
    public abstract class TransferTransactionBuilderBase : ITransferTransactionBuilder
    {
        public abstract string AsSignedTransaction();

        public virtual ITransactionSender OnBlockchain(IVeChainThorBlockchain blockchain)
        {
            // ReSharper disable once ArrangeThisQualifier
            var signedTransaction = this.AsSignedTransaction();
            
            return new TransactionSender(blockchain, signedTransaction);
        }

        public abstract ITransferTransactionBuilder To(string address);
    }
}