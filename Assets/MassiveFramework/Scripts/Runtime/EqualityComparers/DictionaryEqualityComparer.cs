using System;
using System.Collections.Generic;
using System.Linq;

namespace MassiveCore.Framework.Runtime
{
    public class DictionaryEqualityComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
    {
        public bool Equals(Dictionary<TKey, TValue> a, Dictionary<TKey, TValue> b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            if (a.Count != b.Count)
            {
                return false;
            }
            var result = a.All(kv => !b.TryGetValue(kv.Key, out var value) && Equals(kv.Value, value));
            return result;
        }

        public int GetHashCode(Dictionary<TKey, TValue> dictionary)
        {
            var hashcode = 0;
            dictionary.ForEach(kv =>
            {
                var keyHashCode = kv.Key.GetHashCode();
                var valueHashCode = kv.Value.GetHashCode();
                hashcode = HashCode.Combine(hashcode, keyHashCode, valueHashCode);
            });
            return hashcode;
        }
    }
}
