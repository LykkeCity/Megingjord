using System.Numerics;
using JetBrains.Annotations;
using Megingjord.Interfaces;

namespace Megingjord
{
    [PublicAPI]
    public class Transfer : ITransferSource, ITransferSourceWithRequiredParams
    {
        private BigInteger _amount;
        private IVeChainThorBlockchain _blockchain;
        
        private Transfer()
        {
            
        }
        
        public static ITransferSource PrepareTransferOf(
            BigInteger amount)
        {
            return new Transfer
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
            return Transaction.PrepareTransaction()
                .WithRequiredParams(blockchain: _blockchain)
                .With(@params =>
                {
                    @params
                        .WithClause
                        (
                            to: address,
                            value: _amount,
                            data: null
                        );
                });
        }
    }
}