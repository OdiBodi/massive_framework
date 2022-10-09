using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework
{
    public class Timers : ITimers
    {
        [Inject]
        private readonly ILogger _logger;

        private readonly Dictionary<string, ITimer> _timers = new();

        public void Start<T>(string id, params object[] arguments)
            where T : ITimer
        {
            if (TimerBy(id) != null)
            {
                return;
            }

            var timer = Activator.CreateInstance(typeof(T), arguments) as ITimer;
            timer.Ticked += () =>
            {
                var elapsedTime = (int)timer.ElapsedTime().TotalSeconds;
                var duration = timer.Duration() == TimeSpan.MaxValue ? int.MaxValue : (int)timer.Duration().TotalSeconds;
                _logger.Print($"Timer[\"{id}\"] ticked: {elapsedTime}s < {duration}s");
            };
            timer.Completed += () =>
            {
                _timers.Remove(id);
                _logger.Print($"Timer[\"{id}\"] stopped!");
            };

            _timers.Add(id, timer);
            _logger.Print($"Timer[\"{id}\"] started!");
        }

        public void Stop(string id)
        {
            var timer = TimerBy(id);
            if (timer == null)
            {
                return;
            }

            timer.Dispose();
            _timers.Remove(id);

            _logger.Print($"Timer[\"{id}\"] stopped!");
        }

        public ITimer TimerBy(string id)
        {
            _timers.TryGetValue(id, out var timer);
            return timer;
        }
    }
}
