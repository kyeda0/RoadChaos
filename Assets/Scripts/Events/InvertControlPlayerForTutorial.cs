using UnityEngine;

public class InvertControlPlayerForTutorial : Events
{
    public override void Activity()
    {
        base.Activity();
        targetPlayer.isMoveChange = true;
        GameObject.Find("TutorialManager").GetComponent<TutorialManager>().StartStage(TutorialManager.StageTutorial.StageFive,5);
    }

    public override void OffEvent()
    {
        targetPlayer.isMoveChange = false;
        base.OffEvent();
    }
}
