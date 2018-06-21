using JetBrains.Annotations;

namespace Megingjord
{
    [PublicAPI]
    public interface IWallet
    {
        string Address { get; }
        
        string PrivateKey { get; }
    }
}