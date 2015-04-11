using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class StringChecker
    {
        /// <summary>
        /// If the provided value is empty or null, a System.ArgumentException is raised.
        /// </summary>
        /// <param name="obj">The string to be checked.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        public void IsNotEmptyOrNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                Check.ThrowArgumentException("The value cannot be empty or null.");

        }

        /// <summary>
        /// If the provided value is empty or null, a System.ArgumentException is raised with a specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="value">The string to be checked.</param>
        /// <param name="paramName">The name of the parameter that causes this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void IsNotEmptyOrNull(string value, string paramName, string message = null)
        {
            if (string.IsNullOrEmpty(value))
                Check.ThrowArgumentException(message, "The value cannot be empty or null.", paramName);
        }

        /// <summary>
        /// If the provided value is empty, null or consists only of white-space characters, 
        /// a System.ArgumentException is raised.
        /// </summary>
        /// <param name="value">The string to be checked.</param>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void IsNotNullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Check.ThrowArgumentException("The value cannot be empty, null or white-space.");


        }

        /// <summary>
        /// If the provided value is empty, null or consists only of white-space characters, 
        /// a System.ArgumentException is raised with a specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="value">The string to be checked.</param>
        /// <param name="paramName">The name of the parameter that causes this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <exception cref="System.ArgumentException">System.ArgumentException</exception>
        public void IsNotNullOrWhiteSpace(string value, string paramName, string message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                Check.ThrowArgumentException(message, "The value cannot be empty, null or white-space.", paramName);
        }
    }
}
