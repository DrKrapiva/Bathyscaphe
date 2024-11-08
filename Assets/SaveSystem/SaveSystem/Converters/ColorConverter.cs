using UnityEngine;
using System;
using Newtonsoft.Json;

public class ColorConverter : JsonConverter
{
    private readonly Type type;
    public ColorConverter(Type type)
    {
        this.type = type;
    }

    public override bool CanConvert(Type objectType)
    {
        return type == objectType;
    }

    public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    /*public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null; // Проверка на null для корректной обработки пустого значения
        }

        // Парсим объект JObject для извлечения полей цвета
        var obj = Newtonsoft.Json.Linq.JObject.Load(reader);

        // Проверяем наличие и извлекаем значения R, G, B и A
        float r = (float)(obj["R"] ?? 0);
        float g = (float)(obj["G"] ?? 0);
        float b = (float)(obj["B"] ?? 0);
        float a = (float)(obj["A"] ?? 1);  // Если альфа не указана, по умолчанию 1 (полностью непрозрачный)

        // Возвращаем объект Color с прочитанными значениями
        return new Color(r, g, b, a);
    }
    /*public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return null;
    }*/

    /*public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value.GetType() == type)
        {
            var color = (Color)value;
            writer.WriteStartObject();
            writer.WritePropertyName("R");
            writer.WriteValue(color.r);
            writer.WritePropertyName("G");
            writer.WriteValue(color.g);
            writer.WritePropertyName("B");
            writer.WriteValue(color.b);
            writer.WritePropertyName("A");
            writer.WriteValue(color.a);
            writer.WriteEndObject();
        }
    }*/

    public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
