using System;
using System.Diagnostics;
using System.Text;
using Virgil.Crypto;

namespace Tests.iOS
{
    public class HashTest
    {
        //[Test]
        public void DoHashTest()
        {
            try{
                var pythia = new Virgil.Crypto.Pythia.VirgilPythia();
                var crypto = new VirgilCrypto();
                var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            }
            catch(Exception e)
            {
                Debug.Write(e.StackTrace);
            }
           
        }
    }
}
