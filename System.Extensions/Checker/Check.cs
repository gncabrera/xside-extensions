using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class Check
    {

        public static ObjectChecker Object { get { return new ObjectChecker(); } }
        public static StringChecker String { get { return new StringChecker(); } }
        public static EnumerableChecker Enumerable { get { return new EnumerableChecker(); } }
        public static DictionaryChecker Dictionary { get { return new DictionaryChecker(); } }

        internal static void ThrowArgumentException(string message, string delfaultMessage, string paramName)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException(delfaultMessage, paramName);
            else
                throw new ArgumentException(message, paramName);
        }

        internal static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }

    }
}
