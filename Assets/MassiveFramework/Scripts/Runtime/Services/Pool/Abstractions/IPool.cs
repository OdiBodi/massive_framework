using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public interface IPool
    {
        T Request<T>(string id = "") where T : class, IPoolObject;

        void Return<T>(T obj) where T : class, IPoolObject;
    }
}
