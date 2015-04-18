using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class StringExtensionsTests
    {
        [Test]
        public void AGenericConvertionToManyTypesWorks()
        {
            Assert.AreEqual(1234, "1234".ConvertTo<int>());
            Assert.AreEqual(12.34, "12.34".ConvertTo<decimal>());
            Assert.IsTrue("true".ConvertTo<bool>());
            Assert.IsTrue("TruE".ConvertTo<bool>());
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void AGenericConvertionCanBreak()
        {
           "abcd".ConvertTo<int>();
        }

        [Test]
        public void ICanSetDefaultParameterToGenericConvertion()
        {
            Assert.AreEqual(444, "abcd".ConvertTo<int>(444));
        }

        [Test]
        public void ATypeConvertionToManyTypesWorks()
        {
            Assert.AreEqual(1234, "1234".ConvertTo(typeof(int)));
            Assert.AreEqual(12.34, "12,34".ConvertTo(typeof(decimal)));
            Assert.AreEqual(true, "true".ConvertTo(typeof(bool)));
            Assert.AreEqual(true, "TruE".ConvertTo(typeof(bool)));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void ATypeConvertionCanBreak()
        {
            "abcd".ConvertTo(typeof(int));
        }

        [Test]
        public void ICanSetDefaultParameterToTypeConvertion()
        {
            Assert.AreEqual(444, "abcd".ConvertTo(typeof(int), 444));
        }
    }
}
