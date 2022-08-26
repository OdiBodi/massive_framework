using System;
using System.Collections.Generic;
using UniRx;

namespace MassiveCore.Framework
{
    public class WaitingList<T>
    {
        private readonly Dictionary<T, IDisposable> map;

        public WaitingList(int capacity = 2)
        {
            map = new Dictionary<T, IDisposable>(capacity);
        }

        public bool Add(T type, float duration)
        {
            if (map.ContainsKey(type))
            {
                return false;
            }
            map[type] = Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe
            (
                _ => { },
                () => map.Remove(type)
            );
            return true;
        }

        private void Reset()
        {
            map.ForEach(item => item.Value.Dispose());
            map.Clear();
        }
    }
}
