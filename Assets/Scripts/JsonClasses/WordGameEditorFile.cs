using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

[Serializable]
public class WordGameEditorFile
{
    public string projectTitle;
    public Map[] maps;
    public string editingMapId;
    public ProjectInfo projectInfo;
}

[Serializable]
public class Map
{
    public string id;
    public string title;
    public JsonVector2Int stageSize;
    public Event[] events;
    public Variant[] variants;
    public Player player;
}

[Serializable]
public class ProjectInfo
{
    public string title;
    public string description;
    public string hint_1;
    public string hint_2;
    public string hint_3;
}

[Serializable]
public class Player
{
    public string text;
    public string color;
    public int opacity;
}

[Serializable]
[JsonConverter(typeof(EventConverter))]
public class Event
{
    public string id;
    public string type;
    public string name;
    public EventAttribute attribute;
    [JsonConverter(typeof(StringEnumConverter), true)]
    public PropertyId propertyId;
    public BaseProperty property;
}

[Serializable]
public class Variant
{
    public string id;
    [JsonConverter(typeof(StringEnumConverter), true)]
    public VariantType type;
    public string name;
    public object value;
    public BaseAttribute attribute;
}

[Serializable]
[JsonConverter(typeof(CommandConverter))]
public class Command
{
    [JsonConverter(typeof(StringEnumConverter))]
    public CommandType type;
    public BaseCommandValue value;
    public Option option;
    public string id;
    public int indent;
}

[Serializable]
public class Option
{
    public bool expend;
    public string color = "#808000";
}

[Serializable]
public class BaseMoveRoute
{
    public string type;
}

[Serializable]
public class RandomMoveRoute : BaseMoveRoute
{
    public int[] value;
}

[Serializable]
public class RefText
{
    public string id;
}

public class JsonVector2Int
{
    public int x;
    public int y;
}
