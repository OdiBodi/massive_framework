using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            serializer.Serialize(writer, value.r);
            writer.WritePropertyName("g");
            serializer.Serialize(writer, value.g);
            writer.WritePropertyName("b");
            serializer.Serialize(writer, value.b);
            writer.WritePropertyName("a");
            serializer.Serialize(writer, value.a);
            writer.WriteEndObject();
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            reader.Read();
            var r = serializer.Deserialize<float>(reader);
            reader.Read();
            var g = serializer.Deserialize<float>(reader);
            reader.Read();
            var b = serializer.Deserialize<float>(reader);
            reader.Read();
            var a = serializer.Deserialize<float>(reader);
            reader.Read();
            return new Color(r, g, b, a);
        }
    }
}
