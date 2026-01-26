using UnityEngine;

public class NarrowRoadCenterandRight : Events
{
    public override void Activity()
    {
        base.Activity();
        targetPlayer.minLane = 0;
        targetPlayer.maxLane = 1;
        targetPlayer.ClampLane();
        
    }

    public override void OffEvent()
    {
        targetPlayer.minLane = -1;
        targetPlayer.maxLane = 1;
        base.OffEvent();

    }
}
