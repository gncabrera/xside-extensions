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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;

namespace System.Extensions.Test
{
    [TestFixture]
    class EnsureDictionaryParamTests
    {
        private const string ParamName = "test";
        private const string Key = "key";
       
        [Test]
        public void HasKey_WhenDictionaryHasKey_ReturnsPassedDictionary()
        {
            
            var value = new Dictionary<string, string>();
            value.Add(Key, "");

            var returnedValue = Ensure.That(value, ParamName).HasKey(Key);
            Assert.AreEqual(ParamName, returnedValue.Name);
            Assert.AreEqual(value, returnedValue.Value);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void HasKey_WhenDictionaryHasNotKey_Throws()
        {
            try
            {
                var value = new Dictionary<string, string>();
                var returnedValue = Ensure.That(value, ParamName).HasKey(Key);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(ParamName, e.ParamName);
                Assert.AreEqual(MyExceptionMessages.EnsureExtensions_NotContains.Inject("Dictionary", " Key " + Key) + "\r\nParameter name: test", 
                    e.Message);
                throw e;
            }
        }

    }
}
