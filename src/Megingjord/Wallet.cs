using JetBrains.Annotations;
using Megingjord.Core.Crypto;
using Nethereum.Hex.HexConvertors.Extensions;


namespace Megingjord
{
    [PublicAPI]
    public class Wallet
    {
        private Wallet(
            Address address,
            byte[] privateKey)
        {
            Address = address;
            PrivateKey = privateKey;
        }
        
        
        public Address Address { get; }
        
        public byte[] PrivateKey { get; }
        
        
        public static Wallet Generate()
        {
            return Load
            (
                privateKey: Secp256K1.GeneratePrivateKey()
            );
        }
        
        public static Wallet Load(
            byte[] privateKey)
        {
            var publicKey = Secp256K1.GetPublicKey(privateKey); 
            var address = Secp256K1.GetAddress(publicKey);
            
            return new Wallet
            (
                address: new Address(address),
                privateKey: privateKey
            );
        }
        
        public static Wallet Load(
            string privateKey)
        {
            return Load
            (
                privateKey.HexToByteArray()
            );
        }
    }
}