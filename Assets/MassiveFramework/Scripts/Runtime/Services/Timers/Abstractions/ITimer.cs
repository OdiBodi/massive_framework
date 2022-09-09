using System;

namespace MassiveCore.Framework
{
    public interface ITimer : IDisposable
    {
        public TimeSpan StartTime();
        public TimeSpan Duration();
        public TimeSpan ElapsedTime();
        public TimeSpan RemainingTime();
    }
}
