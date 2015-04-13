﻿using System;
using NUnit.Framework;


namespace System.Extensions.Test
{
    [TestFixture]
    public class StringEncrypterTests
    {
         [Test]
        public void ICanEncryptSHASaltString()
        {
            string password = "my-super-password";
            string salt = password.GenerateSalt(50);
            string encrypted = password.EncryptWithSalt(salt);


            string passwordReentry = password.EncryptWithSalt(salt);

            Assert.AreEqual(encrypted, passwordReentry);
        }

         [Test]
        public void ICanEncryptAndDecryptWithAPassword()
        {
            string password = "my-super-password";
            string randomString = "raaaaaaaaaaaaandom";

            var encrypted = randomString.EncryptWithPassword(password);
            var decrypted = encrypted.DecryptWithPassword(password);

            Assert.AreEqual(randomString, decrypted);

        }

        [Test]
        public void DecryptionWithWrongPasswordReturnsInvalidValue()
        {
            string password = "my-super-password";
            string randomString = "raaaaaaaaaaaaandom";

            var encrypted = randomString.EncryptWithPassword(password);
            var decrypted = encrypted.DecryptWithPassword("my-wrong-super-password");

            Assert.AreNotEqual(randomString, decrypted);

        }

        [Test]
        [ExpectedException(typeof(EncryptionException))]
        public void TryingToDecryptionInvalidStringBreaks()
        {
            var decrypted = "foo".DecryptWithPassword("bar");
        }
    }
}