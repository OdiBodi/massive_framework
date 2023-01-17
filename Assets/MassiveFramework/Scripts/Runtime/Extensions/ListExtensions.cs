using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace MassiveCore.Framework.Runtime
{
    public static class ListExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> list, Action<T, T> onWillSwap = null)
        {
            var n = list.Count;
            while (n > 1) {
                var k = Random.Range(0, n) % n;
                n--;
                onWillSwap?.Invoke(list[k], list[n]);
                (list[k], list[n]) = (list[n], list[k]);
            }
            return list;
        }
    }
}
