using Virgil.CryptoAPI;

namespace Virgil.Crypto
{
    public interface IKeyPair<TPublicKey, TPrivateKey>
        where TPublicKey : IPublicKey
        where TPrivateKey : IPrivateKey
    {
        TPublicKey PublicKey { get; }
        TPrivateKey PrivateKey { get; }
    }
}
