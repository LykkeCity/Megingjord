using System.Numerics;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public sealed class BlockRevision
    {
        private const string GenesisBlock = "0";
        private const string BestBlock = "best";
        
        public static BlockRevision Best { get; }
            = new BlockRevision(BestBlock);
        
        public static BlockRevision Genesis { get; }
            = new BlockRevision(GenesisBlock);
        
        
        private readonly string _revision;

        
        public BlockRevision(BigInteger blockNumber)
            : this(blockNumber.ToString())
        {
            
        }
        
        private BlockRevision(string revision)
        {
            _revision = revision;
        }

        public override string ToString()
        {
            return _revision;
        }
        
        public static implicit operator string(BlockRevision revision)
        {
            return revision.ToString();
        }
        
        public static implicit operator BlockRevision(ulong blockNumber)
        {
            return new BlockRevision(blockNumber);
        }
    }
}