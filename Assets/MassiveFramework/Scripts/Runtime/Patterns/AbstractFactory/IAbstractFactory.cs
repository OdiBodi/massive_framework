namespace MassiveCore.Framework.Runtime.Patterns
{
    public interface IAbstractFactory<out T>
        where T : IAbstractProduct
    {
        T Product(IAbstractFactoryArguments arguments);
    }
}
