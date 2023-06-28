using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public class VisualEffectAbstractFactory : IAbstractFactory<VisualEffect>
    {
        private readonly VisualEffectPlaceholderFactory _placeholderFactory;

        public VisualEffectAbstractFactory(VisualEffectPlaceholderFactory placeholderFactory)
        {
            _placeholderFactory = placeholderFactory;
        }

        public VisualEffect Product(IAbstractFactoryArguments arguments)
        {
            var poolArguments = arguments as PoolAbstractFactoryArguments;
            var sound = _placeholderFactory.Create(poolArguments.Id);
            return sound;
        }
    }
}
