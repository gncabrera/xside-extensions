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
