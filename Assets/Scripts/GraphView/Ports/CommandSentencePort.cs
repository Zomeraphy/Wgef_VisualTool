using GraphViewPlayer;
using UnityEngine;

public class CommandSentencePort : BasePort
{
    public CommandSentencePort(string commandId, Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Single)
    {
        name = commandId;
        PortName = "¦¨¥yÄ²µo";
        PortColor = Color.yellow;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.deleteSentenceRule || (int)otherType > 15) && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
