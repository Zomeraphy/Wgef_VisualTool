using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

public class CommandConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Command).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var command = new Command();
        var root = JObject.Load(reader);
        command.id = root.Value<string>("id");
        command.option = new Option();
        command.type = (CommandType)serializer.Deserialize(root["type"].CreateReader(), typeof(CommandType));

        var value = root.SelectToken("value") as JObject;
        if (value == null) return command;
        BaseCommandValue commandValue;
        switch (command.type)
        {
            case CommandType.callback_trigger:
                commandValue = new SingleTargetCommandValue();
                break;
            case CommandType.append_sentence_rule:
                commandValue = new SentenceRuleCommandValue();
                break;
            default:
                commandValue = new BaseCommandValue();
                break;
        }

        serializer.Populate(value.CreateReader(), commandValue);
        command.value = commandValue;
        return command;
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
