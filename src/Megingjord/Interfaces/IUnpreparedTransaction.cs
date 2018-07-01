using System.Numerics;


namespace Megingjord.Interfaces
{
    public interface IUnpreparedTransaction
    {   
        ITransactionWithRequiredParams WithRequiredParams(
            byte chainTag,
            string blockRef,
            BigInteger gas,
            BigInteger nonce);
        
        ITransactionWithRequiredParams WithRequiredParams(
            IVeChainThorBlockchain blockchain,
            byte? chainTag = null,
            string blockRef = null,
            BigInteger? gas = null,
            BigInteger? nonce = null);
    }
}