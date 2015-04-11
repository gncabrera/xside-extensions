using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Extensions
{
    public class ObjectChecker
    {
        /// <summary>
        /// If the provided object is null, a System.ArgumentNullException is raised.
        /// </summary>
        /// <param name="obj">The object to be checked.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        public void IsNotNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
        }

        /// <summary>
        /// If the provided object is null, a System.ArgumentNullException is raised with a specified error message 
        /// and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="obj">The object to be checked.</param>
        /// <param name="paramName">The name of the parameter that causes this exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <exception cref="System.ArgumentNullException">System.ArgumentNullException</exception>
        public void IsNotNull(object obj, string paramName, string message = null)
        {
            if (obj == null)
                if (message == null)
                    throw new ArgumentNullException(paramName);
                else
                    throw new ArgumentNullException(paramName, message);
        }
    }
}
