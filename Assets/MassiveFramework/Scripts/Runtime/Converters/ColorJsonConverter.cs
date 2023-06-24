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
            reader.Read();
            var r = (float)serializer.Deserialize<double>(reader);
            reader.Read();
            reader.Read();
            var g = (float)serializer.Deserialize<double>(reader);
            reader.Read();
            reader.Read();
            var b = (float)serializer.Deserialize<double>(reader);
            reader.Read();
            reader.Read();
            var a = (float)serializer.Deserialize<double>(reader);
            reader.Read();
            return new Color(r, g, b, a);
        }
    }
}
