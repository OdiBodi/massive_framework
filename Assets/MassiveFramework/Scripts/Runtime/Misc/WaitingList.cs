using System;
using System.Collections.Generic;
using UniRx;

namespace MassiveCore.Framework
{
    public class WaitingList<T>
    {
        private readonly Dictionary<T, IDisposable> _map;

        public WaitingList(int capacity = 2)
        {
            _map = new Dictionary<T, IDisposable>(capacity);
        }

        public bool Add(T type, float duration)
        {
            if (_map.ContainsKey(type))
            {
                return false;
            }
            _map[type] = Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe
            (
                _ => { },
                () => _map.Remove(type)
            );
            return true;
        }

        private void Reset()
        {
            _map.ForEach(item => item.Value.Dispose());
            _map.Clear();
        }
    }
}
