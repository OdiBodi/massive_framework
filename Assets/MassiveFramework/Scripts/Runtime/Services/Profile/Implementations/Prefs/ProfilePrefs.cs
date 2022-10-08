using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefs : IProfile
    {
        private readonly Dictionary<string, object> _values = new();
        private readonly Dictionary<Type, IProfileValueHandler> _valueHandlers = new();

        private bool _loaded;

        public event Action OnPreLoading;
        public event Action OnPostLoading;
        public event Action OnPreSaving;
        public event Action OnPostSaving;

        public ProfilePrefs()
        {
            InitializeDefaultValueHandlers();
        }

        public ReactiveProperty<T> Property<T>(string id, T defaultValue = default)
        {
            if (!_values.TryGetValue(id, out var value))
            {
                value = new ReactiveProperty<T>(defaultValue);
                _values[id] = value;
            }
            return (ReactiveProperty<T>)value;
        }

        public ReactiveCollection<T> Collection<T>(string id, T[] defaultValue = default)
        {
            if (!_values.TryGetValue(id, out var value))
            {
                value = defaultValue == default ? new ReactiveCollection<T>() : new ReactiveCollection<T>(defaultValue);
                _values[id] = value;
            }
            return (ReactiveCollection<T>)value;
        }

        public async UniTask<bool> Synchronize()
        {
            if (!_loaded)
            {
                OnPreLoading?.Invoke();
                Load();
                _loaded = true;
                OnPostLoading?.Invoke();
            }
            OnPreSaving?.Invoke();
            Save();
            OnPostSaving?.Invoke();
            return true;
        }

        public void BindPropertyHandler<T>(IProfileValueHandler handler)
        {
            var type = typeof(ReactiveProperty<T>);
            _valueHandlers[type] = handler;
        }

        public void BindCollectionHandler<T>(IProfileValueHandler handler)
        {
            var type = typeof(ReactiveCollection<T>);
            _valueHandlers[type] = handler;
        }

        protected virtual void Load()
        {
            IterateValueHandlerForValues((handler, id, value) => handler.Load(id, value));
        }

        protected virtual void Save()
        {
            IterateValueHandlerForValues((handler, id, value) => handler.Save(id, value));
            PlayerPrefs.Save();
        }

        private void InitializeDefaultValueHandlers()
        {
            InitializeDefaultPropertyHandlers();
            InitializeDefaultCollectionsHandlers();
        }

        private void InitializeDefaultPropertyHandlers()
        {
            BindPropertyHandler<bool>(new ProfilePrefsReactiveBoolHandler());
            BindPropertyHandler<int>(new ProfilePrefsReactiveIntHandler());
            BindPropertyHandler<float>(new ProfilePrefsReactiveFloatHandler());
            BindPropertyHandler<string>(new ProfilePrefsReactiveStringHandler());
            BindPropertyHandler<DateTime>(new ProfilePrefsReactiveDateTimeHandler());
        }

        private void InitializeDefaultCollectionsHandlers()
        {
            BindCollectionHandler<bool>(new ProfilePrefsReactiveCollectionHandler<bool>());
            BindCollectionHandler<int>(new ProfilePrefsReactiveCollectionHandler<int>());
            BindCollectionHandler<float>(new ProfilePrefsReactiveCollectionHandler<float>());
            BindCollectionHandler<string>(new ProfilePrefsReactiveCollectionHandler<string>());
            BindCollectionHandler<DateTime>(new ProfilePrefsReactiveCollectionHandler<DateTime>());
        }

        private void IterateValueHandlerForValues(Action<IProfileValueHandler, string, object> onHandle)
        {
            foreach (var (id, value) in _values)
            {
                var valueType = value.GetType();
                var handler = _valueHandlers[valueType];
                onHandle(handler, id, value);
            }
        }
    }
}
