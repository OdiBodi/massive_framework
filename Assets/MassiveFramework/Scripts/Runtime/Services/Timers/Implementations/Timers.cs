using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class Timers
    {
        private class Timer : IDisposable
        {
            private readonly DateTime startTime;
            private readonly IObservable<long> observable;
            private readonly IDisposable subscription;

            public event Action<TimeSpan> OnTick;
            public event Action OnCompleted;

            public Timer(IObservable<long> observable)
            {
                startTime = DateTime.Now;
                this.observable = observable;
                subscription = observable.Subscribe
                (
                    x =>
                    {
                        if (OnTick == null)
                        {
                            return;
                        }
                        var passedTime = DateTime.Now - startTime;
                        OnTick.Invoke(passedTime);
                    },
                    () => OnCompleted?.Invoke()
                );
            }

            public void Dispose()
            {
                OnCompleted?.Invoke();
                subscription?.Dispose();
            }
        }

        [Inject]
        private readonly ILogger logger;
        
        private readonly Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

        public void Start(string id, int time)
        {
            if (TimerBy(id) != null || time == 0)
            {
                return;
            }

            var stream = Observable.Interval(TimeSpan.FromSeconds(1));
            if (time > 0)
            {
                stream = stream.TakeWhile(x =>
                {
                    var currentTime = x + 1;
                    logger.Print($"Timer[\"{id}\"] tick: {currentTime} < {time}");
                    return currentTime < time;
                });
            }

            var timer = new Timer(stream);
            timer.OnCompleted += () => timers.Remove(id);

            timers.Add(id, timer);
            logger.Print($"Timer[\"{id}\"] start {time}");
        }

        public void Stop(string id)
        {
            var timer = TimerBy(id);
            if (timer == null)
            {
                return;
            }

            timer.Dispose();
            timers.Remove(id);

            logger.Print($"Timer[\"{id}\"] stop");
        }

        public void Subscribe(string id, Action<TimeSpan> onTick, Action onCompleted = null)
        {
            var timer = TimerBy(id);
            if (timer == null)
            {
                return;
            }
            if (onTick != null)
            {
                timer.OnTick += onTick;
            }
            if (onCompleted != null)
            {
                timer.OnCompleted += onCompleted;
            }
        }

        public void Unsubscribe(string id, Action<TimeSpan> onTick, Action onCompleted = null)
        {
            var timer = TimerBy(id);
            if (timer == null)
            {
                return;
            }
            if (onTick != null)
            {
                timer.OnTick -= onTick;
            }
            if (onCompleted != null)
            {
                timer.OnCompleted -= onCompleted;
            }
        }

        public bool Contains(string id)
        {
            return timers.ContainsKey(id);
        }

        private Timer TimerBy(string id)
        {
            timers.TryGetValue(id, out var timer);
            return timer;
        }
    }
}
