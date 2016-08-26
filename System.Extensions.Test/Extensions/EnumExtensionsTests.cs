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
