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
