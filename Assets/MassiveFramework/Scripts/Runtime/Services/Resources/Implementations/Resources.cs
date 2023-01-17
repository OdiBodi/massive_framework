using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Resources : IResources
    {
        [Inject]
        private readonly ILogger _logger;

        private readonly Dictionary<Type, IResource> _resources = new();

        public void BindResource<T>(T resource)
            where T : class, IResource
        {
            if (Resource<T>() != null)
            {
                throw new Exception($"Resource \"{typeof(T)}\" was added!");
            }
            _resources[typeof(T)] = resource;
            _logger.Print($"Resource \"{typeof(T)}\" added!");
        }

        public T Resource<T>()
            where T : class, IResource
        {
            var result = _resources.TryGetValue(typeof(T), out var state);
            if (!result)
            {
                return default;
            }
            return state as T;
        }
    }
}
