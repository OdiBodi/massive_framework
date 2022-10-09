using System;

namespace MassiveCore.Framework
{
    public interface ITimer : IDisposable
    {
        event Action Ticked;
        event Action Completed;
        DateTime StartTime();
        DateTime EndTime();
        TimeSpan Duration();
        TimeSpan ElapsedTime();
        TimeSpan RemainingTime();
    }
}
