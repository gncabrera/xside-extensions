using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class StringCheckerTests
    {
        [Test]
        public void AValidStringPasses()
        {
            Check.String.IsNotEmptyOrNull("value");
            Check.String.IsNotNullOrWhiteSpace("value");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AnEmptyStringBreaksOnEmptyOrNull()
        {
            Check.String.IsNotEmptyOrNull("");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ANullStringBreaksOnEmptyOrNull()
        {
            Check.String.IsNotEmptyOrNull(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AnEmptyStringBreaksOnNotNullOrWhiteSpace()
        {
            Check.String.IsNotNullOrWhiteSpace("");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ANullStringBreaksOnNotNullOrWhiteSpace()
        {
            Check.String.IsNotNullOrWhiteSpace(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AWhiteSpaceStringBreaksOnNotNullOrWhiteSpace()
        {
            Check.String.IsNotNullOrWhiteSpace(" ");
        }
    }
}
