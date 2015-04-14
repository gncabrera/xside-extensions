using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class StringJoinsExtensions
    {
        public static string JoinWithComma(this IEnumerable<string> enumerable)
        {
            Check.Object.IsNotNull(enumerable);
            return Join(enumerable, ", ");
        }

        public static string JoinWithNewLine(this IEnumerable<string> enumerable)
        {
            Check.Object.IsNotNull(enumerable);
            return Join(enumerable, Environment.NewLine);
        }

        public static string JoinWithComma(this Dictionary<string, string> dictionary)
        {
            Check.Object.IsNotNull(dictionary);
            return Join(dictionary, ", ");
        }

        public static string JoinWithNewLine(this Dictionary<string, string> dictionary)
        {
            Check.Object.IsNotNull(dictionary);
            return Join(dictionary, Environment.NewLine);
        }


        private static string Join(IEnumerable<string> enumerable, string separator)
        {
            Check.Object.IsNotNull(enumerable);
            Check.Object.IsNotNull(separator);
            return string.Join(separator, enumerable);
        }

        private static string Join(Dictionary<string, string> dictionary, string separator)
        {
            Check.Object.IsNotNull(dictionary);
            Check.Object.IsNotNull(separator);
            return string.Join(separator, dictionary.Select(pair => pair.Key + ": " + pair.Value));
        }
    }
}
