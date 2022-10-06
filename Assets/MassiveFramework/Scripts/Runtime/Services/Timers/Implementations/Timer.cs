using System;
using UniRx;

namespace MassiveCore.Framework
{
    public class Timer : ITimer
    {
        private readonly DateTime _startTime;
        private readonly TimeSpan _duration;
 
        private readonly IDisposable _stream;

        public event Action OnTicked;
        public event Action OnCompleted;

        public Timer(TimeSpan duration)
        {
            _duration = duration;
            _startTime = DateTime.Now;
            _stream = Observable.Interval(TimeSpan.FromSeconds(1)).TakeWhile(_ => RemainingTime().TotalSeconds > 0)
                .Subscribe(_ => OnTicked?.Invoke(), () => OnCompleted?.Invoke());
        }

        public void Dispose()
        {
            OnCompleted?.Invoke();
            _stream?.Dispose();
        }

        public DateTime StartTime()
        {
            return _startTime;
        }

        public DateTime EndTime()
        {
            return Duration() == TimeSpan.MaxValue ? DateTime.MaxValue : StartTime() + Duration();
        }

        public TimeSpan Duration()
        {
            return _duration;
        }

        public TimeSpan ElapsedTime()
        {
            return DateTime.Now - StartTime();
        }

        public TimeSpan RemainingTime()
        {
            return EndTime() - DateTime.Now;
        }
    }
}
