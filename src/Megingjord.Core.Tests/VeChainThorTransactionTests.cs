using System;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RLP;

namespace Megingjord.Core.Tests
{
    [TestClass]
    public class VeChainThorTransactionTests
    {
        private static void Decode(VeChainThorTransaction transaction)
        {
            var encodedTransaction = transaction.Encode();
            var decodedTransaction = VeChainThorTransaction.Decode(encodedTransaction);

            decodedTransaction.BlockRef
                .Should().BeEquivalentTo(transaction.BlockRef);
            
            decodedTransaction.ChainTag
                .Should().BeEquivalentTo(transaction.ChainTag);
            
            decodedTransaction.DependsOn
                .Should().BeEquivalentTo(transaction.DependsOn);
            
            decodedTransaction.Expiration
                .Should().BeEquivalentTo(transaction.Expiration);
            
            decodedTransaction.Gas
                .Should().BeEquivalentTo(transaction.Gas);

            decodedTransaction.GasPriceCoef
                .Should().BeEquivalentTo(transaction.GasPriceCoef);
            
            decodedTransaction.Nonce
                .Should().BeEquivalentTo(transaction.Nonce);

            
            var clauses = transaction.Clauses.ToList();
            var decodedClauses = decodedTransaction.Clauses.ToList();
            
            decodedClauses.Count
                .Should().Be(clauses.Count);

            for (var i = 0; i < decodedClauses.Count; i++)
            {
                var clause = clauses[i];
                var decodedClause = decodedClauses[i];

                decodedClause.Data
                    .Should().BeEquivalentTo(clause.Data);
                
                decodedClause.To
                    .Should().BeEquivalentTo(clause.To);
                
                decodedClause.Value
                    .Should().BeEquivalentTo(clause.Value);
            }

            
            decodedTransaction.Reserved.Length
                .Should().Be(transaction.Reserved.Length);

            for (var i = 0; i < decodedTransaction.Reserved.Length; i++)
            {
                decodedTransaction.Reserved[i]
                    .Should().BeEquivalentTo(transaction.Reserved[i]);
            }
        }
        
        [TestMethod]
        public void Decode__TransactionHasBeenSigned()
        {
            var transaction = BuildSignedTransaction();
            
            Decode(transaction);
        }
        
        [TestMethod]
        public void Decode__TransactionHasNotBeenSigned()
        {
            var transaction = BuildUnsignedTransaction();
            
            Decode(transaction);
        }
        
        [TestMethod]
        public void Encode__TransactionHasBeenSigned()
        {
            var transaction = BuildSignedTransaction();

            var expectedEncoded =
                "0xf8970184aabbccdd20f840df947567d83b7b8d80addcb281a71d54fc7b3364ffed82271086000000606060df947567d83b7b8d80addcb281a71d54fc7b3364ffed824e208600000060606081808252088083bc614ec0b841f76f3c91a834165872aa9464fc55b03a13f46ea8d3b858e528fcceaf371ad6884193c3f313ff8effbb57fe4d1adc13dceb933bedbf9dbb528d2936203d5511df00"
                    .HexToByteArray();
            
            transaction.Encode()
                .Should().BeEquivalentTo(expectedEncoded);
        }
        
        [TestMethod]
        public void Encode__TransactionHasNotBeenSigned()
        {
            var transaction = BuildUnsignedTransaction();

            var expectedEncoded =
                "0xf8540184aabbccdd20f840df947567d83b7b8d80addcb281a71d54fc7b3364ffed82271086000000606060df947567d83b7b8d80addcb281a71d54fc7b3364ffed824e208600000060606081808252088083bc614ec0"
                    .HexToByteArray();
            
            transaction.Encode()
                .Should().BeEquivalentTo(expectedEncoded);
        }

        [TestMethod]
        public void TryGetId__TransactionHasBeenSigned()
        {
            var transaction = BuildSignedTransaction();

            var expectedId =
                "0xda90eaea52980bc4bb8d40cb2ff84d78433b3b4a6e7d50b75736c5e3e77b71ec"
                    .HexToByteArray();
            
            transaction.TryGetId(out var id)
                .Should().BeTrue();

            id
                .Should().BeEquivalentTo(expectedId);
        }
        
        [TestMethod]
        public void TryGetId__TransactionHasNotBeenSigned()
        {
            var transaction = BuildUnsignedTransaction();
            
            transaction.TryGetId(out var id)
                .Should().BeFalse();

            id
                .Should().BeNull();
        }
        
        [TestMethod]
        public void TryGetSigner__TransactionHasBeenSigned()
        {
            var transaction = BuildSignedTransaction();

            var expectedSigner =
                "0xd989829d88b0ed1b06edf5c50174ecfa64f14a64"
                    .HexToByteArray();
            
            transaction.TryGetSigner(out var signer)
                .Should().BeTrue();

            signer
                .Should().BeEquivalentTo(expectedSigner);
        }
        
        [TestMethod]
        public void TryGetSigner__TransactionHasNotBeenSigned()
        {
            var transaction = BuildUnsignedTransaction();

            transaction.TryGetSigner(out var signer)
                .Should().BeFalse();

            signer
                .Should().BeNull();
        }
        
        [TestMethod]
        public void Sign()
        {
            var transaction = BuildSignedTransaction();

            var expectedSignature =
                "0xf76f3c91a834165872aa9464fc55b03a13f46ea8d3b858e528fcceaf371ad6884193c3f313ff8effbb57fe4d1adc13dceb933bedbf9dbb528d2936203d5511df00"
                    .HexToByteArray();

            transaction.Signature
                .Should().BeEquivalentTo(expectedSignature);
        }

        private static VeChainThorTransaction BuildUnsignedTransaction()
        {
            var clauses = new[]
            {
                new VeChainThorTransaction.Clause
                (
                    data: "0x000000606060".HexToByteArray(), 
                    to: "0x7567d83b7b8d80addcb281a71d54fc7b3364ffed".HexToByteArray(), 
                    value: BigInteger.Parse("10000").ToBytesForRLPEncoding()
                ),
                new VeChainThorTransaction.Clause
                (
                    data: "0x000000606060".HexToByteArray(),
                    to: "0x7567d83b7b8d80addcb281a71d54fc7b3364ffed".HexToByteArray(),
                    value: BigInteger.Parse("20000").ToBytesForRLPEncoding()
                ),
            };
            
            return new VeChainThorTransaction
            (
                blockRef: "0x00000000aabbccdd".HexToByteArray(),
                chainTag: "0x01".HexToByteArray(),
                clauses: clauses,
                dependsOn: null,
                expiration: "0x20".HexToByteArray(),
                gas: BigInteger.Parse("21000").ToBytesForRLPEncoding(),
                gasPriceCoef: "0x80".HexToByteArray(),
                nonce: BigInteger.Parse("12345678").ToBytesForRLPEncoding(),
                reserved: Array.Empty<byte[]>()
            );
        }

        private static VeChainThorTransaction BuildSignedTransaction()
        {
            var transaction = BuildUnsignedTransaction();
            
            var privateKey = 
                "0x7582be841ca040aa940fff6c05773129e135623e41acce3e0b8ba520dc1ae26a"
                    .HexToByteArray();
            
            transaction.Sign(privateKey);

            return transaction;
        }
    }
}