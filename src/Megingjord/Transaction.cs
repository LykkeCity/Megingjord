using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;
using Megingjord.Interfaces;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Megingjord
{
    [PublicAPI]
    public class Transaction : IUnpreparedTransaction,
                               ISignedTransactionRestorer, IUnsignedTransactionRestorer,
                               ITransactionWithRequiredParams, ISignedTransaction, IUnsignedTransaction
    {
        private RequiredParams _requiredParams;
        private NonrequiredParams _nonrequiredParams;
        private VeChainThorTransaction _transaction;


        private Transaction()
        {
            
        }

        public static IUnpreparedTransaction PrepareTransaction()
        {
            return new Transaction();
        }
        
        public static ISignedTransactionRestorer RestoreSignedTransaction()
        {
            return new Transaction();
        }

        public static IUnsignedTransactionRestorer RestoreUnsignedTransaction()
        {
            return new Transaction();
        }

        private void BuildTransactionIfNecessary()
        {
            if (_transaction == null)
            {
                throw new NotImplementedException();
            }
        }

        private string EncodeTransaction()
        {
            return _transaction
                .Encode()
                .ToHex(true);
        }
        
        #region ISignedTransaction
        
        string ISignedTransaction.AndEncode()
        {
            BuildTransactionIfNecessary();

            return EncodeTransaction();
        }
        
        string ISignedTransaction.AndSendTo(IVeChainThorBlockchain blockchain)
        {
            return ((ISignedTransaction) this).AndSendToAsync(blockchain).Result;
        }

        Task<string> ISignedTransaction.AndSendToAsync(IVeChainThorBlockchain blockchain)
        {
            BuildTransactionIfNecessary();
            
            return blockchain.SendRawTransactionAsync
            (
                ((ISignedTransaction) this).AndEncode()
            );
        }
        
        #endregion
        
        #region ISignedTransactionRestorer
        
        ISignedTransaction ISignedTransactionRestorer.From(byte[] txData)
        {
            _transaction = VeChainThorTransaction.Decode(txData);

            if (_transaction.Signature == null)
            {
                throw new ArgumentException
                (
                    $"Specified transaction is not signed. Use {nameof(RestoreUnsignedTransaction)}() method.",
                    nameof(txData)
                );
            }

            return this;
        }
        
        ISignedTransaction ISignedTransactionRestorer.From(string hexTxData)
        {
            return ((ISignedTransactionRestorer) this).From(hexTxData.HexToByteArray());
        }
        
        #endregion
        
        #region ITransactionWithRequiredParams

        IUnsignedTransaction ITransactionWithRequiredParams.With(
            Action<NonrequiredParams> paramsBuilder)
        {
            var nonrequiredParams = new NonrequiredParams();
            
            paramsBuilder(nonrequiredParams);
            
            BuildTransactionIfNecessary();
            
            return this;
        }
        
        #endregion
        
        #region IUnpreparedTransaction

        ITransactionWithRequiredParams IUnpreparedTransaction.WithRequiredParams(
            IVeChainThorBlockchain blockchain,
            byte? chainTag,
            string blockRef,
            BigInteger? gas,
            BigInteger? nonce)
        {
            if (!chainTag.HasValue)
            {
                throw new NotImplementedException();
            }

            if (blockRef == null)
            {
                throw new NotImplementedException();
            }

            if (!gas.HasValue)
            {
                throw new NotImplementedException();
            }

            if (!nonce.HasValue)
            {
                nonce = NonceGenerator.NextRandomNonce();
            }
            
            return ((IUnpreparedTransaction) this).WithRequiredParams
            (
                chainTag: chainTag.Value,
                blockRef: blockRef,
                gas: gas.Value,
                nonce: nonce.Value
            );
        }
        
        ITransactionWithRequiredParams IUnpreparedTransaction.WithRequiredParams(
            byte chainTag,
            string blockRef,
            BigInteger gas,
            BigInteger nonce)
        {
            _requiredParams = new RequiredParams
            {
                BlockRef = blockRef,
                ChainTag = chainTag,
                Gas = gas,
                Nonce = nonce
            };

            return this;
        }

        #endregion
        
        #region IUnsignedTransaction
        
        string IUnsignedTransaction.AndEncode()
        {
            BuildTransactionIfNecessary();

            return EncodeTransaction();
        }

        ISignedTransaction IUnsignedTransaction.ThenSignWith(
            byte[] privateKey)
        {
            BuildTransactionIfNecessary();
            
            _transaction.Sign(privateKey);
            
            return this;
        }

        ISignedTransaction IUnsignedTransaction.ThenSignWith(
            string privateKey)
        {
            return ((IUnsignedTransaction) this).ThenSignWith
            (
                privateKey.HexToByteArray()
            );
        }
        
        #endregion
        
        #region IUnsignedTransactionRestorer
        
        IUnsignedTransaction IUnsignedTransactionRestorer.From(
            byte[] txData)
        {
            _transaction = VeChainThorTransaction.Decode(txData);

            if (_transaction.Signature != null)
            {
                throw new ArgumentException
                (
                    $"Specified transaction is signed. Use {nameof(RestoreSignedTransaction)}() method.",
                    nameof(txData)
                );
            }

            return this;
        }

        IUnsignedTransaction IUnsignedTransactionRestorer.From(
            string hexTxData)
        {
            return ((IUnsignedTransactionRestorer) this)
                .From
                (
                    hexTxData.HexToByteArray()
                );
        }
        
        #endregion


        public class Clause
        {
            public byte[] Data { get; set; }
            
            public Address To { get; set; }
            
            public BigInteger Value { get; set; }
        }
        
        public class NonrequiredParams
        {
            private List<Clause> _clauses;

            public IEnumerable<Clause> Clauses
            {
                get => _clauses;
                set => _clauses = value.ToList();
            }
            
            public string DependsOn { get; set; }
            
            public BigInteger Expiration { get; set; }
            
            public byte GasPriceCoef { get; set; }

            
            public NonrequiredParams WithClause(
                Address to,
                BigInteger value,
                byte[] data)
            {
                _clauses.Add(new Clause
                {
                    Data = data,
                    To = to,
                    Value = value
                });
                
                return this;
            }
        }

        internal class RequiredParams
        {
            public string BlockRef { get; set; }
            
            public byte ChainTag { get; set; }
            
            public BigInteger Gas { get; set; }
            
            public BigInteger Nonce { get; set; }
        }
    }
}