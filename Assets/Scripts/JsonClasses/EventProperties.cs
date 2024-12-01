using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;

[Serializable]
public class BaseProperty
{
    [JsonProperty(Order = 10)]
    public string callbackId;
}

[Serializable]
public class NPCProperty : BaseProperty
{
    [JsonProperty(Order = -6)]
    public string text;
    [JsonProperty(Order = -4)]
    public string tag;
    [JsonProperty(Order = -2)]
    public int[] pos;
}

[Serializable]
public class SplitMergeProperty : BaseProperty
{
    public string eventIdA;
    public string eventIdB;
    public string eventIdC;
}

[Serializable]
public class SentenceProperty : BaseProperty
{
    public string text;
    public string finishId;
    public int level;
}

[Serializable]
public class TargetProperty : BaseProperty
{
    [JsonProperty(Order = -5)]
    public string target;
}

[Serializable]
public class TargetIdProperty : BaseProperty
{
    [JsonProperty(Order = -5)]
    public string targetEventId;
}

[Serializable]
public class TransportProperty : TargetProperty
{
    public string posTarget;
}

[Serializable]
public class TypeWriterProperty : NPCProperty
{
    [JsonConverter(typeof(StringEnumConverter))]
    public LayerType layer;
    public bool needAccept;
    public bool clearAfterComplete;
    [JsonProperty(Order = 12)]
    public RefText[] refTexts;
    [JsonProperty(Order = 14)]
    public bool has_se;
    [JsonProperty(Order = 16)]
    public object char_type;
}

[Serializable]
public class CommandProperty : BaseProperty
{
    public Command[] commands;
}

[Serializable]
public class TimerProperty : BaseProperty
{
    public float delayTime;
}
