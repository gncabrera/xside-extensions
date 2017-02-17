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
