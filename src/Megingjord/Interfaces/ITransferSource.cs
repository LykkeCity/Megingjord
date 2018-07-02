namespace Megingjord.Interfaces
{
    public interface ITransferSource
    {
        ITransferSourceWithRequiredParams On(
            IVeChainThorBlockchain blockchain);
    }
}