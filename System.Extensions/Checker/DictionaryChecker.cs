using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public class DictionaryChecker
    {
        private const string DICTIONARY_KEY_NOT_EXISTS = "The dictionary does not contains the key [{0}]";
        public void HasKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            Check.Object.IsNotNull(dictionary);
            Check.Object.IsNotNull(key);

            if (!dictionary.ContainsKey(key))
                Check.ThrowArgumentException(string.Format(DICTIONARY_KEY_NOT_EXISTS, key));
        }


         public void HasKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, string paramName, string message = null)
        {
            Check.Object.IsNotNull(dictionary);
            Check.Object.IsNotNull(key);

            if (!dictionary.ContainsKey(key))
                Check.ThrowArgumentException(message, string.Format(DICTIONARY_KEY_NOT_EXISTS, key), paramName);
        }
    }
}
