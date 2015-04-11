using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    [Serializable]
    public class EncryptionException : Exception
    {
        public EncryptionException() { }
        public EncryptionException(string message) : base(message) { }
        public EncryptionException(string message, Exception inner) : base(message, inner) { }
        protected EncryptionException(
          Runtime.Serialization.SerializationInfo info,
          Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
