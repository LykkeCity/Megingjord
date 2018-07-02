using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Megingjord.Core;
using Megingjord.Interfaces;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;


namespace Megingjord
{
    [PublicAPI]
    public class Transaction : ITransactionSource,
                               ISignedTransactionRestorer, IUnsignedTransactionRestorer,
                               ITransactionSourceWithRequiredParams, ISignedTransaction, IUnsignedTransaction
    {
        private Params _params;
        private byte[] _privateKey;
        private VeChainThorTransaction _transaction;


        private Transaction()
        {
            
        }

        public static ITransactionSource PrepareTransaction()
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

        private async Task BuildTransactionAsync()
        {
            await _params.ObtainMissedParamsAsync();

            var clauses = _params.Clauses.Select(x => 
                new VeChainThorTransaction.Clause
                (
                    data: x.Data,
                    to: x.To,
                    value: x.Value?.ToBytesForRLPEncoding()
                ));
            
            // ReSharper disable PossibleInvalidOperationException
            _transaction = new VeChainThorTransaction
            (
                blockRef: _params.BlockRef.HexToByteArray(),
                chainTag: _params.ChainTag.HexToByteArray(),
                clauses: clauses,
                dependsOn: _params.DependsOn?.HexToByteArray(),
                expiration: _params.Expiration.Value.ToBytesForRLPEncoding(),
                gas: _params.Gas.Value.ToBytesForRLPEncoding(),
                gasPriceCoef: _params.GasPriceCoef.Value.ToBytesForRLPEncoding(),
                nonce: _params.Nonce.Value.ToBytesForRLPEncoding(),
                reserved: Array.Empty<byte[]>()
            );
            // ReSharper restore PossibleInvalidOperationException
        }
        
        #region ISignedTransaction
        
        string ISignedTransaction.AndEncode()
        {
            return ((ISignedTransaction) this)
                .AndEncodeAsync()
                .Result;
        }
        
        async Task<string> ISignedTransaction.AndEncodeAsync()
        {
            if (_transaction == null)
            {
                await BuildTransactionAsync();
            }
            
            if (_transaction.Signature == null)
            {
                _transaction.Sign(_privateKey);
            }

            return _transaction
                .Encode()
                .ToHex(true);
        }
        
        string ISignedTransaction.AndSendTo(
            IVeChainThorBlockchain blockchain)
        {
            return ((ISignedTransaction) this)
                .AndSendToAsync(blockchain)
                .Result;
        }

        async Task<string> ISignedTransaction.AndSendToAsync(
            IVeChainThorBlockchain blockchain)
        {
            var signedTransaction = await ((ISignedTransaction) this)
                .AndEncodeAsync();
            
            return await blockchain.SendRawTransactionAsync(signedTransaction);
        }
        
        #endregion
        
        #region ISignedTransactionRestorer
        
        ISignedTransaction ISignedTransactionRestorer.From(
            byte[] txData)
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
        
        ISignedTransaction ISignedTransactionRestorer.From(
            string hexTxData)
        {
            return ((ISignedTransactionRestorer) this).From(hexTxData.HexToByteArray());
        }
        
        #endregion
        
        #region ITransactionWithRequiredParams

        IUnsignedTransaction ITransactionSourceWithRequiredParams.With(
            Action<Params> paramsBuilder)
        {
            var nonrequiredParams = new Params();
            
            paramsBuilder(nonrequiredParams);
            
            return this;
        }
        
        #endregion
        
        #region IUnpreparedTransaction

        ITransactionSourceWithRequiredParams ITransactionSource.WithRequiredParams(
            IVeChainThorBlockchain blockchain,
            string chainTag,
            string blockRef,
            BigInteger? gas,
            BigInteger? nonce)
        {
            if (blockchain == null)
            {
                throw new ArgumentNullException(nameof(blockchain));
            }

            _params = new Params(blockchain);

            if (chainTag != null)
            {
                _params
                    .WithChainTag(chainTag);
            }

            if (blockRef != null)
            {
                _params
                    .WithBlockRef(blockRef);
            }

            if (gas.HasValue)
            {
                _params
                    .WithGas(gas.Value);
            }

            if (nonce.HasValue)
            {
                _params
                    .WithNonce(nonce.Value);
            }
            
            return this;
        }
        
        ITransactionSourceWithRequiredParams ITransactionSource.WithRequiredParams(
            string chainTag,
            string blockRef,
            BigInteger gas,
            BigInteger nonce)
        {
            _params = new Params()
                .WithChainTag(chainTag)
                .WithBlockRef(blockRef)
                .WithGas(gas)
                .WithNonce(nonce);

            return this;
        }

        #endregion
        
        #region IUnsignedTransaction
        
        string IUnsignedTransaction.AndEncode()
        {
            return ((IUnsignedTransaction) this)
                .AndEncodeAsync()
                .Result;
        }
        
        async Task<string> IUnsignedTransaction.AndEncodeAsync()
        {
            if (_transaction == null)
            {
                await BuildTransactionAsync();
            }

            return _transaction
                .Encode()
                .ToHex(true);
        }

        ISignedTransaction IUnsignedTransaction.ThenSignWith(
            byte[] privateKey)
        {
            _privateKey = privateKey;
            
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

        #region Common
        
        public class Clause
        {
            internal Clause(
                Address to,
                BigInteger? value,
                byte[] data)
            {
                Data = data;
                To = to;
                Value = value;
            }
            
            public byte[] Data { get; }
            
            public Address To { get; }
            
            public BigInteger? Value { get; }
        }
        
        [PublicAPI]
        public class Params
        {
            private readonly IVeChainThorBlockchain _blockchain;
            private readonly IList<Clause> _clauses;

            
            internal Params()
            {
                _clauses = new List<Clause>();
            }
            
            internal Params(
                IVeChainThorBlockchain blockchain) : this()
            {
                _blockchain = blockchain;
            }
            
            
            public string BlockRef { get; private set; }
            
            public string ChainTag { get; private set; }
            
            public string DependsOn { get; private set; }
            
            public BigInteger? Expiration { get; private set; }
            
            public BigInteger? Gas { get; private set; }
            
            public BigInteger? GasPriceCoef { get; private set; }
            
            public BigInteger? Nonce { get; private set; }
            
            public IEnumerable<Clause> Clauses 
                => _clauses;


            internal async Task ObtainMissedParamsAsync()
            {
                if (BlockRef == null)
                {
                    BlockRef = await _blockchain.GetBlockRefAsync();
                }

                if (ChainTag == null)
                {
                    ChainTag = await _blockchain.GetChainTagAsync();
                }

                if (!Expiration.HasValue)
                {
                    Expiration = BigInteger.Zero;
                }
                
                if (!Gas.HasValue)
                {
                    // TODO: Calculate required amount of gas
                    
                    Gas = new BigInteger(21000);
                }

                if (!GasPriceCoef.HasValue)
                {
                    GasPriceCoef = BigInteger.Zero;
                }

                if (!Nonce.HasValue)
                {
                    Nonce = NonceGenerator.NextRandomNonce();
                }
            }

            public Params WithClause(
                Address to,
                BigInteger? value,
                byte[] data)
            {
                _clauses.Add(
                    new Clause(to, value, data));

                return this;
            }

            public Params WithDependsOn(
                string dependsOn)
            {
                DependsOn = dependsOn;

                return this;
            }

            public Params WithExpiration(
                BigInteger expiration)
            {
                Expiration = expiration;

                return this;
            }
            
            public Params WithGasPriceCoef(
                BigInteger gasPriceCoef)
            {
                GasPriceCoef = gasPriceCoef;

                return this;
            }

            internal Params WithBlockRef(
                string blockRef)
            {
                BlockRef = blockRef;

                return this;
            }
            
            internal Params WithChainTag(
                string chainTag)
            {
                ChainTag = chainTag;

                return this;
            }
            
            internal Params WithGas(
                BigInteger gas)
            {
                Gas = gas;

                return this;
            }
            
            internal Params WithNonce(
                BigInteger nonce)
            {
                Nonce = nonce;

                return this;
            }
        }
        
        #endregion
    }
}