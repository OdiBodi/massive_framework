using System.Collections.Generic;
using System.Linq;

namespace MassiveCore.Framework.Runtime
{
    public class RepeatingList<T>
    {
        private readonly int _maxCount;
        private readonly int _maxRepeatCount;
        private readonly IEqualityComparer<T> _equalityComparer;

        private readonly Queue<T> _queue;

        public RepeatingList(int maxCount, int maxRepeatCount)
            : this(maxCount, maxRepeatCount, EqualityComparer<T>.Default)
        {
        }

        public RepeatingList(int maxCount, int maxRepeatCount, IEqualityComparer<T> equalityComparer)
        {
            _maxCount = maxCount;
            _maxRepeatCount = maxRepeatCount;
            _equalityComparer = equalityComparer;
            _queue = new Queue<T>(maxCount);
        }

        public bool AddItem(T item)
        {
            if (_queue.Count >= _maxCount)
            {
                _queue.Dequeue();
            }
            if (_queue.Count(i => _equalityComparer.Equals(i, item)) >= _maxRepeatCount)
            {
                return false;
            }
            _queue.Enqueue(item);
            return true;
        }

        public void Reset()
        {
            _queue.Clear();
        }
    }
}
