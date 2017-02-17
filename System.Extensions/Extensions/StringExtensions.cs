/*
  	Copyright 2017 Gustavo Cabrera
    Licensed under the Apache License, Version 2.0 (the "License");
 	you may not use this file except in compliance with the License.
 	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 */
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

        /// <summary>
        /// Encodes the string to a base64 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base64Encoded(this string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes the base64 string to a plain string
        /// </summary>
        public static string Base64Decoded(this string value)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Converts the string to the specified type
        /// </summary>
        public static T ConvertTo<T>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
        }

        /// <summary>
        /// Converts the string to the specified type. If there is any error, the default value will be returned
        /// </summary>
        public static T ConvertTo<T>(this string value, T defaultValue)
        {
            try
            {
                return ConvertTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Converts the string to the specified type
        /// </summary>
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

        /// <summary>
        /// Converts the string to the specified type. If there is any error, the default value will be returned
        /// </summary>
        public static object ConvertTo(this string value, Type type, object defaultValue)
        {
            try
            {
                return ConvertTo(value, type);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Simple way to call a string.Format(str, args)
        /// </summary>
        public static string Inject(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        /// Simple way to call a string.Format(str, args)
        /// </summary>
        public static string Inject(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }
    }
}

