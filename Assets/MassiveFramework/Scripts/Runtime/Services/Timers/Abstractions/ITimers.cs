namespace MassiveCore.Framework
{
    public interface ITimers
    {
        void Start<T>(string id, params object[] arguments) where T : ITimer;
        void Stop(string id);
        ITimer TimerBy(string id);
    }
}
