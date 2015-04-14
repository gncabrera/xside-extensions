using System;
using System.Collections.Generic;
using System.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration_Helper
{
    public class MyConfiguration
    {

        // AppSettings
        public static int AppSettings_MyKeyTest { get { return DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest", 100); } }

        // Sections
        public static bool SectionA_MyKeyTest { get { return DefaultConfiguration.GetSectionValue<bool>("section-A", "MyKeyTestA", false); } }
        public static decimal SectionB_MyKeyTest { get { return DefaultConfiguration.GetSectionValue<decimal>("section-B", "MyKeyTestB", 100); } }

        // KeysFile
        public static int KeysFile_Token { get { return DefaultConfiguration.GetKeysFileValue<int>("my-secret-token", 0); } }
    }
}
