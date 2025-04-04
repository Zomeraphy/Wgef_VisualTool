using GraphViewPlayer;
using UnityEngine;

public class SentencePort : BasePort
{
    public SentencePort(Orientation orientation) : base(orientation, Direction.Output, PortCapacity.Single)
    {
        PortName = "���yĲ�o";
        PortColor = Color.yellow;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.deleteSentenceRule || (int)otherType > 15) && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
