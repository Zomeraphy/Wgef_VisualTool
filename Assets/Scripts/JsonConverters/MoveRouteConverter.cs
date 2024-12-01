using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class MoveRouteConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(BaseMoveRoute).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JArray routes = JArray.Load(reader);
        if (routes.Count == 0) return new BaseMoveRoute[0];
        foreach (var route in routes)
        {

        }
        string type = routes["type"]?.ToString();
        BaseMoveRoute result;

        switch (type)
        {
            case "@[move_random]":
                result = new RandomMoveRoute();
                break;
            default:
                result = new BaseMoveRoute();
                break;
        }

        serializer.Populate(routes.CreateReader(), result);
        return result;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.FromObject(value, serializer);
        jsonObject.WriteTo(writer);
    }
}
