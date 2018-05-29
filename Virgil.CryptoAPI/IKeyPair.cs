using Virgil.CryptoAPI;

namespace Virgil.Crypto
{
    public class IKeyPair<TPublicKey, TPrivateKey>
        where TPublicKey : IPublicKey
        where TPrivateKey : IPrivateKey
    {
        public TPublicKey PublicKey { get; }
        public TPrivateKey PrivateKey { get; }
    }
}
