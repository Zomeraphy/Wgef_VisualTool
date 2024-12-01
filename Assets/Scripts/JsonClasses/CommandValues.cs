using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;

[Serializable]
public class BaseCommandValue
{
    
}

[Serializable]
public class SingleTargetCommandValue : BaseCommandValue
{
    public string target;
}

[Serializable]
public class TransportCommandValue : SingleTargetCommandValue
{
    public int[] pos;
}

[Serializable]
public class FadeCommandValue : SingleTargetCommandValue
{
    public float opacity;
    public float time_sec;
}

[Serializable]
public class ThroughCommandValue : SingleTargetCommandValue
{
    public bool isThrough;
}

[Serializable]
public class TypeCommandValue : BaseCommandValue
{
    public string text;
    [JsonProperty("fixed")]
    public bool _fixed;
    public int[] pos;
    public string[] tags;
    public bool has_se;
    public object char_type;
    public bool has_animation;
    [JsonConverter(typeof(StringEnumConverter))]
    public LayerType layer;
    public int z_index;
    public bool can_skip;
    public bool need_accept;
    public bool wait;
    public RefText[] refTexts;
}

[Serializable]
public class SplitMergeRuleCommandValue : BaseCommandValue
{
    public string[] from;
    public string to;
}

[Serializable]
public class SentenceRuleCommandValue : BaseCommandValue
{
    public string text;
    [JsonProperty("switch")]
    public string _switch;
    public bool has_hint;
    public bool has_animation;
    public object[] memory;
    public int progress;
    public int level;
}

[Serializable]
public class DeleteSentenceRuleCommandValue : BaseCommandValue
{
    public string text;
}

public class BgmCommandValue : BaseCommandValue
{
    public BgmType type;
}

public class SeCommandValue : BaseCommandValue
{
    public string type;
}

public class CameraCommandValue : BaseCommandValue
{
    public string parameter;
    public int add;
}