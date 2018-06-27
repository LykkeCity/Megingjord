using System.Numerics;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public interface IWallet
    {
        string Address { get; }
        
        string PrivateKey { get; }

        
        ITransferTransactionBuilder Transfer(BigInteger amount);
        
        ITransferTransactionBuilder Transfer(BigInteger amount, TransferAsset asset);
        
        ITransferTransactionBuilder Transfer(BigInteger amount, string contractAddress);
    }
}