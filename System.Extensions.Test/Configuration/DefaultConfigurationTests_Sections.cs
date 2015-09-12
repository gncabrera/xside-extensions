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
    class DefaultConfigurationTests_Sections
    {
        [SetUp]
        public void Setup()
        {
            DefaultConfiguration.Clean();
        }


        [Test]
        public void ICanLoadAndGetSpecificNameValueSection()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section");
            var actual = DefaultConfiguration.GetSectionValue<bool>("test-section", "MyKeyTest");
            Assert.IsTrue(actual);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void TryingToLoadUnexistingSpecificNameValueSectionBreaks()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("unexisting");
        }


        [Test]
        public void ICanLoadAndGetSpecificDictionarySection()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section-dic");
            var actual = DefaultConfiguration.GetSectionValue<bool>("test-section-dic", "MyKeyTest");
            Assert.IsTrue(actual);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void UnexistingSpecificNameValueSectionBreaks()
        {
            DefaultConfiguration.LoadConfiguration().LoadSections("test-section");
            var actual = DefaultConfiguration.GetSectionValue<bool>("unexisting", "MyKeyTest");
            Assert.IsTrue(actual);
        }
    }
}
