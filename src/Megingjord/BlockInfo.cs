using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public sealed class BlockInfo
    {
        public BlockInfo(
            string beneficiary,
            BigInteger gasLimit,
            BigInteger gasUsed,
            string id,
            bool isTrunk,
            BigInteger number,
            string parentId,
            string receiptsRoot,
            string signer,
            BigInteger size,
            string stateRoot,
            BigInteger timestamp,
            ulong totalScore,
            IEnumerable<string> transactions,
            string txsRoot)
        {
            Beneficiary = beneficiary;
            GasLimit = gasLimit;
            GasUsed = gasUsed;
            Id = id;
            IsTrunk = isTrunk;
            Number = number;
            ParentId = parentId;
            ReceiptsRoot = receiptsRoot;
            Signer = signer;
            Size = size;
            StateRoot = stateRoot;
            Timestamp = timestamp;
            TotalScore = totalScore;
            Transactions = transactions.ToImmutableArray();
            TxsRoot = txsRoot;
        }

        
        public string Beneficiary { get; private set; }
        
        public BigInteger GasLimit { get; private set; }
        
        public BigInteger GasUsed { get; private set; }
        
        public string Id { get; private set; }
        
        public bool IsTrunk { get; private set; }
        
        public BigInteger Number { get; private set; }

        public string ParentId { get; private set; }
        
        public string ReceiptsRoot { get; private set; }
        
        public string Signer { get; private set; }
        
        public BigInteger Size { get; private set; }
        
        public string StateRoot { get; private set; }

        public BigInteger Timestamp { get; private set; }
        
        public ulong TotalScore { get; private set; }

        public ImmutableArray<string> Transactions { get; private set; }

        public string TxsRoot { get; private set; }
    }
}