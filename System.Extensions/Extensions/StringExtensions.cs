using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class StringExtensions
    {

       

        public static string Base64Encoded(this string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decoded(this string value)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string FirstLetterToLower(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToLower();
        }

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

        private static string Join(Dictionary<string, string> dictionary,string separator)
        {
            Check.Object.IsNotNull(dictionary);
            Check.Object.IsNotNull(separator);
            return string.Join(separator, dictionary.Select(pair => pair.Key + ": " + pair.Value));
        }

        public static T ConvertTo<T>(this string value, T defaultValue = default(T))
        {
            Check.Object.IsNotNull(value);

            if (value != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
            }
            else
            {
                return defaultValue;
            }
        }

        public static object ConvertTo(this string value, Type type)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            object result = null;

            if (type.IsValueType)
            {
                result = Activator.CreateInstance(type);
            }


            if (!string.IsNullOrEmpty(value))
            {
                // we are not going to handle exception here
                // if you need SafeParse then you should create
                // another method specially for that.
                TypeConverter tc = TypeDescriptor.GetConverter(type);
                result = tc.ConvertFrom(value);
            }

            return result;
        }

    }
}

