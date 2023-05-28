using System;
using System.Collections.Generic;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class WaitingList<T>
    {
        private readonly Dictionary<T, IDisposable> _map;

        public WaitingList(int capacity = 2)
        {
            _map = new Dictionary<T, IDisposable>(capacity);
        }

        public bool AddItem(T item, float duration)
        {
            if (_map.ContainsKey(item))
            {
                return false;
            }
            _map[item] = Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe
            (
                _ => { },
                () => _map.Remove(item)
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
