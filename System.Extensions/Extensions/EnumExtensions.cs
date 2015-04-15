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
}
