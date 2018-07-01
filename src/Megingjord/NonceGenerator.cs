using System.Numerics;
using Org.BouncyCastle.Security;

namespace Megingjord
{
    public static class NonceGenerator
    {
        private static readonly SecureRandom Random = new SecureRandom();
        
        public static BigInteger NextRandomNonce()
        {
            return new BigInteger
            (
                Random.NextLong()
            );
        }
    }
}