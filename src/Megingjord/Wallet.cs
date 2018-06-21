using JetBrains.Annotations;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;

namespace Megingjord
{
    [PublicAPI]
    public class Wallet : IWallet
    {
        public Wallet(
            string privateKey)
        {
            Address = GetAddress(privateKey);
            PrivateKey = privateKey;
        }
        
        private Wallet(
            string address,
            string privateKey)
        {
            Address = address;
            PrivateKey = privateKey;
        }
        
        
        public string Address { get; }
        
        public string PrivateKey { get; }
        
        
        public static Wallet Generate()
        {
            var key = EthECKey.GenerateKey();
            var ethAddress = key.GetPublicAddress();
            
            return new Wallet
            (
                address: ThorifyAddress(ethAddress),
                privateKey: key.GetPrivateKey()
            );
        }
        
        public static string GetAddress(string privateKey)
        {
            var ethAddress = EthECKey.GetPublicAddress(privateKey);

            return ThorifyAddress(ethAddress);
        }

        public static string ThorifyAddress(string address)
        {
            return address
                .EnsureHexPrefix()
                .Replace("0x", "Vx");
        }
    }
}