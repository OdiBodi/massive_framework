using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MassiveCore.Framework.Runtime
{
    public class CustomJsonConverter<T> : JsonConverter
    {
        private class ContractResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                if (typeof(T).IsAssignableFrom(objectType))
                {
                    var contract = CreateObjectContract(objectType);
                    contract.Converter = null; // Prevent infinite recursion
                    return contract;
                }
                return base.CreateContract(objectType);
            }
        }

        private JsonSerializer DefaultSerializer
        {
            get
            {
                var resolver = new ContractResolver();
                var settings = new JsonSerializerSettings { ContractResolver = resolver };
                var serializer = JsonSerializer.CreateDefault(settings);
                return serializer;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = DefaultSerializer.Deserialize(reader, objectType);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DefaultSerializer.Serialize(writer, value);
        }
    }
}
