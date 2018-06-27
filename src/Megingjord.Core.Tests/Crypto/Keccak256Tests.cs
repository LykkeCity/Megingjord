using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Megingjord.Core.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Megingjord.Core.Tests.Crypto
{
    [TestClass]
    public class Keccak256Tests
    {
        [TestMethod]
        public void Sum__SingleParameterPassed()
        {
            var dataSample =
                Encoding.ASCII.GetBytes("hello world");
            
            var expectedSum =
                "0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad"
                    .HexToByteArray();

            Keccak256.Sum(dataSample)
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
                "0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad"
                    .HexToByteArray();

            Keccak256.Sum(dataSample)
                .Should().BeEquivalentTo(expectedSum);
        }
        
        [TestMethod]
        public async Task SumAsync__SingleParameterPassed()
        {
            var dataSample =
                Encoding.ASCII.GetBytes("hello world");
            
            var expectedSum =
                "0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad"
                    .HexToByteArray();

            (await Keccak256.SumAsync(dataSample))
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
                "0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad"
                    .HexToByteArray();

            (await Keccak256.SumAsync(dataSample))
                .Should().BeEquivalentTo(expectedSum);
        }
    }
}