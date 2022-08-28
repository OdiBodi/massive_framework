using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefs : IProfile
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();
        private readonly Dictionary<Type, IProfileValueHandler> valueHandlers = new Dictionary<Type, IProfileValueHandler>();

        private bool loaded;

        public ProfilePrefs()
        {
            InitDefaultValueHandlers();
        }

        public ReactiveProperty<T> Property<T>(string id, T defaultValue = default)
        {
            if (!values.TryGetValue(id, out var value))
            {
                value = new ReactiveProperty<T>(defaultValue);
                values[id] = value;
            }
            return (ReactiveProperty<T>)value;
        }

        public ReactiveCollection<T> Collection<T>(string id, T[] defaultValue = default)
        {
            if (!values.TryGetValue(id, out var value))
            {
                value = defaultValue == default ? new ReactiveCollection<T>() : new ReactiveCollection<T>(defaultValue);
                values[id] = value;
            }
            return (ReactiveCollection<T>)value;
        }

        public void Sync()
        {
            if (!loaded)
            {
                loaded = true;
                Load();
            }
            Save();
        }

        public void BindPropertyHandler<T>(IProfileValueHandler handler)
        {
            var type = typeof(ReactiveProperty<T>);
            valueHandlers[type] = handler;
        }

        public void BindCollectionHandler<T>(IProfileValueHandler handler)
        {
            var type = typeof(ReactiveCollection<T>);
            valueHandlers[type] = handler;
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

        private void InitDefaultValueHandlers()
        {
            InitDefaultPropertyHandlers();
            InitDefaultCollectionsHandlers();
        }

        private void InitDefaultPropertyHandlers()
        {
            BindPropertyHandler<bool>(new ProfilePrefsReactiveBoolHandler());
            BindPropertyHandler<int>(new ProfilePrefsReactiveIntHandler());
            BindPropertyHandler<float>(new ProfilePrefsReactiveFloatHandler());
            BindPropertyHandler<string>(new ProfilePrefsReactiveStringHandler());
            BindPropertyHandler<DateTime>(new ProfilePrefsReactiveDateTimeHandler());
        }

        private void InitDefaultCollectionsHandlers()
        {
            BindCollectionHandler<bool>(new ProfilePrefsReactiveCollectionHandler<bool>());
            BindCollectionHandler<int>(new ProfilePrefsReactiveCollectionHandler<int>());
            BindCollectionHandler<float>(new ProfilePrefsReactiveCollectionHandler<float>());
            BindCollectionHandler<string>(new ProfilePrefsReactiveCollectionHandler<string>());
            BindCollectionHandler<DateTime>(new ProfilePrefsReactiveCollectionHandler<DateTime>());
        }

        private void IterateValueHandlerForValues(Action<IProfileValueHandler, string, object> onHandle)
        {
            foreach (var pair in values)
            {
                var id = pair.Key;
                var value = pair.Value;
                var valueType = value.GetType();
                var handler = valueHandlers[valueType];
                onHandle(handler, id, value);
            }
        }
    }
}
