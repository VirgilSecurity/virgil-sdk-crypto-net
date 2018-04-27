using System;
using Virgil.Crypto;

namespace Tests.Core
{
    public class EmptyClass
    {
        public EmptyClass()
        {
            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            var s = new Virgil.Crypto.Pfs.VirgilPFS();
            var v = new Virgil.Crypto.VirgilCrypto();
            var p = new Virgil.Crypto.Pythia.VirgilPythia();
        }
    }
}
