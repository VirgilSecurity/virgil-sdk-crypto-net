using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using Virgil.Crypto;

namespace Virgil.Crypto.Tests
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

      
        [Test]
        public void DecryptEncryptedMessage_Should_ReturnEquivalentMessage()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var messageBytes = Encoding.UTF8.GetBytes("hi");
            var encryptedData = crypto.Encrypt(messageBytes, keyPair.PublicKey);
            Assert.AreEqual(messageBytes, crypto.Decrypt(encryptedData, keyPair.PrivateKey));
        }

        [Test]
        public void DecryptEncryptedMessageByGeneratedFromKeyMateria_Should_ReturnEquivalentMessage()
        {
            var crypto = new VirgilCrypto();
                var keyMateria = Encoding.UTF8.GetBytes("26dfhvnslvdsfkdfvnndsb234q2xrFOuY5EDSAFGCCXHCJSHJAD");
            var keyPair = crypto.GenerateKeys();
            var messageBytes = Encoding.UTF8.GetBytes("hi");
            var encryptedData = crypto.Encrypt(messageBytes, keyPair.PublicKey);
            Assert.AreEqual(messageBytes, crypto.Decrypt(encryptedData, keyPair.PrivateKey));
        }

        [Test]
        public void DecryptEncryptedMessageWithWrongPassword_Should_RaiseException()
        {
            var crypto = new VirgilCrypto();
            var aliceKeyPair = crypto.GenerateKeys();
            var bobKeyPair = crypto.GenerateKeys();

            var messageBytes = Encoding.UTF8.GetBytes("hi");
            var encryptedDataForAlice = crypto.Encrypt(messageBytes, aliceKeyPair.PublicKey);
            Assert.Throws<VirgilCryptoException>(() => crypto.Decrypt(encryptedDataForAlice, bobKeyPair.PrivateKey));
        }

        [Test]
        public void GenerateSignature_Should_ReturnValidSignature()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var snapshot = Encoding.UTF8.GetBytes("some card snapshot");
            var signatureSnapshot = Encoding.UTF8.GetBytes("some signature snapshot");
            var extendedSnapshot = signatureSnapshot != null
                ? CombineBytesArrays(snapshot, signatureSnapshot)
                : snapshot;
            var signature = crypto.GenerateSignature(extendedSnapshot, keyPair.PrivateKey);
            Assert.IsTrue(crypto.VerifySignature(signature, extendedSnapshot, keyPair.PublicKey));
        }


        [Test]
        public void DecryptThenVerifyDetached_Should_ReturnOriginData()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var snapshot = Encoding.UTF8.GetBytes("some card snapshot");
            var keyPair2 = crypto.GenerateKeys();
            var encrypted = crypto.SignThenEncryptDetached(snapshot, keyPair.PrivateKey, new PublicKey[] { keyPair2.PublicKey });
            var decrypted = crypto.DecryptThenVerifyDetached(encrypted.Value, encrypted.Meta, keyPair2.PrivateKey, new PublicKey[] { keyPair.PublicKey });
            Assert.AreEqual(snapshot, decrypted);
        }



        private  byte[] CombineBytesArrays(params byte[][] arrays)
        {
            var rv = new byte[arrays.Sum(a => a.Length)];
            var offset = 0;
            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
