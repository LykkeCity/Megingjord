using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Megingjord.Core.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Megingjord.Core.Tests.Crypto
{
    [TestClass]
    public class Secp256K1Tests
    {
        private static readonly byte[] Address =
            "0xd989829d88b0ed1b06edf5c50174ecfa64f14a64"
                .HexToByteArray();

        private static readonly byte[] MessageHash =
            "0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad"
                .HexToByteArray();
        
        private static readonly byte[] PrivateKey =
            "0x7582be841ca040aa940fff6c05773129e135623e41acce3e0b8ba520dc1ae26a"
                .HexToByteArray();
        
        private static readonly byte[] PublicKey =
            "0x04b90e9bb2617387eba4502c730de65a33878ef384a46f1096d86f2da19043304afa67d0ad09cf2bea0c6f2d1767a9e62a7a7ecc41facf18f2fa505d92243a658f"
                .HexToByteArray();

        private static readonly byte[] Signature =
            "0xf8fe82c74f9e1f5bf443f8a7f8eb968140f554968fdcab0a6ffe904e451c8b9244be44bccb1feb34dd20d9d8943f8c131227e55861736907b02d32c06b934d7200"
                .HexToByteArray();
        
        [TestMethod]
        public void GetAddress()
        {
            Secp256K1.GetAddress(PublicKey)
                .Should().BeEquivalentTo(Address);
        }
        
        [TestMethod]
        public async Task GetAddressAsync()
        {
            (await Secp256K1.GetAddressAsync(PublicKey))
                .Should().BeEquivalentTo(Address);
        }

        [TestMethod]
        public void GetPublicKey()
        {
            Secp256K1.GetPublicKey(PrivateKey)
                .Should().BeEquivalentTo(PublicKey);
        }

        [TestMethod]
        public void RecoverAddress()
        {
            Secp256K1.RecoverAddress(MessageHash, Signature)
                .Should().BeEquivalentTo(Address);
        }
        
        [TestMethod]
        public void RecoverPublicKey()
        {
            Secp256K1.RecoverPublicKey(MessageHash, Signature)
                .Should().BeEquivalentTo(PublicKey);
        }

        [TestMethod]
        public void Sign()
        {
            Secp256K1.Sign(MessageHash, PrivateKey)
                .Should().BeEquivalentTo(Signature);
        }
    }
}