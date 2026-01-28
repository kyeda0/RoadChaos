using UnityEngine;

public class AddHealthPlayerForTutorial : Events
{
    public override void Activity()
    {
        targetPlayer.health += 1;
        GameObject.Find("PanelForHealth").GetComponent<HealthUI>().UpdateHealth();
        GameObject.Find("TutorialManager").GetComponent<TutorialManager>().StartStage(TutorialManager.StageTutorial.StageFour,2);
        OffEvent();
    }
}
