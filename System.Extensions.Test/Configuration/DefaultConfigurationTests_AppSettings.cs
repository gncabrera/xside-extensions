using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Configuration
{
    [TestFixture]
    class DefaultConfigurationTests_AppSettings
    {
        [SetUp]
        public void Setup()
        {
            DefaultConfiguration.Clean();
        }
        
        [Test]
        public void ICanGetValue_KeyExists()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest");
            Assert.AreEqual(12, actual);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void Breaks_KeyNotExists()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<int>("Inexisting");
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void Breaks_WrongType()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<bool>("MyKeyTest");
        }

        [Test]
        public void ICanGetValue_ExistsKey_WithDefault()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest", 123456);
            Assert.AreEqual(12, actual);
        }

        [Test]
        public void ICanGetValueDefault_KeyNotExists()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<bool>("Inexisting", true);
            Assert.IsTrue(actual);
        }

        [Test]
        public void ICanGetValueDefault_WrongType()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<bool>("MyKeyTest", true);
            Assert.IsTrue(actual);
        }



    }
}
