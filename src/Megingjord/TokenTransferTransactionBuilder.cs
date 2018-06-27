using System.Numerics;

namespace Megingjord
{
    public class TokenTransferTransactionBuilder : TransferTransactionBuilderBase
    {
        private readonly BigInteger _amount;
        private readonly string _contractAddress;
        private readonly string _privateKey;
        
        
        internal TokenTransferTransactionBuilder(
            BigInteger amount,
            string contractAddress,
            string privateKey)
        {
            _amount = amount;
            _contractAddress = contractAddress;
            _privateKey = privateKey;
        }
        
        
        public override string AsSignedTransaction()
        {
            throw new System.NotImplementedException();
        }

        public override ITransferTransactionBuilder To(string address)
        {
            return new TransferTransactionBuilder(_amount, address);
        }
    }
}