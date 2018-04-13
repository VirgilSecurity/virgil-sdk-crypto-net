using System.Text;
using AppKit;
using Virgil.Crypto;

namespace Tests.MacApp
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            var crypto = new VirgilCrypto();
            var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));

            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());

            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
