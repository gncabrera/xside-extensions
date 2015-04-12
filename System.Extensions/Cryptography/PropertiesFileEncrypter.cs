using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class PropertiesFileEncrypter
    {
        public PropertiesFileEncrypter(string keyFile)
        {
            Check.String.IsNotNullOrWhiteSpace(keyFile);
            if (!File.Exists(keyFile)) throw new EncryptionException("File [" + KeyFile + "] does not exist.");
            KeyFile = keyFile;
            MasterPassword = "MasterPassword".Base64Encoded();
        }

        public PropertiesFileEncrypter(string keyFile, string masterPassword)
            : this(keyFile)
        {
            Check.String.IsNotEmptyOrNull(masterPassword);
            MasterPassword = masterPassword.Base64Encoded();
        }

        public string KeyFile { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }
        public string MasterPassword { get; private set; }

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

                    var key = line.Substring(0, idx);
                    var value = line.Substring(idx +1);
                    if (parsedLines.ContainsKey(key))
                    {
                        invalidLines.Add("The key [" + key + "] is repeated");
                        continue;
                    }
                    parsedLines.Add(key, value);
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
