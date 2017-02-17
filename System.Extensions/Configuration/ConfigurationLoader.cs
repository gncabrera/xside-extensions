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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class ConfigurationLoader
    {
        private static Dictionary<string, string> KeysFile { get { return DefaultConfiguration.KeysFile; } }
        private static Dictionary<string, string> AppSettings { get { return DefaultConfiguration.AppSettings; } }
        private static Dictionary<string, Dictionary<string, string>> Sections { get { return DefaultConfiguration.Sections; } }

        public void LoadAppSettings()
        {
            DefaultConfiguration.AppSettings = DefaultConfiguration.AppSettings ?? new Dictionary<string, string>();
            if (AppSettings.Any())
                throw new ConfigurationErrorsException("AppSettings are already loaded");


            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                AppSettings.Add(key, ConfigurationManager.AppSettings[key]);
            }
        }

        public void LoadKeysFile(string path)
        {
            LoadKeysFile(path, null);
        }

        public void LoadKeysFile(string path, string masterPassword)
        {
            CheckKeyFile();
            LoadFullKeyFile(path, masterPassword);
        }

        public void LoadKeysFileFromAppData(string path, string masterPassword)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = path.StartsWith(@"\") ? path.Substring(1) : path;
            var finalPath = appDataPath + @"\" + path;
            LoadKeysFile(finalPath, masterPassword);
        }

        public void LoadKeysFileFromAppData(string path)
        {
            LoadKeysFileFromAppData(path, null);
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
                KeysFile.Add(prop.Key, prop.Value);
            }
        }

        private void CheckKeyFile()
        {
            DefaultConfiguration.KeysFile = KeysFile ?? new Dictionary<string, string>();
            if (KeysFile.Any())
                throw new ConfigurationErrorsException("KeyFile is already loaded");

        }
        public void LoadSections(params string[] sections)
        {
            DefaultConfiguration.Sections = Sections ?? new Dictionary<string, Dictionary<string, string>>();
            if (Sections.Any())
                throw new ConfigurationErrorsException("Sections are already loaded");

            foreach (var section in sections)
            {
                if (Sections.ContainsKey(section)) continue; //skipping repeated sections
                var obtainedSection = ConfigurationManager.GetSection(section);
                if (obtainedSection == null)
                    throw new ConfigurationErrorsException("The section [" + section + "] does not exists.");
                var newSection = new Dictionary<string, string>();

                if (obtainedSection is NameValueCollection)
                {
                    var nameValues = (NameValueCollection)obtainedSection;
                    foreach (var name in nameValues.AllKeys)
                    {
                        newSection.Add(name, nameValues[name]);
                    }
                }
                else if (obtainedSection is Hashtable)
                {
                    var dictionary = (Hashtable)obtainedSection;
                    foreach (string key in dictionary.Keys)
                    {
                        newSection.Add(key, (string)dictionary[key]);
                    }
                }
                else
                {
                    throw new ConfigurationErrorsException("The configuration type [" + obtainedSection.GetType().FullName + "] is not recognized.");
                }
                Sections.Add(section, newSection);
            }
        }
    }

}
