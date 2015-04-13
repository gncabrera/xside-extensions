using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Configuration
{
    [TestFixture]
    class DefaultConfigurationTests
    {
        [Test]
        public void ICanLoadAndGetKeyFile()
        {
            var TestFile = Directory.GetCurrentDirectory() + @"\TestKeyFile.key";
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            File.WriteAllLines(TestFile, new List<string> { "MyKeyTest=55" });

            DefaultConfiguration.LoadConfiguration().LoadKeyFileFromAppData(TestFile);
            var actual = DefaultConfiguration.GetKeyFileValue<int>("MyKeyTest");
            Assert.AreEqual(55, actual);
        }

        [Test]
        public void ICanLoadAndGetAppSettings()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest");
            Assert.AreEqual(12, actual);
        }

        [Test]
        public void ICanLoadAndGetSpecificSection()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section");
            var actual = DefaultConfiguration.GetSectionValue<bool>("test-section", "MyKeyTest");
            Assert.IsTrue(actual);
        }
    }
}
