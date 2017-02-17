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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(T)));
        }

        public static string AssemblyDirectory(this Assembly assembly)
        {
            var path = assembly.CodeBaseString();
            return Path.GetDirectoryName(path);
        }

        private static string CodeBaseString(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }
    }
}
