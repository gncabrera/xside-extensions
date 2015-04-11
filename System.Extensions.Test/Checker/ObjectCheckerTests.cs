using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class ObjectCheckerTests
    {
        [Test]
        public void AValidObjectPasses()
        {
            Check.Object.IsNotNull("value");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ANullObjectBreaksInNotNull()
        {
            Check.Object.IsNotNull(null);
        }

    }
}
