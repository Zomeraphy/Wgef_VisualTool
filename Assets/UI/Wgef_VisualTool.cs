using UnityEngine;
using UnityEngine.UIElements;
using SFB;
using Newtonsoft.Json;
using System.IO;
using GraphViewPlayer;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

public class Wgef_VisualTool : MonoBehaviour
{
    private VisualElement _root;
    private Button _button_Load;

    private VisualElement _visualTool;
    private VisualElement _toolBar;
    private Button _button_LoadFile;
    private Button _button_SaveFile;
    private Button _button_Undo;
    private Button _button_Redo;
    private Button _button_InitializePosition;
    private VisualElement _content;
    private ListView _listView_Maps;
    private Label _label_Infos;
    private MapGraphView _graphView;

    private string _currentPath;
    private string _jsonString;
    private WordGameEditorFile _wgef;

    public void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _button_Load = _root.Q<Button>("Button_Load");

        _visualTool = _root.Q("VisualTool");
        _toolBar = _visualTool.Q("ToolBar");
        _button_LoadFile = _toolBar.Q<Button>("Button_LoadFile");
        _button_SaveFile = _toolBar.Q<Button>("Button_SaveFile");
        //_button_Undo = _toolBar.Q<ToolbarButton>("Button_Undo");
        //_button_Redo = _toolBar.Q<ToolbarButton>("Button_Redo");
        _button_InitializePosition = _toolBar.Q<Button>("Button_IntializePosition");
        _content = _visualTool.Q<VisualElement>("Content");
        _listView_Maps = _content.Q<ListView>();
        _label_Infos = _content.Q<Label>("Label_Infos");

        foreach (var item in _button_LoadFile.Children())
            item.pickingMode = PickingMode.Ignore;
        foreach (var item in _button_SaveFile.Children())
            item.pickingMode = PickingMode.Ignore;
        _button_Load.RegisterCallback<ClickEvent>(OnButtonLoadClick);
        _button_LoadFile.RegisterCallback<ClickEvent>(OnButtonLoadClick);
        _button_SaveFile.RegisterCallback<ClickEvent>(OnButtonSaveClick);
        _button_InitializePosition.RegisterCallback<ClickEvent>(e => InitializeNodesPosition());

        _listView_Maps.makeItem = () => new Label();
        _listView_Maps.selectedIndicesChanged += i =>
        {
            var map = _listView_Maps.selectedItem as Map;
            GenerateMapGraph(map);
            _graphView.MarkDirtyRepaint();
        };

        _graphView = new MapGraphView { name = "MapGraphView" };
        _graphView.style.flexGrow = 1;
        _graphView.style.color = Color.black;
        _graphView.style.width = new StyleLength(new Length(80, LengthUnit.Percent));
        _content.Add(_graphView);
    }

    private void OnButtonLoadClick(ClickEvent evt)
    {
        var extensions = new[] { new ExtensionFilter("Word Game Editor Files", "wgef") };
        var path = StandaloneFileBrowser.OpenFilePanel("讀取檔案", "", extensions, false);
        if (path != null && path.Length > 0)
        {
            if (_wgef != null)
            {
                _listView_Maps.ClearSelection();
                _listView_Maps.dataSource = null;
                _graphView.DeleteElements(_graphView.GraphElements);
            }
            _currentPath = path[0];
            _jsonString = File.ReadAllText(_currentPath);
            _wgef = JsonConvert.DeserializeObject<WordGameEditorFile>(_jsonString);
            CloseStartPage();
            LoadMaps(_wgef);
        }
    }

    private void OnButtonSaveClick(ClickEvent evt)
    {
        var extensions = new[] { new ExtensionFilter("Word Game Editor Files", "wgef") };
        var path = StandaloneFileBrowser.SaveFilePanel("儲存檔案", Path.GetDirectoryName(_currentPath), Path.GetFileNameWithoutExtension(_currentPath) + "_temp.wgef", extensions);
        if (!string.IsNullOrEmpty(path))
        {
            var obj = JObject.Parse(_jsonString);
            var map = (JObject)((JArray)obj["maps"])[_listView_Maps.selectedIndex];
            var events = (JArray)map["events"];

            foreach (var port in _graphView.Ports)
            {
                if (port.Direction == Direction.Input && port.ParentNode is EventNode input)
                {
                    var event_input = events.FirstOrDefault(x => ((string)x["id"]) == (input.Id));
                    if (event_input == null) continue;
                    if (!port.Connected())
                    {
                        event_input["attribute"]["eventTriggerAction"] = EventTriggerType.trigger.ToString();
                        event_input["attribute"]["existCondition"] = string.Format("s:switch_name_{0}==true", event_input["name"]);
                        continue;
                    }
                    var output_port = port.Connections.FirstOrDefault().Output;
                    var output = output_port.ParentNode as EventNode;
                    
                    if (output.IsObject)
                    {
                        event_input["attribute"]["eventTriggerAction"] = EventTriggerType.trigger.ToString();
                        event_input["attribute"]["existCondition"] = string.Format("s:switch_name_{0}==true", event_input["name"]);
                    }
                    else
                    {
                        event_input["attribute"]["existCondition"] = string.Empty;
                        if (output.IsStart)
                            event_input["attribute"]["eventTriggerAction"] = EventTriggerType.auto.ToString();
                        else if (output.IsPlayer && output_port.PortName == "互動")
                            event_input["attribute"]["eventTriggerAction"] = EventTriggerType.press.ToString();
                        else if (output.IsPlayer && output_port.PortName == "觸碰")
                            event_input["attribute"]["eventTriggerAction"] = EventTriggerType.touch.ToString();
                    }
                }
                else if (port.Direction == Direction.Output && port.ParentNode is EventNode output && output.IsObject)
                {
                    var event_output = events.FirstOrDefault(x => ((string)x["id"]) == (output.Id));
                    if (event_output == null) continue;
                    var targetId = port.Connected() ? (port.Connections.FirstOrDefault().Input.ParentNode as EventNode)?.Id : null;
                    switch (output.Event.propertyId)
                    {
                        case PropertyId.sentenceRule:
                            event_output["property"]["finishId"] = targetId;
                            break;
                        case PropertyId.customCommand:
                        case PropertyId.animationCustomCommand:
                            if (port.PortName == "執行完觸發")
                            {
                                event_output["property"]["callbackId"] = targetId;
                                break;
                            }
                            var commands = (JArray)event_output["property"]["commands"];
                            var value = commands.FirstOrDefault(c => ((string)c["id"]) == port.name)["value"];
                            if (port.PortName == "事件觸發")
                                value["target"] = targetId;
                            else if (port.PortName == "成句觸發")
                                value["switch"] = targetId;
                            break;
                        default:
                            event_output["property"]["callbackId"] = targetId;
                            break;
                    }
                }
            }
            File.WriteAllText(path, obj.ToString());
        }
    }

    private void LoadMaps(WordGameEditorFile wgef)
    {
        _listView_Maps.bindItem = (e, i) =>
        {
            var label = e as Label;
            label.text = wgef.maps[i].title;
            label.SetPadding(15, UIElementUtility.SideType.TopLeft);
            label.SetMargin(5, UIElementUtility.SideType.Horizontal);
        };
        _listView_Maps.itemsSource = wgef.maps;
    }

    private void GenerateMapGraph(Map map)
    {
        if (map == null) return;

        var startNode = new EventNode(new Event { id = "start", name = "遊戲開始" });

        _graphView.AddElement(startNode);
        var playerNode = new EventNode(new Event { id = "player", name = "我｜玩家" });
        _graphView.AddElement(playerNode);
        var triggerPorts = new List<BasePort>();

        foreach (var e in map.events)
        {
            if (e.propertyId == PropertyId.dummy ||
               (e.propertyId >= PropertyId.deletableEvent && e.propertyId <= PropertyId.mergeRule) ||
                e.propertyId == PropertyId.loopLightEvent || e.propertyId == PropertyId.placeholderEvent) continue;
            var node = new EventNode(e);
            node.RegisterCallback<MouseDownEvent>(OnNodeMouseDown);
            _graphView.AddElement(node);
        }

        foreach (var p in _graphView.Ports)
        {
            var node = p.ParentNode as EventNode;
            if (node != null && node.IsObject)
            {
                if (p.Direction == Direction.Input)
                {
                    switch (node.TriggerType)
                    {
                        case EventTriggerType.auto:
                            if (node.InputPort == null) break;
                            _graphView.AddEdgeByPorts(startNode.OutputPort, node.InputPort);
                            break;
                        case EventTriggerType.press:
                            _graphView.AddEdgeByPorts(playerNode.OutputPorts[0], node.InputPort);
                            break;
                        case EventTriggerType.touch:
                            _graphView.AddEdgeByPorts(playerNode.OutputPorts[1], node.InputPort);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var triggerId = string.Empty;
                    switch (p.PortName)
                    {
                        case "執行完觸發":
                            triggerId = node.Event.property.callbackId;
                            break;
                        case "成句觸發":
                            if (node.PropertyId == PropertyId.sentenceRule)
                                triggerId = (node.Event.property as SentenceProperty)?.finishId;
                            else
                                triggerId = ((node.Event.property as CommandProperty)?.commands
                                            .FirstOrDefault(c => c.type == CommandType.append_sentence_rule && c.id == p.name)
                                            .value as SentenceRuleCommandValue)?._switch;
                            break;
                        case "事件觸發":
                            triggerId = ((node.Event.property as CommandProperty)?.commands
                                         .FirstOrDefault(c => c.type == CommandType.callback_trigger && c.id == p.name)
                                         .value as SingleTargetCommandValue).target;
                            break;
                    }

                    if (!string.IsNullOrEmpty(triggerId))
                    {
                        var inputNode = _graphView.Nodes.FirstOrDefault(n => (n as EventNode).Id == triggerId);
                        var input = (inputNode as EventNode).InputPort;
                        _graphView.AddEdgeByPorts(p, input);
                    }
                }
            }
        }
        InitializeNodesPosition();
    }

    private void OnNodeMouseDown(MouseDownEvent evt)
    {
        if(evt.target is EventNode node)
        {
            _label_Infos.text = node.InspectorText;
        }
    }

    private void InitializeNodesPosition()
    {
        float startX = 50;
        float startY = 50;
        float x = 50;
        float y = 50;
        float firstColWidth = 0;
        float maxWidth = 0;
        var start = _graphView.Nodes.FirstOrDefault(n => n is EventNode node && node.IsStart);
        start.SetPosition(new Vector2(startX, startY));
        y += start.layout.height + 200;
        firstColWidth = Mathf.Max(firstColWidth, start.layout.width);

        var player = _graphView.Nodes.FirstOrDefault(n => (n as EventNode).IsPlayer);
        player.SetPosition(new Vector2(startX, y));
        y += player.layout.height + 200;
        firstColWidth = Mathf.Max(firstColWidth, player.layout.width);

        _graphView.Nodes.Where(n => n is EventNode node && node.PropertyId == PropertyId.sentenceRule)
                        .ToList()
                        .ForEach(n =>
                        {
                            n.SetPosition(new Vector2(startX, y));
                            y += n.layout.height + 20;
                            firstColWidth = Mathf.Max(firstColWidth, n.layout.width);
                        });

        x += firstColWidth + 30;
        y = startY;

        _graphView.Nodes.Where(n => n is EventNode node && node.IsObject && node.TriggerType == EventTriggerType.auto &&
                                    node.PropertyId != PropertyId.sentenceRule)
                        .ToList()
                        .ForEach(n =>
                        {
                            if (y + n.layout.height > startY + start.layout.height + 200)
                            {
                                y = startY;
                                x += maxWidth + 10;
                            }
                            n.SetPosition(new Vector2(x, y));
                            y += n.layout.height + 10;
                            maxWidth = Mathf.Max(maxWidth, n.layout.width);
                        });

        x = startX + firstColWidth + 30;
        y = startY + start.layout.height + 200;

        _graphView.Nodes.Where(n => n is EventNode node && node.IsObject &&
                               (node.TriggerType == EventTriggerType.press || node.TriggerType == EventTriggerType.touch))
                        .ToList()
                        .ForEach(n =>
                        {
                            if (y + n.layout.height > startY + start.layout.height + player.layout.height + 400)
                            {
                                y = startY + start.layout.height + 200;
                                x += maxWidth + 10;
                                maxWidth = 0;
                            }
                            n.SetPosition(new Vector2(x, y));
                            y += n.layout.height + 10;
                            maxWidth = Mathf.Max(maxWidth, n.layout.width);
                        });

        x = startX + firstColWidth + 30;
        y = startY + start.layout.height + player.layout.height + 400;
        maxWidth = 0;

        _graphView.Nodes.Where(n => n is EventNode node && node.IsObject && node.TriggerType == EventTriggerType.trigger)
                        .ToList()
                        .ForEach(n =>
                        {
                            if (y + n.layout.height > _graphView.layout.height)
                            {
                                y = startY + start.layout.height + player.layout.height + 400;
                                x += maxWidth + 10;
                                maxWidth = 0;
                            }
                            n.SetPosition(new Vector2(x, y));
                            y += n.layout.height + 10;
                            maxWidth = Mathf.Max(maxWidth, n.layout.width);
                        });
    }

    private void CloseStartPage()
    {
        if (!_button_Load.visible) return;
        _button_Load.style.display = DisplayStyle.None;
        _visualTool.style.display = DisplayStyle.Flex;
    }


}
