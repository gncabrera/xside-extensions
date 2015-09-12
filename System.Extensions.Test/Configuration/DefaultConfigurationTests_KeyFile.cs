using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Configuration
{
    [TestFixture]
    class DefaultConfigurationTests_KeyFile
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
        public void ICanLoadAndGetDefaultKeyFile()
        {
            var TestFile = Directory.GetCurrentDirectory() + @"\TestKeyFile.key";
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            File.WriteAllLines(TestFile, new List<string> { "MyKeyTest=88" });

            DefaultConfiguration.LoadConfiguration().LoadKeysFile(TestFile);
            var actual = DefaultConfiguration.GetKeysFileValue<int>("Inexisting", 88);
            Assert.AreEqual(88, actual);
        }

       
        
        
        

       

    }
}
