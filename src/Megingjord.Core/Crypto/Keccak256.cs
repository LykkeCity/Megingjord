using System.Threading.Tasks;
using Megingjord.Core.Extensions;
using Multiformats.Hash;
using Multiformats.Hash.Algorithms;


namespace Megingjord.Core.Crypto
{
    public static class Keccak256
    {
        public static byte[] Sum(params byte[][] data)
        {
            var multihash = Multihash.Sum<KECCAK_256>
            (
                data: data.ConcatMany()
            );

            return multihash.Digest;
        }
        
        public static async Task<byte[]> SumAsync(params byte[][] data)
        {
            var multihash = await Multihash.SumAsync<KECCAK_256>
            (
                data: data.ConcatMany()
            );

            return multihash.Digest;
        }
    }
}