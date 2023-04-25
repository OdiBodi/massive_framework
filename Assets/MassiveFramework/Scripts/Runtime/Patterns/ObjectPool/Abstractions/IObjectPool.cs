namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface IObjectPool<T>
        where T : IPoolObject
    {
        int Capacity { get; }
        int Count { get; }

        T Request(string id = "", IPoolObjectArguments arguments = null);
        void Return(T obj);
    }
}
