using System;
using System.Collections.Generic;

namespace Framework
{
    public class ServiceLocator
    {
        private static ServiceLocator locator;

        public static ServiceLocator Instance => locator ??= new ServiceLocator();

        private readonly Dictionary<Type, object> _registry = new();

        public void Register<T>(T serviceInstance)
        {
            _registry[typeof(T)] = serviceInstance;
        }

        public void Unregister<T>(T serviceInstance)
        {
            if (_registry.ContainsKey(typeof(T)))
            {
                _registry.Remove(typeof(T));
            }
        }

        public T GetService<T>()
        {
            var serviceInstance = (T)_registry[typeof(T)];
            return serviceInstance;
        }
    }
}