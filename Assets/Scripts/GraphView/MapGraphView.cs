using System.Linq;
using GraphViewPlayer;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGraphView : GraphView
{
    public GraphElementContainer Container => ContentContainer;
    public UQueryState<GraphElement> GraphElements => ContentContainer.ElementsAll;
    public UQueryState<BasePort> Ports => ContentContainer.Ports;
    public UQueryState<BaseNode> Nodes => ContentContainer.Nodes;

    public MapGraphView()
    {
        MaxScale = 2;
    }

    public void AddEdgeByPorts(BasePort outputPort, BasePort inputPort)
    {
        if (outputPort.ParentNode.Equals(inputPort.ParentNode)) return;
        var edge = outputPort.ConnectTo(inputPort) as Edge;
        edge.ColorSelected = edge.Output.PortColor;
        edge.ColorUnselected = edge.Output.PortColor;
        edge.InputColor = edge.Output.PortColor;
        edge.OutputColor = edge.Output.PortColor;
        edge.Input.PortColor = edge.Output.PortColor;
        AddElement(edge);
    }


    protected override void ExecuteEdgeCreate(BaseEdge edge, BasePort output, BasePort input)
    {
        var outputNode = output.ParentNode as EventNode;
        var inputNode = input.ParentNode as EventNode;
        var connected = input.Connected();
        bool deleteOld = false;

        if (outputNode.IsStart && inputNode.TriggerType != EventTriggerType.auto)
        {
            inputNode.Event.attribute.eventTriggerAction = EventTriggerType.auto;
            if (connected) deleteOld = true;
        }
        else if (outputNode.IsPlayer)
        {
            if (output.PortName == "¤¬°Ê" && inputNode.TriggerType != EventTriggerType.press)
            {
                inputNode.Event.attribute.eventTriggerAction = EventTriggerType.press;
                if (connected) deleteOld = true;
            }
            else if (output.PortName == "Ä²¸I" && inputNode.TriggerType != EventTriggerType.touch)
            {
                inputNode.Event.attribute.eventTriggerAction = EventTriggerType.touch;
                if (connected) deleteOld = true;
            }
        }
        else if (outputNode.IsObject && inputNode.TriggerType != EventTriggerType.trigger)
        {
            inputNode.Event.attribute.eventTriggerAction = EventTriggerType.trigger;
            if (connected) deleteOld = true;
        }

        if (deleteOld)
        {
            var edges = input.Connections.ToList();
            foreach (var item in edges)
                DeleteElement(item);
        }
        var _edge = edge as Edge;
        _edge.ColorSelected = output.PortColor;
        _edge.ColorUnselected = output.PortColor;
        input.PortColor = output.PortColor;
        _edge.Input = input;
        _edge.Output = output;
        AddElement(_edge);
        input.RemoveFromClassList("port");
        input.AddToClassList("port");
    }

    protected override void ExecuteEdgeDelete(BaseEdge edge)
    {
        var inputNode = edge.Input.ParentNode as EventNode;
        var outputNode = edge.Output.ParentNode as EventNode;
        if (edge.Input.Connections.Count() == 1)
        {
            edge.Input.PortColor = Color.black;
            edge.Input.RemoveFromClassList("port");
            edge.Input.AddToClassList("port");
        }

        DeleteElement(edge);

        if(outputNode.IsPlayer && (inputNode.PropertyId == PropertyId.moveNPC || inputNode.PropertyId == PropertyId.staticNPC))
        {
            if(inputNode.InputPort.Connections.Count() < 1)
            {
                var e = inputNode.Graph.CreateEdge();
                var o = inputNode.TriggerType == EventTriggerType.press ? outputNode.OutputPorts[0] : outputNode.OutputPorts[1];
                AddEdgeByPorts(o, inputNode.InputPort);
            }
        }
    }

    protected override void OnViewportChanged()
    {
        
    }

    protected override void ExecuteCopy()
    {

    }

    protected override void ExecuteCut()
    {

    }

    protected override void ExecutePaste()
    {

    }

    protected override void ExecuteDuplicate()
    {

    }

    protected override void ExecuteDelete()
    {

    }

    protected override void ExecuteUndo()
    {

    }

    protected override void ExecuteRedo()
    {

    }
}
