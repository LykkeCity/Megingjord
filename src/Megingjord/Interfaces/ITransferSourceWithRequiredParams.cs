namespace Megingjord.Interfaces
{
    public interface ITransferSourceWithRequiredParams
    {
        IUnsignedTransaction To(
            Address address);
    }
}