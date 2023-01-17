using System;

namespace MassiveCore.Framework.Runtime
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
