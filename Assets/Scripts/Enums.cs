using Newtonsoft.Json;
using System.Runtime.Serialization;

public enum PropertyId
{
    none,
    dummy,
    moveNPC,
    staticNPC,
    #region Not Generate
    deletableEvent,
    pushableEvent,
    splitableEvent,
    splitedEvent,
    splitRule,
    mergeableEvent,
    mergedEvent,
    mergeRule,
    #endregion
    sentenceRule,
    deleteSentenceRule,
    loopLightEvent,
    placeholderEvent,
    addLoopLight,
    dissolver,
    typewriter,
    clearTypewriter,
    bell,
    timer,
    transporter,
    customCommand,
    animationCustomCommand
}

public enum EventTriggerType
{
    auto,
    press,
    touch,
    trigger
}

public enum CommandType
{
    #region 物件操作
    [EnumMember(Value = "#transport_event:interaction_block")]
    transport_event_interaction_block,
    [EnumMember(Value = "#dissolve_event")]
    dissolve_event,
    [EnumMember(Value = "#callback_trigger")]
    callback_trigger,
    [EnumMember(Value = "#play_animation")]
    play_animation,
    [EnumMember(Value = "#event_fade_to")]
    event_fade_to,
    [EnumMember(Value = "#move_up")]
    move_up,
    [EnumMember(Value = "#move_down")]
    move_down,
    [EnumMember(Value = "#move_left")]
    move_left,
    [EnumMember(Value = "#move_right")]
    move_right,
    [EnumMember(Value = "#move_to_point")]
    move_to_point,
    [EnumMember(Value = "#set_through")]
    set_through,
    #endregion

    #region 打字機
    [EnumMember(Value = "#type_parallel")]
    type_parallel,
    clear_typed,
    #endregion

    #region 能力設定
    set_split_power,
    set_backspace_power,
    set_push_power,
    set_ctrl_z_power,
    #endregion

    #region 遊戲機制
    [EnumMember(Value = "#append_split_rule")]
    append_split_rule,
    [EnumMember(Value = "#append_merge_rule")]
    append_merge_rule,
    [EnumMember(Value = "#append_sentence_rule")]
    append_sentence_rule,
    [EnumMember(Value = "#delete_sentence_rule")]
    delete_sentence_rule,
    #endregion

    #region 點唱機
    editor_play_bgm,
    editor_stop_bgm,
    editor_play_se,
    #endregion

    #region 攝影機
    set_camera_follow_player,
    [EnumMember(Value = "#pan_camera")]
    pan_camera,
    pan_camera_to_point,
    start_constant_shake,
    stop_constant_shake,
    [EnumMember(Value = "#fade_screen")]
    fade_screen,
    #endregion

    #region 提示
    event_end_hint,
    [EnumMember(Value = "#add_loop_light")]
    add_loop_light,
    [EnumMember(Value = "#remove_loop_light")]
    remove_loop_light,
    highlight,
    #endregion

    #region 系統
    [EnumMember(Value = "#wait")]
    wait,
    [EnumMember(Value = "#wait_random_in_range")]
    wait_random_in_range,
    [EnumMember(Value = "break")]
    _break,
    editor_save,
    editor_load,
    [EnumMember(Value = "#kill_player")]
    kill_player,
    change_editor_map,
    #endregion

    #region 代數
    [EnumMember(Value = "#set_switch")]
    set_switch,
    [EnumMember(Value = "#toggle_switch")]
    toggle_switch,
    [EnumMember(Value = "#set_variable")]
    set_variable,
    [EnumMember(Value = "#add_variable")]
    add_variable,
    [EnumMember(Value = "#save_event_pos")]
    save_event_pos,
    #endregion

    #region 條件
    [EnumMember(Value = "#if")]
    _if,
    [EnumMember(Value = "#else")]
    _else,
    end_if,
    #endregion

    [EnumMember(Value = "#comment")]
    comment
}

public enum VariantType
{
    [JsonProperty(PropertyName = "var")]
    Var,
    [JsonProperty(PropertyName = "switch")]
    Switch,
}

public enum LayerType
{
    back,
    mid,
    front
}

public enum AnimationType
{
    [EnumMember(Value = "001_Candle")]
    _001_Candle,
    [EnumMember(Value = "002_Ray")]
    _002_Ray,
    [EnumMember(Value = "003_Glitch")]
    _003_Glitch,
    [EnumMember(Value = "004_Float")]
    _004_Float,
    [EnumMember(Value = "011_Boom")]
    _011_Boom,
    [EnumMember(Value = "012_Shine")]
    _012_Shine,
    [EnumMember(Value = "013_Frozen")]
    _013_Frozen,
    [EnumMember(Value = "014_Stone")]
    _014_Stone,
    [EnumMember(Value = "015_FrozenSudden")]
    _015_FrozenSudden,
    [EnumMember(Value = "016_Smoke")]
    _016_Smoke,
    [EnumMember(Value = "101_Love")]
    _101_Love,
    [EnumMember(Value = "102_Clock")]
    _102_Clock,
    [EnumMember(Value = "103_Stove")]
    _103_Stove,
    [EnumMember(Value = "104_Tree")]
    _104_Tree,
    [EnumMember(Value = "105_Firewood")]
    _105_Firewood,
    [EnumMember(Value = "106_Pump")]
    _106_Pump,
    [EnumMember(Value = "107_Stream")]
    _107_Stream,
    [EnumMember(Value = "108_Laugh")]
    _108_Laugh,
    [EnumMember(Value = "111_WineShake")]
    _111_WineShake,
    [EnumMember(Value = "112_WineDrink")]
    _112_WineDrink,
    [EnumMember(Value = "113_IceMelt")]
    _113_IceMelt,
    [EnumMember(Value = "114_DoorOpen")]
    _114_DoorOpen,
    [EnumMember(Value = "115_DoorClose")]
    _115_DoorClose,
    [EnumMember(Value = "116_RoomLock")]
    _116_RoomLock,
    [EnumMember(Value = "117_RoomUnlock")]
    _117_RoomUnlock
}

public enum BgmType
{
    None,
    [EnumMember(Value ="101_CozyRoom")]
    _101_CozyRoom,
    [EnumMember(Value = "102_ClippyVillage")]
    _102_ClippyVillage,
    [EnumMember(Value = "103_RecoverStore")]
    _103_RecoverStore,
    [EnumMember(Value = "104_Faith")]
    _104_Faith,
    [EnumMember(Value = "105_Intro")]
    _105_Intro,
    [EnumMember(Value = "201_QwertySad")]
    _201_QwertySad,
    [EnumMember(Value = "202_CurvedCave")]
    _202_CurvedCave,
    [EnumMember(Value = "203_SwordCave")]
    _203_SwordCave,
    [EnumMember(Value = "204_QwertyHappy")]
    _204_QwertyHappy,
    [EnumMember(Value = "301_HeroVillage")]
    _301_HeroVillage,
    [EnumMember(Value = "302_BartEnder")]
    _302_BartEnder,
    [EnumMember(Value = "303_CathedralOfBones")]
    _303_CathedralOfBones,
    [EnumMember(Value = "304_TemPlait")]
    _304_TemPlait,
    [EnumMember(Value = "305_HeroRoad")]
    _305_HeroRoad,
    [EnumMember(Value = "401_ColorOfIce")]
    _401_ColorOfIce,
    [EnumMember(Value = "402_OutPost")]
    _402_OutPost,
    [EnumMember(Value = "403_TwoHeadedGiant")]
    _403_TwoHeadedGiant,
    [EnumMember(Value = "404_Fragments")]
    _404_Fragments,
    [EnumMember(Value = "405_CastleAhead")]
    _405_CastleAhead,
    [EnumMember(Value = "501_CastleGate")]
    _501_CastleGate,
    [EnumMember(Value = "502_CastleHall")]
    _502_CastleHall,
    [EnumMember(Value = "503_PrincessBedroom")]
    _503_PrincessBedroom,
    [EnumMember(Value = "504_CastleHallway")]
    _504_CastleHallway,
    [EnumMember(Value = "505_TheDragon")]
    _505_TheDragon,
    [EnumMember(Value = "601_BadEnd")]
    _601_BadEnd,
    [EnumMember(Value = "602_Message")]
    _602_Message,
    [EnumMember(Value = "603_Farewell")]
    _603_Farewell,
    [EnumMember(Value = "701_EmehTemagDrow")]
    _701_EmehTemagDrow,
    [EnumMember(Value = "702_KillTheDragon")]
    _702_KillTheDragon,
    [EnumMember(Value = "801_MindTyper")]
    _801_MindTyper,
    [EnumMember(Value = "802_Home")]
    _802_Home,
    [EnumMember(Value = "803_Daughter")]
    _803_Daughter,
    [EnumMember(Value = "901_HolyTower")]
    _901_HolyTower,
}

public enum SeType
{

}