using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MassiveCore.Framework
{
    public static class EnumerableExtensions
    {
        public static string ToString<T>(this IEnumerable<T> list, Func<T, string> argument)
        {
            var builder = new StringBuilder();
            if (!list.Any())
            {
                var first = list.First();
                builder.Append(argument(first));
                foreach (var element in list.Except(first))
                {
                    builder.Append($",{argument(element)}");
                }
            }
            return $"[{builder}]";
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> predicate)
        {
            foreach (var element in list)
            {
                predicate(element);
            }
        }

        public static T RandomElement<T>(this IEnumerable<T> list)
        {
            var index = Random.Range(0, list.Count());
            return list.ElementAt(index);
        }

        public static int IndexOf<T>(this IEnumerable<T> list, T value)
            where T : IEquatable<T>
        {
            return list.IndexOf(value, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this IEnumerable<T> list, T value, IEqualityComparer<T> comparer)
        {
            var index = 0;
            foreach (var item in list)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static IEnumerable<T> OfInterfaceComponent<T>(this IEnumerable<GameObject> list)
            where T : class
        {
            foreach (var item in list)
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
