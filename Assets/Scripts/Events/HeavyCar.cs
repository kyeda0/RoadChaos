using UnityEngine;

public class HeavyCar : Events
{
    [SerializeField] private float smoothSpeedForEvent;
    [SerializeField] private float tiltAmountForEvent;
    public override void Activity()
    {
        base.Activity();
        targetPlayer.smoothSpeed = smoothSpeedForEvent;
        targetPlayer.tiltAmount = tiltAmountForEvent;
        
    }

    public override void OffEvent()
    {
        base.OffEvent();
        targetPlayer.smoothSpeed = 2f;
        targetPlayer.tiltAmount = 15f;
    }
}
