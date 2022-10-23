using System;

namespace MassiveCore.Framework
{
    public class TimerArguments : ITimerArguments
    {
        public TimerArguments(TimeSpan duration)
        {
            Duration = duration;
        }

        public TimeSpan Duration { get; }
    }
}
