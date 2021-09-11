using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Converter
{
    internal class JsonStringGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString().Insert(8, "-").Insert(8 + 1 + 4, "-").Insert(8 + 1 + 4 + 1 + 4, "-").Insert(8 + 1 + 4 + 1 + 4 + 1 + 4, "-");
            return Guid.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().Replace("-", ""));
        }
    }
}