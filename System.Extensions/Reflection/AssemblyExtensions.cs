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
