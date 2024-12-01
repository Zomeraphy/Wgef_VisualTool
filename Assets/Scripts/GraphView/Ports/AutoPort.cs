using GraphViewPlayer;
using UnityEngine;

public class AutoPort : BasePort
{
    public AutoPort(Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Multi)
    {
        PortName = "¦Û°Ê";
        PortColor = Color.red;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (int)otherType > 15 && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
