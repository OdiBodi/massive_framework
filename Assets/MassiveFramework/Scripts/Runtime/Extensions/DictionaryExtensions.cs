using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MassiveCore.Framework.Runtime
{
    public static class DictionaryExtensions
    {
        public static string ToFormattedString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, string format = "\"{0}\":\"{1}\"\n")
        {
            var sb = new StringBuilder();
            foreach (var pair in dictionary)
            {
                var formattedText = string.Format(format, pair.Key, pair.Value);
                sb.Append(formattedText);
            }
            return sb.ToString();
        }

        public static Dictionary<TKey, int> Sum<TKey>(this IDictionary<TKey, int> dictionary, IDictionary<TKey, int> other)
        {
            var result = dictionary.Concat(other).GroupBy(kv => kv.Key)
                .Select(g => new KeyValuePair<TKey, int>(g.Key, g.Sum(kv => kv.Value)))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            return result;
        }

        public static Dictionary<TKey, TValue> Combine<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other)
        {
            return dictionary.Concat(other).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
