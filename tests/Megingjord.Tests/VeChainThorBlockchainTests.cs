using System.Threading.Tasks;
using FluentAssertions;
using Megingjord.Core;
using Megingjord.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Megingjord.Tests
{
    [TestClass]
    public class VeChainThorBlockchainTests
    {
        [TestMethod]
        public async Task GetBlockRefAsync()
        {
            var apiClient = new Mock<IVeChainThorApi>();

            apiClient
                .Setup(x => x.GetBlockAsync(BlockRevision.Best))
                .ReturnsAsync(
                    new BlockResponse
                    {
                        Id = "0x0002d18ea07596b0f63402763de425be7c6939b00b712c9d576b41bc2ef60256"
                    });

            var blockchain = new VeChainThorBlockchain(apiClient.Object);

            (await blockchain.GetBlockRefAsync())
                .Should().Be("0x0002d18ea07596b0");
        }
        
        [TestMethod]
        public async Task GetChainTagAsync()
        {
            var apiClient = new Mock<IVeChainThorApi>();

            apiClient
                .Setup(x => x.GetBlockAsync(BlockRevision.Genesis))
                .ReturnsAsync(
                    new BlockResponse
                    {
                        Id = "0x0002d18ea07596b0f63402763de425be7c6939b00b712c9d576b41bc2ef60256"
                    });

            var blockchain = new VeChainThorBlockchain(apiClient.Object);

            (await blockchain.GetChainTagAsync())
                .Should().Be("0x56");
        }
    }
}