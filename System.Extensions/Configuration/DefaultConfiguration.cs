﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class DefaultConfiguration
    {
        public static Dictionary<string, string> KeyFile { get; private set; }
        public static Dictionary<string, string> AppSettings { get; private set; }
        public static Dictionary<string, Dictionary<string, string>> Sections { get; private set; }

        public static T GetKeyFileValue<T>(string key, T defaultValue = default(T))
        {
            try
            {
                Check.Object.IsNotNull(KeyFile, "The configuration wasn't loaded correctly");
                Check.Enumerable.HasElements(KeyFile, null, "The Key File has not any keys");
                Check.Dictionary.HasKey(KeyFile, key, "key", "The Key file does not contain the key [" + key + "]");
                return KeyFile[key].ConvertTo<T>(defaultValue);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            throw new NotImplementedException();

        }

        private static void HandleException(Exception e)
        {
            throw new ConfigurationErrorsException("An error has ocurred while trying to obtain a key");
        }

        public static T GetAppSettingsValue<T>(string key, T defaultValue = default(T))
        {
            try
            {
                Check.Object.IsNotNull(AppSettings, "The configuration wasn't loaded correctly");
                Check.Enumerable.HasElements(AppSettings, null, "The AppSettings has not any keys");
                Check.Dictionary.HasKey(AppSettings, key, "key", "The AppSettings does not contain the key [" + key + "]");
                return AppSettings[key].ConvertTo<T>(defaultValue);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            throw new NotImplementedException();
        }

        public static T GetSectionValue<T>(string section, string key, T defaultValue = default(T))
        {
            try
            {
                Check.Object.IsNotNull(Sections, "The configuration wasn't loaded correctly");
                Check.Enumerable.HasElements(Sections, null, "The Sections has not any keys");
                Check.Dictionary.HasKey(Sections, section, "section", "The Sections does not contain the section [" + section + "]");

                var values = Sections[section];

                Check.Object.IsNotNull(Sections, "The configuration wasn't loaded correctly for section [" + section + "]");
                Check.Enumerable.HasElements(Sections, null, "The section [" + section + "] has not any keys");
                Check.Dictionary.HasKey(values, key, "section", "The section [" + section + "] does not contain the key [" + key + "]");

                return values[key].ConvertTo<T>(defaultValue);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            throw new NotImplementedException();
        }

        public static ConfigurationLoader LoadConfiguration()
        {
            return new ConfigurationLoader();
        }

        public class ConfigurationLoader
        {
            public void LoadAppSettings()
            {
                AppSettings = AppSettings ?? new Dictionary<string, string>();
                if (AppSettings.Any())
                    throw new ConfigurationErrorsException("AppSettings are already loaded");


                foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                {
                    AppSettings.Add(key, ConfigurationManager.AppSettings[key]);
                }
            }

            public void LoadKeyFile(string path)
            {
                LoadFullKeyFile(path, null);
            }

            public void LoadKeyFile(string path, string masterPassword)
            {
                CheckKeyFile();
                LoadFullKeyFile(path, masterPassword);
            }

            public void LoadKeyFileFromAppData(string path, string masterPassword)
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = path.StartsWith(@"\") ? path.Substring(1) : path;
                var finalPath = appDataPath + @"\" + path;
                CheckKeyFile();
                LoadFullKeyFile(path, masterPassword);
            }

            public void LoadKeyFileFromAppData(string path)
            {
                LoadKeyFileFromAppData(path, null);
            }

            private void LoadFullKeyFile(string path, string masterPassword)
            {
                PropertiesFileEncrypter fileLoader = null;
                if (masterPassword == null)
                    fileLoader = new PropertiesFileEncrypter(path);
                else
                    fileLoader = new PropertiesFileEncrypter(path, masterPassword);

                fileLoader.LoadProperties();

                foreach (var prop in fileLoader.Properties)
                {
                    KeyFile.Add(prop.Key, prop.Value);
                }
            }

            private void CheckKeyFile()
            {
                KeyFile = KeyFile ?? new Dictionary<string, string>();
                if (KeyFile.Any())
                    throw new ConfigurationErrorsException("KeyFile is already loaded");

            }
            public void LoadSections(params string[] sections)
            {
                Sections = Sections ?? new Dictionary<string, Dictionary<string, string>>();
                if (Sections.Any())
                    throw new ConfigurationErrorsException("Sections are already loaded");

                foreach (var section in sections)
                {
                    if (Sections.ContainsKey(section)) continue; //skipping repeated sections
                    var nameValues = (NameValueCollection)ConfigurationManager.GetSection(section);

                    var newSection = new Dictionary<string, string>();
                    foreach (var name in nameValues.AllKeys)
                    {
                        newSection.Add(name, nameValues[name]);
                    }
                    Sections.Add(section, newSection);
                }
            }
        }
    }
}