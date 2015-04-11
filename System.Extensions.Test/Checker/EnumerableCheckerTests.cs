using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class EnumerableCheckerTests
    {
        [Test]
        public void AValidEnumerablePasses()
        {
            Check.Enumerable.HasElements(new List<int> { 1 });
            Check.Enumerable.HasElements(new List<int> { 1, 2 }, 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AnEnumerableWithoutElementsFails()
        {
            List<int> enumerable = new List<int>();
            Check.Enumerable.HasElements(enumerable);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ANullEnumerableFails()
        {
            List<int> enumerable = null;
            Check.Enumerable.HasElements(enumerable);

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ANullEnumerableWithSizeFails()
        {
            List<int> enumerable = null;
            Check.Enumerable.HasElements(enumerable, 5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AnEnumerableWithoutExpectedElementsFails()
        {
            List<int> enumerable = new List<int> { 1, 2, 3 };
            Check.Enumerable.HasElements(enumerable, 2);
        }

    }
}
