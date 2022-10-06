using System;

namespace MassiveCore.Framework
{
    public interface ITimer : IDisposable
    {
        event Action OnTicked;
        event Action OnCompleted;
        DateTime StartTime();
        DateTime EndTime();
        TimeSpan Duration();
        TimeSpan ElapsedTime();
        TimeSpan RemainingTime();
    }
}
