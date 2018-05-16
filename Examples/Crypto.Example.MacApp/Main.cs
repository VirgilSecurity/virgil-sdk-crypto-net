using System.Text;
using AppKit;
using Virgil.Crypto;

namespace Crypto.Example.MacApp
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            var crypto = new VirgilCrypto();
            var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            var p = new Virgil.Crypto.Pythia.VirgilPythia();

            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
