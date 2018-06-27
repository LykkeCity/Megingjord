using System;
using System.Numerics;
using JetBrains.Annotations;
using Megingjord.Core;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;

namespace Megingjord
{
    [PublicAPI]
    public class Wallet : IWallet
    {
        private Wallet(
            string address,
            string privateKey)
        {
            Address = address;
            PrivateKey = privateKey;
        }
        
        
        public string Address { get; }
        
        public string PrivateKey { get; }
        
        
        public ITransferTransactionBuilder Transfer(
            BigInteger amount)
        {
            return Transfer(amount, TransferAsset.VET);
        }

        public ITransferTransactionBuilder Transfer(
            BigInteger amount,
            TransferAsset asset)
        {
            switch (asset)
            {
                case TransferAsset.VET:
                    return new TransferTransactionBuilder
                    (
                        amount: amount,
                        privateKey: PrivateKey
                    );
                case TransferAsset.VTHO:
                    return Transfer(amount, Constants.VTHOAddress);
                default:
                    throw new ArgumentOutOfRangeException(nameof(asset), asset, null);
            }
        }

        public ITransferTransactionBuilder Transfer(
            BigInteger amount,
            string contractAddress)
        {
            return new TokenTransferTransactionBuilder
            (
                amount: amount,
                contractAddress: contractAddress,
                privateKey: PrivateKey
            );
        }


        public static IWallet Generate()
        {
            var key = EthECKey.GenerateKey();
            var ethAddress = key.GetPublicAddress();
            
            return new Wallet
            (
                address: Utils.ThorifyAddress(ethAddress),
                privateKey: key.GetPrivateKey()
            );
        }
        
        public static IWallet Load(
            string privateKey)
        {
            return new Wallet
            (
                address: Utils.GetAddress(privateKey),
                privateKey: privateKey
            );
        }
        
        public static class Utils
        {
            public static string GetAddress(
                string privateKey)
            {
                var ethAddress = EthECKey.GetPublicAddress(privateKey);

                return ThorifyAddress(ethAddress);
            }
            
            public static string ThorifyAddress(
                string address)
            {
                return address
                    .EnsureHexPrefix()
                    .Replace("0x", "Vx");
            }
        }
    }
}