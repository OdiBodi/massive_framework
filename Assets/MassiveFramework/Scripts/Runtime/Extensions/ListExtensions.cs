using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace MassiveCore.Framework.Runtime
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Action<T, T> onWillSwap = null)
        {
            var n = list.Count;
            while (n > 1)
            {
                var k = Random.Range(0, n) % n;
                n--;
                onWillSwap?.Invoke(list[k], list[n]);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static (List<T> List, int Columns) RotatedMatrix90<T>(this IList<T> list, int columns)
        {
            var rows = list.Count / columns;
            var result = new List<T>(new T[columns * rows]);
            for (var i = rows - 1; i >= 0; i--)
            {
                for (var j = 0; j < columns; j++)
                {
                    var index = (rows - i - 1) + j * rows;
                    result[index] = list[i * columns + j];
                }
            }
            return (result, rows);
        }
        
        public static (List<T> List, int Columns) RotatedMatrix180<T>(this IList<T> list, int columns)
        {
            var rows = list.Count / columns;
            var result = new List<T>(new T[columns * rows]);
            for (var i = rows - 1; i >= 0; i--)
            {
                for (var j = columns - 1; j >= 0; j--)
                {
                    var index = (rows - i - 1) * columns + (columns - j - 1);
                    result[index] = list[i * columns + j];
                }
            }
            return (result, columns);
        }
        
        public static (List<T> List, int Columns) RotatedMatrix270<T>(this IList<T> list, int columns)
        {
            var rows = list.Count / columns;
            var result = new List<T>(new T[columns * rows]);
            for (var i = 0; i < rows; i++)
            {
                for (var j = columns - 1; j >= 0; j--)
                {
                    var index = i + (columns - j - 1) * rows;
                    result[index] = list[i * columns + j];
                }
            }
            return (result, rows);
        }
    }
}
