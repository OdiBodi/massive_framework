using System.Collections.Generic;
using System.Text;

namespace MassiveCore.Framework
{
    public static class DictionaryExtensions
    {
        public static string ToFormattedString<K, V>(this IDictionary<K, V> dictionary, string format = "{0}:{1}\n")
        {
            var sb = new StringBuilder();
            foreach (var pair in dictionary)
            {
                var formattedText = string.Format(format, pair.Key, pair.Value);
                sb.Append(formattedText);
            }
            return sb.ToString();
        }
    }
}
