using System.Numerics;


namespace Megingjord.Interfaces
{
    public interface ITransactionSource
    {   
        ITransactionSourceWithRequiredParams WithRequiredParams(
            string chainTag,
            string blockRef,
            BigInteger gas,
            BigInteger nonce);
        
        ITransactionSourceWithRequiredParams WithRequiredParams(
            IVeChainThorBlockchain blockchain,
            string chainTag = null,
            string blockRef = null,
            BigInteger? gas = null,
            BigInteger? nonce = null);
    }
}