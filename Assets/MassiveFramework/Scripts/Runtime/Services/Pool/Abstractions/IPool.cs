namespace MassiveCore.Framework
{
    public interface IPool
    {
        T Request<T>(string id = "") where T : class, IPoolObject;

        void Return<T>(T obj) where T : class, IPoolObject;
        void ReturnAll<T>() where T : class, IPoolObject;
        void ReturnAll();

        void RemoveAll<T>() where T : class, IPoolObject;
    }
}
