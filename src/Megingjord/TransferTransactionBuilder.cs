using System;
using System.Numerics;

namespace Megingjord
{
    public sealed class TransferTransactionBuilder : TransferTransactionBuilderBase
    {
        private readonly BigInteger _amount;
        private readonly string _privateKey;
        private readonly string _to;
        
        
        internal TransferTransactionBuilder( 
            BigInteger amount,
            string privateKey)
        {
            _amount = amount;
            _privateKey = privateKey;
        }

        private TransferTransactionBuilder(
            BigInteger amount,
            string privateKey,
            string to)
        {
            _amount = amount;
            _privateKey = privateKey;
            _to = to;
        }
        
        
        public override string AsSignedTransaction()
        {
            if (string.IsNullOrEmpty(_to))
            {
                throw new InvalidOperationException("Recipient address has not been specified.");
            }
            
            throw new System.NotImplementedException();
        }

        public override ITransferTransactionBuilder To(string address)
        {
            return new TransferTransactionBuilder
            (
                amount: _amount,
                privateKey: _privateKey,
                to: address
            );
        }
    }
}