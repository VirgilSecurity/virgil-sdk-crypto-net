# Virgil Security .NET/C# Crypto Library

[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.Crypto.svg)](https://www.nuget.org/packages/Virgil.Crypto/)
[![GitHub license](https://img.shields.io/badge/license-BSD%203--Clause-blue.svg)](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE)

### [Introduction](#introduction) | [Library purposes](#library-purposes) | [Usage examples](#usage-examples) | [Installation](#installation) | [Docs](#docs) | [License](#license) | [Contacts](#support)

## Introduction
VirgilCrypto is a stack of security libraries (ECIES with Crypto Agility wrapped in Virgil Cryptogram) and an open-source high-level [cryptographic library](https://github.com/VirgilSecurity/virgil-crypto) that allows you to perform all necessary operations for securely storing and transferring data in your digital solutions. Crypto Library is written in C++ and is suitable for mobile and server platforms.

Virgil Security, Inc., guides software developers into the forthcoming security world in which everything will be encrypted (and passwords will be eliminated). In this world, the days of developers having to raise millions of dollars to build a secure chat, secure email, secure file-sharing, or a secure anything have come to an end. Now developers can instead focus on building features that give them a competitive market advantage while end-users can enjoy the privacy and security they increasingly demand.

## Library purposes
* Asymmetric Key Generation
* Encryption/Decryption of data and streams
* Generation/Verification of digital signatures
* PFS (Perfect Forward Secrecy)

## Usage examples

#### Generate a key pair

Generate a Private Key with the default algorithm (EC_X25519):
```cs
using Virgil.Crypto;

var crypto = new VirgilCrypto();
var keyPair = crypto.GenerateKeys();
```

#### Generate and verify a signature

Generate signature and sign data with a private key:
```cs
using Virgil.Crypto;

var crypto = new VirgilCrypto();

// prepare a message
var messageToSign = "Hello, Bob!";
var dataToSign = Encoding.UTF8.GetBytes(messageToSign);

// generate a signature
var signature = crypto.GenerateSignature(dataToSign, senderPrivateKey);
```

Verify a signature with a public key:
```cs
using Virgil.Crypto;

var crypto = new VirgilCrypto();

// verify a signature
var verified = crypto.VerifySignature(signature, dataToSign, senderPublicKey);
```

#### Encrypt and decrypt data

Encrypt Data on a Public Key:

```cs
using Virgil.Crypto;

var crypto = new VirgilCrypto();

// prepare a message
var messageToEncrypt = "Hello, Bob!";
var dataToEncrypt = Encoding.UTF8.GetBytes(messageToEncrypt);

// encrypt the message
var encryptedData = crypto.Encrypt(dataToEncrypt, receiverPublicKey);
```

Decrypt the encrypted data with a Private Key:
```cs
using Virgil.Crypto;

var crypto = new VirgilCrypto();

// prepare data to be decrypted
var decryptedData = crypto.Decrypt(encryptedData, receiverPrivateKey);

// decrypt the encrypted data using a private key
var decryptedMessage = Encoding.UTF8.GetString(decryptedData);
```

Need more examples? Visit our [developer documentation](https://developer.virgilsecurity.com/docs/how-to#cryptography).

## Installation

The Virgil .NET Crypto is provided as a package named Virgil.SDK.Crypto. The package is distributed via NuGet package management system.

  Supported Platforms:
   - .NET Standard 1.1+
   - .NET Framework 4.5+
   - .NET Core 1.0+
   - Universal Windows Platform 10
   - Windows 8.0+
   - Windows Phone 8.1+
   - Xamarin.Android 7.0+
   - Xamarin.iOS 10.0+
   - Mono 4.6+ for x86-64(AMD64)

Installing the package using Package Manager Console:

```bash
Run PM> Install-Package Virgil.Crypto
```

## Docs
- [Crypto Core Library](https://github.com/VirgilSecurity/virgil-crypto)
- [More usage examples](https://developer.virgilsecurity.com/docs/how-to#cryptography)

## License

This library is released under the [3-clause BSD License](https://github.com/VirgilSecurity/virgil-sdk-javascript/blob/master/LICENSE).

## Support
Our developer support team is here to help you. Find out more information on our [Help Center](https://help.virgilsecurity.com/).

You can find us on [Twitter](https://twitter.com/VirgilSecurity) or send us email support@VirgilSecurity.com.

Also, get extra help from our support team on [Slack](https://virgilsecurity.slack.com/join/shared_invite/enQtMjg4MDE4ODM3ODA4LTc2OWQwOTQ3YjNhNTQ0ZjJiZDc2NjkzYjYxNTI0YzhmNTY2ZDliMGJjYWQ5YmZiOGU5ZWEzNmJiMWZhYWVmYTM).
