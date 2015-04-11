using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class EnumerableChecker
    {

        private static string EMPTY_ENUMERABLE = "The enumerable is empty.";
        private static string ENUMERABLE_INVALID_SIZE = "The enumerable has {0} elements and should have {1}";

        public void HasElements<T>(IEnumerable<T> enumerable)
        {
            Check.Object.IsNotNull(enumerable);
            if (!enumerable.Any())
                Check.ThrowArgumentException(EMPTY_ENUMERABLE);
        }

        public void HasElements<T>(IEnumerable<T> enumerable, int size)
        {
            Check.Object.IsNotNull(enumerable);
            if (enumerable.Count() != size)
                Check.ThrowArgumentException(string.Format(ENUMERABLE_INVALID_SIZE, enumerable.Count(), size));
        }

        public void HasElements<T>(IEnumerable<T> enumerable, string paramName, string message = null)
        {
            Check.Object.IsNotNull(enumerable);
            if (!enumerable.Any())
                Check.ThrowArgumentException(message, EMPTY_ENUMERABLE, paramName);
        }

        public void HasElements<T>(IEnumerable<T> enumerable, int size, string paramName, string message = null)
        {
            Check.Object.IsNotNull(enumerable);
            if (enumerable.Count() != size)
                Check.ThrowArgumentException(message, string.Format(ENUMERABLE_INVALID_SIZE, enumerable.Count(), size), paramName);

        }
    }
}
