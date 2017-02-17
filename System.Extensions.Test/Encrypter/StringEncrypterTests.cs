/*
  	Copyright 2017 Gustavo Cabrera
    Licensed under the Apache License, Version 2.0 (the "License");
 	you may not use this file except in compliance with the License.
 	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 */
using System;
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
