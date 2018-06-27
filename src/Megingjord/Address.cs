using System;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Megingjord.Core.Crypto;
using Nethereum.Hex.HexConvertors.Extensions;


namespace Megingjord
{
    [PublicAPI]
    public sealed class Address
    {
        private const string AddressPrefix = "Vx";
        private const string HexPrefix = "0x";

        private static readonly Regex AddressStringExpression
            = new Regex($@"^{AddressPrefix}[0-9a-fA-F]{{40}}$", RegexOptions.Compiled);
        
        private readonly byte[] _addressBytes;
        
        
        public Address(
            byte[] addressBytes)
        {
            if (addressBytes.Length != 20)
            {
                throw new ArgumentException
                (
                    "Address should contain 20 bytes",
                    nameof(addressBytes)
                );
            }

            _addressBytes = addressBytes;
        }

        public string ToHex(
            bool prefix = true)
        {
            return _addressBytes.ToHex(prefix);
        }
        
        public override string ToString()
        {
            return $"{AddressPrefix}{_addressBytes.ToHex(prefix: false)}";
        }


        public static Address Parse(
            string addressString)
        {
            ValidateFormatAndThrowIfInvalid(addressString);
            
            return new Address
            (
                addressBytes: Sanitize(addressString).HexToByteArray()
            );
        }
        
        public static bool TryParse(
            string addressString,
            out Address address)
        {
            try
            {
                address = Parse(addressString);

                return true;
            }
            catch (Exception e)
            {
                address = null;

                return false;
            }
        }

        public static bool ValidateFormat(
            string addressString)
        {
            return AddressStringExpression.IsMatch(addressString);
        }

        public static bool ValidateFormatAndChecksum(
            string addressString)
        {
            return ValidateFormat(addressString)
                && ValidateChecksum(addressString);
        }

        
        private static string Sanitize(
            string addressString)
        {
            return addressString
                .Replace(AddressPrefix, "");
        }

        private static bool ValidateChecksum(
            string addressString)
        {
            var addressBytes = Encoding.UTF8.GetBytes(addressString.ToLowerInvariant());
            var caseMapBytes = Keccak256.Sum(addressBytes);
        
            for (var i = 0; i < 40; i++)
            {
                var addressChar = addressString[i];
        
                if (!char.IsLetter(addressChar))
                {
                    continue;
                }
        
                var leftShift = i % 2 == 0 ? 7 : 3;
                var shouldBeUpper = (caseMapBytes[i / 2] & (1 << leftShift)) != 0;
                var shouldBeLower = !shouldBeUpper;
        
                if (shouldBeUpper && char.IsLower(addressChar) ||
                    shouldBeLower && char.IsUpper(addressChar))
                {
                    return false;
                }
            }
        
            return true;
        }
        
        private static void ValidateFormatAndThrowIfInvalid(
            string addressString)
        {
            if (!ValidateFormat(addressString))
            {
                throw new FormatException("Specified address string is not in valid format.");
            }
        }

        
        public static implicit operator byte[](
            Address address)
        {
            return address._addressBytes;
        }

        public static implicit operator string(
            Address address)
        {
            return address.ToString();
        }

        public static explicit operator Address(
            byte[] addressBytes)
        {
            return new Address(addressBytes);
        }
    }
}