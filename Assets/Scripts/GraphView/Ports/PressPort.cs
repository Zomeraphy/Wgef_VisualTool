using GraphViewPlayer;
using UnityEngine;

public class PressPort : BasePort
{
    public PressPort(Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Multi)
    {
        PortName = "¤¬°Ê";
        PortColor = Color.cyan;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.moveNPC || otherType == PropertyId.staticNPC ||
                otherType == PropertyId.customCommand || otherType == PropertyId.animationCustomCommand)
                && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
