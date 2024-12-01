using GraphViewPlayer;
using UnityEngine;

public class CommandTriggerPort : BasePort
{
    public CommandTriggerPort(string commandId, Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Single)
    {
        name = commandId;
        PortName = "¨Æ¥óÄ²µo";
        PortColor = Color.yellow;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.deleteSentenceRule || (int)otherType > 15) && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
