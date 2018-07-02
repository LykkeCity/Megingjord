using System.Numerics;
using JetBrains.Annotations;
using Megingjord.Interfaces;

namespace Megingjord
{
    [PublicAPI]
    public class TransferEnergy : ITransferSource, ITransferSourceWithRequiredParams
    {
        private BigInteger _amount;
        private IVeChainThorBlockchain _blockchain;
        
        private TransferEnergy()
        {
            
        }
        
        public static ITransferSource PrepareTransferOf(
            BigInteger amount)
        {
            return new TransferEnergy
            {
                _amount = amount
            };
        }
        
        ITransferSourceWithRequiredParams ITransferSource.On(
            IVeChainThorBlockchain blockchain)
        {
            _blockchain = blockchain;

            return this;
        }

        IUnsignedTransaction ITransferSourceWithRequiredParams.To(
            Address address)
        {
            throw new System.NotImplementedException();
        }
    }
}