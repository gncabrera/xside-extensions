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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            FieldInfo fi = EnumValue.GetType().GetField(EnumValue.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0 && !string.IsNullOrWhiteSpace(attributes[0].Description))
                return attributes[0].Description;
            else
                return EnumValue.ToString();
        }


    }

    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : struct
        {
            // Can't use type constraints on value types, so have to do check like this
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
