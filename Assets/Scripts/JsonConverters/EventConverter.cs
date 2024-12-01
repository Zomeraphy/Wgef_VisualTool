using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class EventConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Event).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var _event = new Event();
        var root = JObject.Load(reader);
        var attribute = (JObject)root.SelectToken("attribute");
        _event.id = root.Value<string>("id");
        _event.type = root.Value<string>("type");
        _event.name = root.Value<string>("name");
        Enum.TryParse(root.Value<string>("propertyId"), out _event.propertyId);
        _event.attribute = _event.propertyId == PropertyId.animationCustomCommand ? new AnimationAttribute() : new EventAttribute();
        serializer.Populate(attribute.CreateReader(), _event.attribute);

        var prop = (JObject)root.SelectToken("property");
        BaseProperty property;
        switch (_event.propertyId)
        {
            case PropertyId.moveNPC:
            case PropertyId.staticNPC:
                property = new NPCProperty();
                break;
            case PropertyId.splitRule:
            case PropertyId.mergeRule:
                property = new SplitMergeProperty();
                break;
            case PropertyId.sentenceRule:
                property = new SentenceProperty();
                break;
            case PropertyId.typewriter:
                property = new TypeWriterProperty();
                break;
            case PropertyId.timer:
                property = new TimerProperty();
                break;
            case PropertyId.customCommand:
            case PropertyId.animationCustomCommand:
                property = new CommandProperty();
                break;
            case PropertyId.deleteSentenceRule:
            case PropertyId.addLoopLight:
            case PropertyId.clearTypewriter:
                property = new TargetProperty();
                break;
            case PropertyId.dissolver:
                property = new TargetIdProperty();
                break;
            case PropertyId.transporter:
                property = new TransportProperty();
                break;
            default:
                property = new BaseProperty();
                break;
        }

        serializer.Populate(prop.CreateReader(), property);
        _event.property = property;
        return _event;
    }


    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var _event = (Event)value;
        writer.WriteStartObject();
        writer.WritePropertyName("id");
        writer.WriteValue(_event.id);
        writer.WritePropertyName("type");
        writer.WriteValue(_event.type);
        writer.WritePropertyName("name");
        writer.WriteValue(_event.name);
        writer.WritePropertyName("attribute");
        serializer.Serialize(writer, _event.attribute);
        writer.WritePropertyName("propertyId");
        writer.WriteValue(_event.propertyId.ToString());
        writer.WritePropertyName("property");
        serializer.Serialize(writer, _event.property);
        writer.WriteEndObject();
    }
}

