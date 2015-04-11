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

        /// <summary>
        /// If the provided enumerable is null, a System.ArgumentNullException is raised. If the provided
        /// enumerable is empty, a System.ArgumentException is raised.
        /// </summary>
        /// <param name="enumerable">The enumerable to be checked.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void HasElements<T>(IEnumerable<T> enumerable)
        {
            Check.Object.IsNotNull(enumerable);
            if (!enumerable.Any())
                Check.ThrowArgumentException(EMPTY_ENUMERABLE);
        }

        /// <summary>
        /// If the provided enumerable is null, a System.ArgumentNullException is raised. If the provided
        /// enumerable does not contain the specified amount of elements, a System.ArgumentException is raised.
        /// </summary>
        /// <param name="enumerable">The enumerable to be checked.</param>
        /// <param name="size">The size the enumerable should be.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void HasElements<T>(IEnumerable<T> enumerable, int size)
        {
            Check.Object.IsNotNull(enumerable);
            if (enumerable.Count() != size)
                Check.ThrowArgumentException(string.Format(ENUMERABLE_INVALID_SIZE, enumerable.Count(), size));
        }

        /// <summary>
        /// If the provided enumerable is null, a System.ArgumentNullException is raised. If the provided
        /// enumerable is empty, a System.ArgumentException is raised with the specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="enumerable">The enumerable to be checked.</param>
        /// /// <param name="paramName">The name of the parameter that causes this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void HasElements<T>(IEnumerable<T> enumerable, string paramName, string message = null)
        {
            Check.Object.IsNotNull(enumerable);
            if (!enumerable.Any())
                Check.ThrowArgumentException(message, EMPTY_ENUMERABLE, paramName);
        }

        /// <summary>
        /// If the provided enumerable is null, a System.ArgumentNullException is raised. If the provided
        /// enumerable does not contain the specified amount of elements, a System.ArgumentException is raised 
        /// with the specified error message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="enumerable">The enumerable to be checked.</param>
        /// <param name="size">The size the enumerable should be.</param>
        /// <param name="paramName">The name of the parameter that causes this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void HasElements<T>(IEnumerable<T> enumerable, int size, string paramName, string message = null)
        {
            Check.Object.IsNotNull(enumerable);
            if (enumerable.Count() != size)
                Check.ThrowArgumentException(message, string.Format(ENUMERABLE_INVALID_SIZE, enumerable.Count(), size), paramName);

        }
    }
}
