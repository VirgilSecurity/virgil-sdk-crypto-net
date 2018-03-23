using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using Virgil.CryptoImpl;
using Virgil.Crypto;

namespace Virgil.CryptoImpl.Tests
{
    public class CryptoTests
    {
        [Test]
        public void GenerateHash_Should_GenerateNonEmptyArray()
        {
            var crypto = new VirgilCrypto();
            var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            Assert.AreNotEqual(hash, null);
        }


    }


}
