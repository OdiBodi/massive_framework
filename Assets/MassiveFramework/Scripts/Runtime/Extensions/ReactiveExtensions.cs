using System;
using Newtonsoft.Json;
using UniRx;

namespace MassiveCore.Framework
{
    public static class ReactiveExtensions
    {
        public static string SerializeToJson<T>(this ReactiveCollection<T> property)
        {
            var json = JsonConvert.SerializeObject(property);
            return json;
        }

        public static void DeserializeFromJson<T>(this ReactiveCollection<T> property, string json)
        {
            var collection = JsonConvert.DeserializeObject<ReactiveCollection<T>>(json);
            property.Clear();
            collection.ForEach(property.Add);
        }

        public static void Max(this ReactiveProperty<int> property, int value)
        {
            property.Value = Math.Max(value, property.Value);
        }

        public static void Min(this ReactiveProperty<int> property, int value)
        {
            property.Value = Math.Min(value, property.Value);
        }
    }
}
