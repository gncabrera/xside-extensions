﻿using NUnit.Framework;
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

        [SetUp]
        public void Setup()
        {
            DefaultConfiguration.Clean();
        }

        [Test]
        public void ICanLoadAndGetKeyFile()
        {
            var TestFile = Directory.GetCurrentDirectory() + @"\TestKeyFile.key";
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            File.WriteAllLines(TestFile, new List<string> { "MyKeyTest=88" });

            DefaultConfiguration.LoadConfiguration().LoadKeysFile(TestFile);
            var actual = DefaultConfiguration.GetKeysFileValue<int>("MyKeyTest");
            Assert.AreEqual(88, actual);
        }

        [Test]
        public void ICanLoadAndGetAppSettings()
        {
            DefaultConfiguration.LoadConfiguration().LoadAppSettings();
            var actual = DefaultConfiguration.GetAppSettingsValue<int>("MyKeyTest");
            Assert.AreEqual(12, actual);
        }

        [Test]
        public void ICanLoadAndGetSpecificNameValueSection()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section");
            var actual = DefaultConfiguration.GetSectionValue<bool>("test-section", "MyKeyTest");
            Assert.IsTrue(actual);
        }

        [Test]
        public void ICanLoadAndGetSpecificDictionarySection()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section-dic");
            var actual = DefaultConfiguration.GetSectionValue<bool>("test-section-dic", "MyKeyTest");
            Assert.IsTrue(actual);
        }
    }
}
