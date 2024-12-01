using GraphViewPlayer;
using UnityEngine;

public class TouchPort : BasePort
{
    public TouchPort(Orientation orientation = Orientation.Horizontal) : base(orientation, Direction.Output, PortCapacity.Multi)
    {
        PortName = "Ä²¸I";
        PortColor = Color.green;
    }

    public override bool CanConnectTo(BasePort other, bool ignoreCandidateEdges = true)
    {
        var otherType = (other.ParentNode as EventNode).Event.propertyId;
        return (otherType == PropertyId.moveNPC || otherType == PropertyId.staticNPC ||
                otherType == PropertyId.customCommand || otherType == PropertyId.animationCustomCommand) 
                && base.CanConnectTo(other, ignoreCandidateEdges);
    }
}
