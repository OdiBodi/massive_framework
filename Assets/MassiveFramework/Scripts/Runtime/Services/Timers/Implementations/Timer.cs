using System;
using UniRx;

namespace MassiveCore.Framework
{
    public class Timer : ITimer
    {
        private readonly DateTime startTime;
        private readonly TimeSpan duration;
        private readonly IObservable<long> observable;
        private readonly IDisposable subscription;
    
        public event Action<TimeSpan> OnTicked;
        public event Action OnCompleted;
    
        public Timer(IObservable<long> observable)
        {
            startTime = DateTime.Now;
            this.observable = observable;
            subscription = observable.Subscribe
            (
                x =>
                {
                    if (OnTicked == null)
                    {
                        return;
                    }
                    var passedTime = DateTime.Now - startTime;
                    OnTicked.Invoke(passedTime);
                },
                () => OnCompleted?.Invoke()
            );
        }
    
        public void Dispose()
        {
            OnCompleted?.Invoke();
            subscription?.Dispose();
        }

        public TimeSpan StartTime()
        {
            throw new NotImplementedException();
        }

        public TimeSpan Duration()
        {
            throw new NotImplementedException();
        }

        public TimeSpan ElapsedTime()
        {
            throw new NotImplementedException();
        }

        public TimeSpan RemainingTime()
        {
            throw new NotImplementedException();
        }
    }
}
