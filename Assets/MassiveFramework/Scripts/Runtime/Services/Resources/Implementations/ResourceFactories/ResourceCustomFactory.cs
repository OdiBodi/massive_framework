using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class ResourceCustomFactory : IFactory<Type, IResource>
    {
        private readonly DiContainer _diContainer;

        public ResourceCustomFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public IResource Create(Type type)
        {
            var state = _diContainer.Instantiate(type) as IResource; 
            return state;
        }
    }
}
