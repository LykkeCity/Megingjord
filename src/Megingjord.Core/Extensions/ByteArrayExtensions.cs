using System.Collections.Generic;
using System.Linq;

namespace Megingjord.Core.Extensions
{
    internal static class ByteArrayExtensions
    {
        public static byte[] ConcatMany(this IEnumerable<byte[]> data)
        {
            return data
                .SelectMany(x => x)
                .ToArray();
        }
    }
}