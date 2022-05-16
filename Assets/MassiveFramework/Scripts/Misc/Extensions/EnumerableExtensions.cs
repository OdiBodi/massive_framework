using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using Random = UnityEngine.Random;

namespace MassiveCore.Framework
{
    public static class EnumerableExtensions
    {
        public static string ToString<T>(this IEnumerable<T> array, Func<T, string> argument)
        {
            var builder = new StringBuilder();
            if (array.Any())
            {
                var first = array.First();
                builder.Append(argument(first));
                foreach (var element in array.Except(first))
                {
                    builder.Append($",{argument(element)}");
                }
            }
            return $"[{builder}]";
        }

        public static void ForEach<T>(this IEnumerable<T> array, Action<T> predicate)
        {
            foreach (var element in array)
            {
                predicate(element);
            }
        }

        public static T RandomElement<T>(this IEnumerable<T> list)
        {
            var index = Random.Range(0, list.Count());
            return list.ElementAt(index);
        }
    }
}
