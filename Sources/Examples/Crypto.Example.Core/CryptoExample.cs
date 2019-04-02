using System;
using Virgil.Crypto;

namespace Crypto.Example.Std
{
    public class CryptoExample
    {
        public CryptoExample()
        {
            var version = VirgilVersion.AsString();
            var pfs = new Virgil.Crypto.Pfs.VirgilPFS();
            var cryptoHighLevel = new Virgil.Crypto.VirgilCrypto();
            var pythia = new Virgil.Crypto.Pythia.VirgilPythia();
        }
    }
}
