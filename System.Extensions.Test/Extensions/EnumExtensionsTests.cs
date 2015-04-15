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
