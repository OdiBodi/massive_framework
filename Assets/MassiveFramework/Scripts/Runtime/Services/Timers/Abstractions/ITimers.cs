using System;

namespace MassiveCore.Framework
{
    public interface ITimers
    {
        public void Start(string id, TimeSpan duration);
        public void Stop(string id);
        public void Subscribe(string id, Action<ITimer> onTick, Action onCompleted = null);
        public void Unsubscribe(string id, Action<ITimer> onTick, Action onCompleted = null);
        public ITimer Timer(string id);
    }
}
