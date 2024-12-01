using System.Collections.Generic;
using System.Linq;
using GraphViewPlayer;
using UnityEngine;
using UnityEngine.UIElements;

public class EventNode : BaseNode
{
    public Event Event => _event;
    private Event _event;

    public string Id => _event?.id ?? string.Empty;
    public bool IsStart => Id == "start";
    public bool IsPlayer => Id == "player";
    public bool IsObject => !IsStart && !IsPlayer;
    public EventTriggerType TriggerType => _event?.attribute?.eventTriggerAction ?? EventTriggerType.auto;
    public PropertyId PropertyId => _event?.propertyId ?? PropertyId.none;

    public BasePort InputPort => _inputPort;
    private BasePort _inputPort;

    public BasePort OutputPort => _outputPorts.FirstOrDefault();
    public List<BasePort> OutputPorts => _outputPorts;
    private List<BasePort> _outputPorts = new List<BasePort>();

    public string InspectorText = string.Empty;


    public EventNode(Event e)
    {
        _event = e;
        name = e.name;

        Initialize();
    }

    private void Initialize()
    {
        Title = name;
        if (Id == "start")
        {
            InspectorText += "�N��C���}�l���`�I�A���O��ڪ���";
            var port = new AutoPort();
            _outputPorts.Add(port);
        }
        else if (Id == "player")
        {
            InspectorText += $"<b>����W�١G</b>\nplayer\n";
            InspectorText += $"\n<b>�r���G</b>\n��\n";
            var port1 = new PressPort();
            _outputPorts.Add(port1);

            var port2 = new TouchPort();
            _outputPorts.Add(port2);
        }
        else
        {
            InspectorText += $"<b>����W�١G</b>\n{_event.name}\n";
            InspectorText += $"\n<b>�r���G</b>\n{_event.attribute?.text}\n";
            switch (_event.propertyId)
            {
                case PropertyId.sentenceRule:
                    BasePort port = new SentencePort(Orientation.Horizontal);
                    InspectorText += $"\n<b>���y�G</b>\n{(_event.property as SentenceProperty)?.text}\n";
                    _outputPorts.Add(port);
                    break;
                case PropertyId.customCommand:
                case PropertyId.animationCustomCommand:
                    _inputPort = new InputPort();

                    if (_event.property is CommandProperty property)
                    {
                        var commands = property.commands;
                        foreach (var command in commands)
                        {
                            if (command.type != CommandType.callback_trigger && command.type != CommandType.append_sentence_rule) continue;

                            if (command.type == CommandType.callback_trigger)
                                _outputPorts.Add(new CommandTriggerPort(command.id));
                            else if (command.type == CommandType.append_sentence_rule)
                            {
                                InspectorText += $"\n<b>���y�G</b>\n{(command.value as SentenceRuleCommandValue)?.text} \n";
                                _outputPorts.Add(new CommandSentencePort(command.id));
                            }
                        }
                    }
                    _outputPorts.Add(new EndTriggerPort());
                    break;
                default:
                    _inputPort = new InputPort();
                    _outputPorts.Add(new EndTriggerPort());
                    break;
            }
        }

        if (_inputPort != null) AddPort(_inputPort);
        foreach (var port in _outputPorts)
        {
            //port.Q<Label>("type").style.fontSize = 20;
            AddPort(port);
        }
    }
}
