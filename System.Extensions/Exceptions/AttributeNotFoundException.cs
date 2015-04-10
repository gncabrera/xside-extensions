using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    [Serializable]
    public class AttributeNotFoundException : Exception
    {
        public AttributeNotFoundException() { }
        public AttributeNotFoundException(string message) : base(message) { }
        public AttributeNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected AttributeNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
