using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MassiveCore.Framework.Runtime
{
    public static class EnumerableExtensions
    {
        public static bool EqualsTo<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
            where T : IComparable<T>
        {
            if (enumerable.Count() != other.Count())
            {
                return false;
            }
            return enumerable.OrderBy(x => x).SequenceEqual(other.OrderBy(x => x));
        }

        public static string ToString<T>(this IEnumerable<T> enumerable, Func<T, string> argument)
        {
            var builder = new StringBuilder();
            if (!enumerable.Any())
            {
                var first = enumerable.First();
                builder.Append(argument(first));
                foreach (var element in enumerable.Except(first))
                {
                    builder.Append($",{argument(element)}");
                }
            }
            return $"[{builder}]";
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> predicate)
        {
            foreach (var element in enumerable)
            {
                predicate(element);
            }
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var index = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable, int count)
        {
            var index = Random.Range(0, count);
            return enumerable.ElementAt(index);
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T value)
            where T : IEquatable<T>
        {
            return enumerable.IndexOf(value, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T value, IEqualityComparer<T> comparer)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static IEnumerable<T> OfInterfaceComponent<T>(this IEnumerable<GameObject> enumerable)
            where T : class
        {
            foreach (var item in enumerable)
            {
                var result = item.TryGetComponent<T>(out var component);
                if (result)
                {
                    yield return component;
                }
            }
        }
    }
}
