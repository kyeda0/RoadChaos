using UnityEngine;

public class NarrowRoadCenterandLeft : Events
{
    public override void Activity()
    {
        base.Activity();
        targetPlayer.minLane = -1;
        targetPlayer.maxLane = 0;
        targetPlayer.ClampLane();
        
    }

    public override void OffEvent()
    {
        targetPlayer.minLane = -1;
        targetPlayer.maxLane = 1;
        base.OffEvent();

    }
}
