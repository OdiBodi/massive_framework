using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public class SoundAbstractFactory : IAbstractFactory<Sound>
    {
        private readonly SoundPlaceholderFactory _placeholderFactory;

        public SoundAbstractFactory(SoundPlaceholderFactory placeholderFactory)
        {
            _placeholderFactory = placeholderFactory;
        }

        public Sound Product(IAbstractFactoryArguments arguments)
        {
            var poolArguments = arguments as PoolAbstractFactoryArguments;
            var sound = _placeholderFactory.Create(poolArguments.Id);
            return sound;
        }
    }
}
