using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using Virgil.Crypto;

namespace Virgil.Crypto.Tests
{
    public class InternalCryptoTests
    {
        [Test]
        public void GenerateHash_Should_GenerateNonEmptyArray()
        {
            var crypto = new VirgilCrypto();
            var hash = crypto.GenerateHash(Encoding.UTF8.GetBytes("hi"));
            Assert.AreNotEqual(hash, null);
        }

        [Test]
        public void ImportExportedPrivateKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var exportedKey = crypto.ExportPrivateKey(keyPair.PrivateKey, "12345");
            var importedKey = (PrivateKey)crypto.ImportPrivateKey(exportedKey, "12345");


            Assert.IsTrue(importedKey.Id.SequenceEqual(keyPair.PrivateKey.Id));
            Assert.IsTrue(importedKey.RawKey.SequenceEqual(keyPair.PrivateKey.RawKey));

        }

        [Test]
        public void ImportExportedPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var exportedKey = crypto.ExportPublicKey(keyPair.PublicKey);
            var importedKey = (PublicKey)crypto.ImportPublicKey(exportedKey);
            Assert.IsTrue(importedKey.Id.SequenceEqual(keyPair.PublicKey.Id));
            Assert.IsTrue(importedKey.RawKey.SequenceEqual(keyPair.PublicKey.RawKey));

        }

        [Test]
        public void ExtractPublicKey_Should_ReturnEquivalentKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var extractedPublicKey = crypto.ExtractPublicKey(keyPair.PrivateKey);

            Assert.IsTrue(((PublicKey)extractedPublicKey).RawKey.SequenceEqual(keyPair.PublicKey.RawKey));
            Assert.IsTrue(((PublicKey)extractedPublicKey).Id.SequenceEqual(keyPair.PublicKey.Id));

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
        public void KeyPairId_Should_Be8BytesFromSha512FromPublicKey()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            var keyId = crypto.GenerateHash(keyPair.PublicKey.RawKey, HashAlgorithm.SHA512).Take(8).ToArray();
            Assert.AreEqual(keyPair.PrivateKey.Id, keyId);
            Assert.AreEqual(keyPair.PublicKey.Id, keyId);
        }

        [Test]
        public void ExportPrivateKey_Should_ReturnDerFormat()
        {
            var privateKey2Passw = "qwerty";
            var crypto = new VirgilCrypto();
            var privateKey1 = crypto.ImportPrivateKey(
                Convert.FromBase64String(AppSettings.PrivateKeySTC31_1));
            var privateKey2 = crypto.ImportPrivateKey(
                Convert.FromBase64String(AppSettings.PrivateKeySTC31_2), privateKey2Passw);
            var exportedPrivateKey1Bytes = crypto.ExportPrivateKey(privateKey1);
            var privateKeyToDer = VirgilKeyPair.PrivateKeyToDER(((PrivateKey)privateKey1).RawKey);

            var exportedPrivateKey2Bytes = crypto.ExportPrivateKey(privateKey2);
            var privateKeyToDer2 = VirgilKeyPair.PrivateKeyToDER(((PrivateKey)privateKey2).RawKey);

            Assert.IsTrue(privateKeyToDer.SequenceEqual(exportedPrivateKey1Bytes));
            Assert.IsTrue(privateKeyToDer2.SequenceEqual(exportedPrivateKey2Bytes));
        }

        [Test]
        public void ExportPublicKey_Should_ReturnDerFormat()
        {
            var crypto = new VirgilCrypto();
            var publicKey = crypto.ImportPublicKey(
                Convert.FromBase64String(AppSettings.PublicKeySTC32));
            var exportedPublicKey1Bytes = crypto.ExportPublicKey(publicKey);
            var privateKeyToDer = VirgilKeyPair.PublicKeyToDER(((PublicKey)publicKey).RawKey);
            Assert.IsTrue(privateKeyToDer.SequenceEqual(exportedPublicKey1Bytes));
        }

        private byte[] CombineBytesArrays(params byte[][] arrays)
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
