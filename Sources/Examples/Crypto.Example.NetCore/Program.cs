using System;
using Virgil.Crypto;

namespace Crypto.Example.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            var s = new Virgil.Crypto.Pfs.VirgilPFS();
            var v = new Virgil.Crypto.VirgilCrypto();
            var p = new Virgil.Crypto.Pythia.VirgilPythia();
        }
    }
}
