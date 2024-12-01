using GraphViewPlayer;
using UnityEngine;

public class InputPort : BasePort
{
    public InputPort(Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Input, PortCapacity.Multi)
    {
        PortName = "³QÄ²µo";
        PortColor = Color.black;

    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var inputNode = ParentNode as EventNode;
        var outputNode = other.ParentNode as EventNode;
        if (inputNode == null || outputNode == null) return false;
        switch (inputNode.PropertyId)
        {
            case PropertyId.moveNPC:
            case PropertyId.staticNPC:
                return outputNode.IsPlayer && base.CanConnectTo(other);
            case PropertyId.deleteSentenceRule:
                return outputNode.IsObject && base.CanConnectTo(other);
            case PropertyId.customCommand:
            case PropertyId.animationCustomCommand:
                return base.CanConnectTo(other);
            default:
                return (outputNode.IsStart || outputNode.IsObject) && base.CanConnectTo(other);
        }
    }
}
