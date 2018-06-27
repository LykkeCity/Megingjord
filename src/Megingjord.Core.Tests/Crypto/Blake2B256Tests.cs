using System.Buffers.Text;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Megingjord.Core.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Megingjord.Core.Tests.Crypto
{
    [TestClass]
    public class Blake2B256Tests
    {
        [TestMethod]
        public void Sum__SingleParameterPassed()
        {
            var dataSample =
                Encoding.ASCII.GetBytes("hello world");
            
            var expectedSum =
                "0x256c83b297114d201b30179f3f0ef0cace9783622da5974326b436178aeef610"
                    .HexToByteArray();

            Blake2B256.Sum(dataSample)
                .Should().BeEquivalentTo(expectedSum);
        }
        
        [TestMethod]
        public void Sum__MultipleParametersPassed()
        {
            var dataSample = new[]
            {
                Encoding.ASCII.GetBytes("hello "),
                Encoding.ASCII.GetBytes("world")
            };
                
            var expectedSum =
                "0x256c83b297114d201b30179f3f0ef0cace9783622da5974326b436178aeef610"
                    .HexToByteArray();

            Blake2B256.Sum(dataSample)
                .Should().BeEquivalentTo(expectedSum);
        }
        
        [TestMethod]
        public async Task SumAsync__SingleParameterPassed()
        {
            var dataSample =
                Encoding.ASCII.GetBytes("hello world");
            
            var expectedSum =
                "0x256c83b297114d201b30179f3f0ef0cace9783622da5974326b436178aeef610"
                    .HexToByteArray();

            (await Blake2B256.SumAsync(dataSample))
                .Should().BeEquivalentTo(expectedSum);
        }
        
        [TestMethod]
        public async Task SumAsync__MultipleParametersPassed()
        {
            var dataSample = new[]
            {
                Encoding.ASCII.GetBytes("hello "),
                Encoding.ASCII.GetBytes("world")
            };
                
            var expectedSum =
                "0x256c83b297114d201b30179f3f0ef0cace9783622da5974326b436178aeef610"
                    .HexToByteArray();

            (await Blake2B256.SumAsync(dataSample))
                .Should().BeEquivalentTo(expectedSum);
        }
    }
}