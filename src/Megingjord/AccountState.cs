using System.Numerics;
using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public sealed class AccountState
    {
        public AccountState(
            BigInteger balance,
            BigInteger energy,
            bool hasCode)
        {
            Balance = balance;
            Energy = energy;
            HasCode = hasCode;
        }

        public BigInteger Balance { get; }
        
        public BigInteger Energy { get; }
        
        public bool HasCode { get; }
    }
}