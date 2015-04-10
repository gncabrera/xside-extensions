using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class AttributeMemberPair<AttributeType, MemberType>
    {
        public AttributeType Attribute { get; set; }
        public MemberType MemberInfo { get; set; }
    }
}
