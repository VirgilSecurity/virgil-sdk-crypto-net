using System;
using System.Diagnostics;
using System.Text;
using AppKit;
using Virgil.Crypto;

namespace Crypto.Example.MacApp
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            var crypto = new VirgilCrypto();
            var pythia = new Virgil.Crypto.Pythia.VirgilPythia();
            try
            {
                var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }

            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
