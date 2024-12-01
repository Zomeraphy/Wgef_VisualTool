using GraphViewPlayer;
using UnityEngine;

public class EndTriggerPort : BasePort
{
    public EndTriggerPort(Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Single)
    {
        PortName = "°õ¦æ§¹Ä²µo";
        PortColor = Color.yellow;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.deleteSentenceRule || (int)otherType > 15) && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
