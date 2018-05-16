using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using Virgil.Crypto;

namespace Tests.iOS
{
    public class HashTest
    {
        [Test]
        public void DoHashTest()
        {
            try{
                var s = new Virgil.Crypto.Pfs.VirgilPFS();
                var v = new Virgil.Crypto.VirgilCrypto();
                var p = new Virgil.Crypto.Pythia.VirgilPythia();

                //var crypto = new VirgilCrypto();
                //var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            }
            catch(Exception e)
            {
                Debug.Write(e.StackTrace);
            }
           
        }
    }
}
