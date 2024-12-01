using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;

[Serializable]
public class BaseAttribute
{
    [JsonProperty(Order = -5)]
    public JsonVector2Int pos;
    [JsonProperty(Order = -2)]
    public string text;
}

[Serializable]
public class EventAttribute : BaseAttribute
{
    public string textColor;
    public bool hasBackground;
    public float opacity;
    public string layer;
    public bool canPush;
    public bool canDelete;
    public bool canSplit;
    [JsonConverter(typeof(StringEnumConverter), true)]
    public EventTriggerType eventTriggerAction;
    public string existCondition;
    public int zIndex;
    public Command[] command;
    [JsonProperty("#loopMoveRoute")]
    public object[] loopMoveRoute;
}

[Serializable]
public class AnimationAttribute : EventAttribute
{
    [JsonProperty(Order = 10)]
    public AnimationType animationType;
}
