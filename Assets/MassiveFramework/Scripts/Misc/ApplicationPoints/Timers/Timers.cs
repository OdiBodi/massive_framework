using System;
using System.Collections.Generic;
using UniRx;

namespace MassiveCore.Framework
{
    public class Timers
    {
        private Dictionary<string, IDisposable> timers = new Dictionary<string, IDisposable>();

        public void Start(string id, int time, Action<int> onTick)
        {
            if (!timers.ContainsKey(id) && time != 0)
            {
                var stream = Observable.Interval(TimeSpan.FromSeconds(1));
                if (time > 0)
                {
                    stream = stream.TakeWhile(x => x < time);
                }
                var disposable = stream.Subscribe(x => onTick.Invoke((int)x), () => timers.Remove(id));
                timers.Add(id, disposable);
            }
        }

        public void Stop(string id)
        {
            if (timers.TryGetValue(id, out var timer))
            {
                timer.Dispose();
                timers.Remove(id);
            }
        }
    }
}
