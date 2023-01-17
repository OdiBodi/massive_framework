using System;

namespace MassiveCore.Framework.Runtime
{
    public interface ITimer : IDisposable
    {
        event Action Ticked;
        event Action Completed;
        ITimerArguments Arguments { get; }
        DateTime StartTime();
        DateTime EndTime();
        TimeSpan Duration();
        TimeSpan ElapsedTime();
        TimeSpan RemainingTime();
    }
}
