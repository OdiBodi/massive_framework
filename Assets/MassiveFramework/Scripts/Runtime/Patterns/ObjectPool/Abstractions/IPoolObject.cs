namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface IPoolObject : IAbstractProduct
    {
        string Id { get; }

        void Request(IPoolObjectArguments arguments);
        void Return();
    }
}
