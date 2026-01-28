using UnityEngine;

public class TutorialWall : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyCarTutorial"))
        {
            Destroy(collision.gameObject);
            tutorialManager.SetStage(TutorialManager.StageTutorial.StageThree);
        }
    }
}
