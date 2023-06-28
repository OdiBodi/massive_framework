namespace MassiveCore.Framework.Runtime.Patterns
{
    public class PoolAbstractFactoryArguments : IAbstractFactoryArguments
    {
        public PoolAbstractFactoryArguments(string id, IAbstractFactoryArguments factoryArguments)
        {
            Id = id;
            FactoryArguments = factoryArguments;
        }

        public string Id { get; private set; }
        public IAbstractFactoryArguments FactoryArguments { get; private set; }
    }
}
