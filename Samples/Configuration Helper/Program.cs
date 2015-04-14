using System;
using System.Collections.Generic;
using System.Extensions;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration_Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Loading AppSettings...");

            // Loads the configuration
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();

            // DefaultConfiguration.AppSettings is a Dictionary with all keys and values as strings
            Dictionary<string, string> appSettings = DefaultConfiguration.AppSettings;

            // The method GetAppSettingsValue returns a typed value of the key
            int appSettingsValueTyped = DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest");
            Console.WriteLine("MyKeyTest from AppSettings Parsing: " + appSettingsValueTyped);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Loading Sections section-A & section-B...");

            // Loads all specified sections
            DefaultConfiguration.LoadConfiguration().LoadSections("section-A", "section-B");

            // DefaultConfiguration.Sections Dictionary with all sections with their keys and values as string
            Dictionary<string, string> sectionA = DefaultConfiguration.Sections["section-A"];
            Dictionary<string, string> sectionB = DefaultConfiguration.Sections["section-B"];

            // The method will return the value of the key as bool
            bool sectionAValue = DefaultConfiguration.GetSectionValue<bool>("section-A", "MyKeyTestA");
            Console.WriteLine("MyKeyTestA from Section A: " + sectionAValue);

            // The method will return the value of the key as decimal
            decimal sectionBValue = DefaultConfiguration.GetSectionValue<decimal>("section-B", "MyKeyTestB");
            Console.WriteLine("MyKeyTestB from Section B: " + sectionBValue);


            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Loading Keys File...");

            // Loads the specified keys file. The first run will encrypt all unencrypted keys
            var keysFile = Directory.GetCurrentDirectory() + @"\myKeys.key";
            DefaultConfiguration.LoadConfiguration().LoadKeysFile(keysFile, "myPassword");

            // Loads the specified key file from %APPDATA%\folder\my-file
            //DefaultConfiguration.LoadConfiguration().LoadKeysFileFromAppData(@"folder\my-file.key"); 

            // DefaultConfiguration.KeysFile is a Dictionary with all keys and values as strings
            Dictionary<string, string> keys = DefaultConfiguration.KeysFile;

            // Gets the value of the key as int
            int token = DefaultConfiguration.GetKeysFileValue<int>("my-secret-token");
            Console.WriteLine("Token from KeysFile: " + token);

            Console.WriteLine();
            Console.WriteLine();

            // A nice way to keep all the configuration ordered is to write a wrapper like MyConfiguration and add all your settings there

            Console.WriteLine("Getting data from MyConfiguration: ");
            Console.WriteLine("AppSettings_MyKeyTest: " + MyConfiguration.AppSettings_MyKeyTest);
            Console.WriteLine("SectionA_MyKeyTest: " + MyConfiguration.SectionA_MyKeyTest);
            Console.WriteLine("SectionB_MyKeyTest: " + MyConfiguration.SectionB_MyKeyTest);
            Console.WriteLine("KeysFile_Token: " + MyConfiguration.KeysFile_Token);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Hit ENTER to exit");
            Console.ReadLine();
        }
    }
}
