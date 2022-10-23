namespace MassiveCore.Framework
{
    public interface ITimers
    {
        void Start<T>(string id, ITimerArguments arguments) where T : class, ITimer;
        void Stop(string id);
        ITimer TimerBy(string id);
    }
}
