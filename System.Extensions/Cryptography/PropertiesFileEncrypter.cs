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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class PropertiesFileEncrypter
    {
        public PropertiesFileEncrypter(string keyFile)
        {
            Ensure.That(keyFile, "Key File").IsNotNullOrWhiteSpace();
            if (!File.Exists(keyFile)) throw new EncryptionException("File [" + keyFile + "] does not exist.");
            KeyFile = keyFile;
            MasterPassword = "BYG5fCNmzDP62QRwcBDP8szxSacZaOi6cViEZhq4U0U8WQvDG5".Base64Encoded();
        }

        public PropertiesFileEncrypter(string keyFile, string masterPassword)
            : this(keyFile)
        {
            Ensure.That(masterPassword, "Master Password").IsNotNullOrWhiteSpace();
            MasterPassword = masterPassword.Base64Encoded();
        }

        public string KeyFile { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }
        public string MasterPassword { get; private set; }

        /// <summary>
        /// Loads the properties from the file with a master password if specified. See the documentation for more information
        /// </summary>
        public void LoadProperties()
        {
            if (!File.Exists(KeyFile)) throw new EncryptionException("File [" + KeyFile + "] does not exist.");
            try
            {
                var fileLines = File.ReadAllLines(KeyFile);
                Properties = ParseLines(fileLines);
                Properties = EncryptProperties(Properties);
                File.WriteAllLines(KeyFile, Properties.Select(pair => pair.Key + "=" + pair.Value));
                Properties = DecryptProperties(Properties);
            }
            catch (Exception e)
            {

                throw new EncryptionException("An error has occured while Loading properties from file [" + KeyFile + "]", e);
            }
        }

        private Dictionary<string, string> ParseLines(string[] lines)
        {
            var parsedLines = new Dictionary<string, string>();
            var invalidLines = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("#")) continue; // ignore comments
                if (string.IsNullOrWhiteSpace(line)) continue; //ignore empty lines

                if (line.Contains("="))
                {
                    var idx = line.IndexOf("=");
                    if (idx == 0)
                    {
                        invalidLines.Add(line);
                        continue;
                    }

                    var key = line.Substring(0, idx).Trim();
                    var value = line.Substring(idx + 1).Trim();


                    Regex r = new Regex(@"^[a-zA-Z0-9.\-]*$");
                    if (r.IsMatch(key))
                    {
                        if (parsedLines.ContainsKey(key))
                        {
                            invalidLines.Add("The key [" + key + "] is repeated");
                            continue;
                        }
                        parsedLines.Add(key, value);
                    }
                    else
                    {
                        invalidLines.Add("The key [" + key + "] is is not valid. Only alphanumeric and '.' characters are allowed");

                    }
                }
                else
                {
                    invalidLines.Add(line);
                }
            }

            if (invalidLines.Any())
                throw new EncryptionException("The following lines are invalid: " + Environment.NewLine + invalidLines.JoinWithNewLine());
            return parsedLines;
        }

        private Dictionary<string, string> EncryptProperties(Dictionary<string, string> properties)
        {
            var encryptedProperties = new Dictionary<string, string>();
            foreach (var pair in properties)
            {
                var value = pair.Value;
                if (!IsEncrypted(pair.Value))
                {
                    value = Encrypt(pair.Value);
                }
                encryptedProperties.Add(pair.Key, value);


            }
            return encryptedProperties;
        }

        private Dictionary<string, string> DecryptProperties(Dictionary<string, string> properties)
        {
            return properties.ToDictionary(p => p.Key, p => Decrypt(p.Value));
        }

        private const string DecryptionKey = "encrypted-";
        private string Decrypt(string value)
        {
            return value.DecryptWithPassword(MasterPassword).Substring(DecryptionKey.Length);
        }
        private string Encrypt(string value)
        {
            var toEncrypt = DecryptionKey + value;
            return toEncrypt.EncryptWithPassword(MasterPassword);
        }

        private bool IsEncrypted(string value)
        {
            try
            {
                return value.DecryptWithPassword(MasterPassword).StartsWith(DecryptionKey);
            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}
