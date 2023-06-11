using System;
using Newtonsoft.Json;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public static class ReactiveExtensions
    {
        public static void Max(this ReactiveProperty<int> property, int value)
        {
            property.Value = Math.Max(value, property.Value);
        }

        public static void Min(this ReactiveProperty<int> property, int value)
        {
            property.Value = Math.Min(value, property.Value);
        }

        public static string SerializeToJson<T>(this ReactiveProperty<T> property)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ColorJsonConverter());
            var json = JsonConvert.SerializeObject(property, settings);
            return json;
        }

        public static void DeserializeFromJson<T>(this ReactiveProperty<T> property, string json)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ColorJsonConverter());
            var newProperty = JsonConvert.DeserializeObject<ReactiveProperty<T>>(json, settings);
            if (newProperty == null)
            {
                return;
            }
            property.Value = newProperty.Value;
        }

        public static string SerializeToJson<T>(this ReactiveCollection<T> property)
        {
            var json = JsonConvert.SerializeObject(property);
            return json;
        }

        public static void DeserializeFromJson<T>(this ReactiveCollection<T> property, string json)
        {
            var collection = JsonConvert.DeserializeObject<ReactiveCollection<T>>(json);
            if (collection == null)
            {
                return;
            }
            property.Clear();
            collection.ForEach(property.Add);
        }

        public static void ChangeAsStruct<T>(this ReactiveProperty<T> property, Func<T, T> change)
            where T : struct
        {
            property.Value = change(property.Value);
        }
    }
}
