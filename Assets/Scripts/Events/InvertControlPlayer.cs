using UnityEngine;

public class InvertControlPlayer : Events
{
    public override void Activity()
    {
        base.Activity();
        targetPlayer.isMoveChange = true;
        
    }

    public override void OffEvent()
    {
        targetPlayer.isMoveChange = false;
        base.OffEvent();
    }
}
