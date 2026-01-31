using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    private int bestScore;
    [SerializeField] private Text bestScoreText;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        ShowBestScoreForMenu();
    }

    public void UpdateScore()
    {
        GetComponent<Text>().text = "" + score;
    }

    public void UpdateBestScore()
    {
        if (score >= bestScore)
        {
            bestScore = score;
            bestScoreText.GetComponent<Text>().text = "" + bestScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
        else
        {
            bestScoreText.GetComponent<Text>().text = "" + bestScore;
        }
    }

    private void ShowBestScoreForMenu()
    {
        bestScoreText.GetComponent<Text>().text = "" + bestScore;
    }
}
