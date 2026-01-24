using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    private int bestScore;
    [SerializeField] private Text bestScoreText;
    public void UpdateScore()
    {
        GetComponent<Text>().text = "Score: " + score;
    }

    public void UpdateBestScore()
    {
        bestScore = score;
        bestScoreText.GetComponent<Text>().text = "" + bestScore;
    }
}
