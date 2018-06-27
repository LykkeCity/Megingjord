using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Megingjord.Core.Crypto;
using Nethereum.RLP;
using Nethereum.Util;


using static Nethereum.RLP.RLP;

namespace Megingjord.Core
{
    public sealed class VeChainThorTransaction
    {   
        public VeChainThorTransaction(
            [NotNull] byte[] blockRef,
            [NotNull] byte[] chainTag,
            [NotNull] IEnumerable<Clause> clauses,
            [CanBeNull] byte[] dependsOn,
            [NotNull] byte[] expiration,
            [NotNull] byte[] gas,
            [NotNull] byte[] gasPriceCoef,
            [NotNull] byte[] nonce,
            [NotNull] byte[][] reserved)
        {
            BlockRef = blockRef;
            ChainTag = chainTag;
            Clauses = clauses;
            DependsOn = dependsOn;
            Expiration = expiration;
            Gas = gas;
            GasPriceCoef = gasPriceCoef;
            Nonce = nonce;
            Reserved = reserved;
        }
        
        private VeChainThorTransaction(
            byte[] blockRef,
            byte[] chainTag,
            IEnumerable<Clause> clauses,
            byte[] dependsOn,
            byte[] expiration,
            byte[] gas,
            byte[] gasPriceCoef,
            byte[] nonce,
            byte[][] reserved,
            byte[] signature) : this(
                blockRef: blockRef,
                chainTag: chainTag,
                clauses: clauses,
                dependsOn: dependsOn,
                expiration: expiration,
                gas: gas,
                gasPriceCoef: gasPriceCoef,
                nonce: nonce,
                reserved: reserved
            )
        {
            Signature = signature;
        }

        
        public byte[] BlockRef { get; }
        
        public byte[] ChainTag { get; }
        
        public IEnumerable<Clause> Clauses { get; }
        
        public byte[] DependsOn { get; }
        
        public byte[] Expiration { get; }
        
        public byte[] Gas { get; }
        
        public byte[] GasPriceCoef { get; }
        
        public byte[] Nonce { get; }
        
        public byte[][] Reserved { get; }
        
        public byte[] Signature { get; private set; }


        public byte[] Encode()
        {
            return Encode(includeSignature: true);
        }

        public void Sign(
            byte[] privateKey)
        {
            Signature = Secp256K1.Sign
            (
                messageHash: GetSigningHash(),
                privateKey: privateKey
            );
        }
        
        public bool TryGetId(out byte[] id)
        {
            if (Signature != null && TryGetSigner(out var signer))
            {
                id = Blake2B256.Sum
                (
                    GetSigningHash(),
                    signer
                );

                return true;
            }
            else
            {
                id = null;

                return false;
            }
        }
        
        public bool TryGetSigner(out byte[] signer)
        {
            if (Signature != null)
            {
                signer = Secp256K1.RecoverAddress
                (
                    messageHash: GetSigningHash(),
                    signature: Signature
                );
                
                return true;
            }
            else
            {
                signer = null;

                return false;
            }
        }

        public static VeChainThorTransaction Decode(
            byte[] encodedTxData)
        {
            var txElements = (RLPCollection) RLP.Decode(encodedTxData)[0];

            byte[] blockRef = null;
            byte[] chainTag = null;
            IEnumerable<Clause> clauses = null;
            byte[] dependsOn = null;
            byte[] expiration = null;
            byte[] gas = null;
            byte[] gasPriceCoef = null;
            byte[] nonce = null;
            byte[][] reserved = null;
            byte[] signature = null;

            byte[] DecodeBlockRef(IRLPElement rlpElement)
            {
                var encodedBlockRef = rlpElement.RLPData;
                var decodedBlockRef = new byte[8];

                Buffer.BlockCopy
                (
                    src: encodedBlockRef,
                    srcOffset: 0,
                    dst: decodedBlockRef,
                    dstOffset: 8 - encodedBlockRef.Length,
                    count: encodedBlockRef.Length
                );

                return decodedBlockRef;
            }
            
            IEnumerable<Clause> DecodeClauses(IRLPElement rlpElement)
            {
                return ((RLPCollection) rlpElement)
                    .Cast<RLPCollection>()
                    .Select(encodedClause => new Clause(
                        data: encodedClause[2].RLPData,
                        to: encodedClause[0].RLPData,
                        value: encodedClause[1].RLPData))
                    .ToList();
            }

            byte[][] DecodeReserved(IRLPElement rlpElement)
            {
                return ((RLPCollection) rlpElement)
                    .Select(x => x.RLPData)
                    .ToArray();
            }
            
            for (var i = 0; i < txElements.Count; i++)
            {
                var element = txElements[i];
                var elementData = element.RLPData;
                
                switch (i)
                {
                    case 0:
                        chainTag = elementData;
                        break;
                    case 1:
                        blockRef = DecodeBlockRef(element);
                        break;
                    case 2:
                        expiration = elementData;
                        break;
                    case 3:
                        clauses = DecodeClauses(element);
                        break;
                    case 4:
                        gasPriceCoef = elementData;
                        break;
                    case 5:
                        gas = elementData;
                        break;
                    case 6:
                        dependsOn = elementData;
                        break;
                    case 7:
                        nonce = elementData;
                        break;
                    case 8:
                        reserved = DecodeReserved(element);
                        break;
                    case 9:
                        signature = elementData;
                        break;
                }
            }

            return new VeChainThorTransaction
            (
                blockRef: blockRef,
                chainTag: chainTag,
                clauses: clauses,
                dependsOn: dependsOn,
                expiration: expiration,
                gas: gas,
                gasPriceCoef: gasPriceCoef,
                nonce: nonce,
                reserved: reserved,
                signature: signature
            );
        }


        private byte[] Encode(bool includeSignature)
        {
            byte[] EncodeClauses()
            {
                var clauses = Clauses.Select(x => 
                    
                    EncodeList
                    (                
                        EncodeElement(x.To),
                        EncodeElement(x.Value),
                        EncodeElement(x.Data)
                    )
                    
                ).ToArray();

                return EncodeList(clauses);
            }

            var trimmedBlockRef = TrimLeadingZeroes(BlockRef);
            
            var encodedData = new List<byte[]>
            {
                EncodeElement(ChainTag),
                EncodeElement(trimmedBlockRef),
                EncodeElement(Expiration),
                EncodeClauses(),
                EncodeElement(GasPriceCoef),
                EncodeElement(Gas),
                EncodeElement(DependsOn),
                EncodeElement(Nonce),
                EncodeList(Reserved)
            };

            if (includeSignature && Signature != null)
            {
                encodedData.Add
                (
                    EncodeElement(Signature)
                );
            }
            
            return EncodeList
            (
                encodedData.ToArray()
            );
        }

        private byte[] GetSigningHash()
        {
            return Blake2B256.Sum
            (
                data: Encode(includeSignature: false)
            );
        }
        
        private static byte[] TrimLeadingZeroes(byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 0)
                {
                    return bytes.Slice(i);
                }
            }

            return bytes;
        }
        
        
        
        
        public sealed class Clause
        {
            public Clause(
                byte[] data,
                byte[] to,
                byte[] value)
            {
                Data = data;
                To = to;
                Value = value;
            }

            public byte[] Data { get; }
            
            public byte[] To { get; }
            
            public byte[] Value { get; }
        }
    }
}