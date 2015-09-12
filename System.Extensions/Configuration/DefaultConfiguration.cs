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
    public class DefaultConfiguration
    {
        public static Dictionary<string, string> KeysFile { get; internal set; }
        public static Dictionary<string, string> AppSettings { get; internal set; }
        public static Dictionary<string, Dictionary<string, string>> Sections { get; internal set; }


        private static T GetValue<T>(Dictionary<string, string> dictionary, string paramName, string key, T defaultValue, bool returnDefaultOnError)
        {
            Ensure.That(dictionary, paramName).WithExtraMessageOf(() => "The configuration wasn't loaded correctly").IsNotNull();
            try
            {
                return dictionary[key].ConvertTo<T>();
            }
            catch (Exception e)
            {
                if (returnDefaultOnError)
                    return defaultValue;
                else
                    HandleException(e);
            }
            throw new NotImplementedException();
        }

        public static T GetKeysFileValue<T>(string key, T defaultValue)
        {
            return GetValue(KeysFile, "KeysFile", key, defaultValue, true);
        }
        public static T GetKeysFileValue<T>(string key)
        {
            return GetValue(KeysFile, "KeysFile", key, default(T), false);
        }

        public static T GetAppSettingsValue<T>(string key, T defaultValue)
        {
            return GetValue(AppSettings, "AppSettings", key, defaultValue, true);
        }

        public static T GetAppSettingsValue<T>(string key)
        {
            return GetValue(AppSettings, "AppSettings", key, default(T), false);
        }



        public static T GetSectionValue<T>(string section, string key, T defaultValue)
        {
            try
            {
                Ensure.That(Sections).WithExtraMessageOf(() => "The configuration wasn't loaded correctly").IsNotNull();

                var values = Sections[section];

                return GetValue(values, "section", key, defaultValue, true);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            throw new NotImplementedException();
        }

        public static T GetSectionValue<T>(string section, string key)
        {
            try
            {
                Ensure.That(Sections).WithExtraMessageOf(() => "The configuration wasn't loaded correctly").IsNotNull();

                var values = Sections[section];

                return GetValue(values, "section", key, default(T), false);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            throw new NotImplementedException();
        }


        private static void HandleException(Exception e)
        {
            throw new ConfigurationErrorsException("An error has ocurred while trying to obtain a key", e);
        }

        public static ConfigurationLoader LoadConfiguration()
        {
            return new ConfigurationLoader();
        }


        public static void Clean()
        {
            KeysFile = null;
            AppSettings = null;
            Sections = null;

        }
    }
}
