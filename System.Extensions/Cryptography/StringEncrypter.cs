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

using EnsureThat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class StringEncrypter
    {
        /// <summary>
        /// Encrypts a string using a SHA1 + Salt. The salt must be generated preiously with
        /// the StringEncrypter.GenerateSalt() method
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <param name="salt">A valid salt.</param>
        /// <returns>The encrypted value</returns>
        public static string EncryptWithSalt(this string value, string salt)
        {
            HashAlgorithm algorithm = new SHA1Managed();

            var plainTextBytes = Encoding.ASCII.GetBytes(value);
            var saltBytes = Encoding.ASCII.GetBytes(salt);

            var passwordWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes);
            var saltedSHA1Bytes = algorithm.ComputeHash(passwordWithSaltBytes);
            var saltedSHA1WithAppendedSaltBytes = AppendByteArray(saltedSHA1Bytes, saltBytes);

            return Convert.ToBase64String(saltedSHA1WithAppendedSaltBytes);
        }

        public static string GenerateSalt(this string value, int saltSize)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[saltSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(saltSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private static byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult = new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }

        static readonly string SaltKey = "lCdCV1cIV9UQ5g9AKNqMluZuHFbR1xcvxKSfyPUOj4tF13jW4w";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string EncryptWithPassword(this string value, string password)
        {
            Ensure.That(value, "value").IsNotNull();
            Ensure.That(password, "password").IsNotNull();

            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);

                byte[] keyBytes = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                byte[] cipherTextBytes;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception e)
            {

                throw new EncryptionException("An error has occured while encrypting.", e);
            }
        }

        public static string DecryptWithPassword(this string value, string password)
        {
            Ensure.That(value, "value").IsNotNull();
            Ensure.That(password, "password").IsNotNull();
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(value);
                byte[] keyBytes = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception e)
            {

                throw new EncryptionException("An error has occured while decrypting.", e);
            }


        }
    }
}
