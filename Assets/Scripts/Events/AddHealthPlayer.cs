using UnityEngine;

public class AddHealthPlayer : Events
{
    public override void Activity()
    {
        base.Activity();
        targetPlayer.health += 1;
        GameObject.Find("PanelForHealth").GetComponent<HealthUI>().UpdateHealth();
    }
    
    public override void OffEvent()
    {
        base.OffEvent();
    }
}
