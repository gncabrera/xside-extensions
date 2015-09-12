using EnsureThat;
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
            Ensure.That(enumerable).IsNotNull();
            return Join(enumerable, ", ");
        }

        public static string JoinWithNewLine(this IEnumerable<string> enumerable)
        {
            Ensure.That(enumerable).IsNotNull();
            return Join(enumerable, Environment.NewLine);
        }

        public static string JoinWithComma(this Dictionary<string, string> dictionary)
        {
            Ensure.That(dictionary).IsNotNull();
            return Join(dictionary, ", ");
        }

        public static string JoinWithNewLine(this Dictionary<string, string> dictionary)
        {
            Ensure.That(dictionary).IsNotNull();
            return Join(dictionary, Environment.NewLine);
        }


        private static string Join(IEnumerable<string> enumerable, string separator)
        {
            Ensure.That(enumerable).IsNotNull();
            Ensure.That(separator).IsNotNull();
            return string.Join(separator, enumerable);
        }

        private static string Join(Dictionary<string, string> dictionary, string separator)
        {
            Ensure.That(dictionary).IsNotNull();
            Ensure.That(separator).IsNotNull();
            return string.Join(separator, dictionary.Select(pair => pair.Key + ": " + pair.Value));
        }
    }
}
