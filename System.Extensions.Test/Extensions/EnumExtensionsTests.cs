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
using System.Extensions;

namespace System.Extensions.Test.Extensions
{
    [TestFixture]
    class EnumExtensionsTests
    {
        [Test]
        public void ICanGetADescriptionFromAnEnum()
        {
            Assert.AreEqual("MyPropertyWithDescription", Foo.MyPropertyWithDescription.GetDescription());
            Assert.AreEqual("Specific", Foo.MyPropertyWithSpecificDescription.GetDescription());
            Assert.AreEqual("MyPropertyWithoutDescription", Foo.MyPropertyWithoutDescription.GetDescription());
            Assert.AreEqual("MyPropertyWithEmptyDescription", Foo.MyPropertyWithEmptyDescription.GetDescription());
        }

        [Test]
        public void ICanGetAllValuesOfAnEnum()
        {
            var values = EnumHelper.GetValues<Foo>();
            Assert.AreEqual(4, values.Count());
            Assert.IsTrue(values.Any(v => v == Foo.MyPropertyWithDescription));
            Assert.IsTrue(values.Any(v => v == Foo.MyPropertyWithSpecificDescription));
            Assert.IsTrue(values.Any(v => v == Foo.MyPropertyWithoutDescription));
            Assert.IsTrue(values.Any(v => v == Foo.MyPropertyWithEmptyDescription));
        }

        enum Foo {
            [System.ComponentModel.Description]
            MyPropertyWithDescription,

            [System.ComponentModel.Description("Specific")]
            MyPropertyWithSpecificDescription, 

            MyPropertyWithoutDescription,

            [System.ComponentModel.Description("")]
            MyPropertyWithEmptyDescription
        }
    }
}
