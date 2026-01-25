using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerOriginal;
    private Player player;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private List<RoadScroller> roads = new List<RoadScroller>();
    [SerializeField] private GameObject panelForDeath;
    [SerializeField] private GameObject panelForPause;
    [SerializeField] private Score score;
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private PauseButton pauseButton;
    private GameState currentgameState;
    void Start()
    {
        ChangeGameState(GameState.StartGame);
        eventTrigger.OnEventTime += HandleEventTrigger;
        eventTrigger.OnGamePlaying += HandlePlayingTrigger;
        pauseButton.OnClickPause += HandlePauseTrigger;
    }
    

    private void State()
    {
        switch (currentgameState)
        {
            case GameState.StartGame:
                player = Instantiate(playerOriginal, new Vector3(0f,-4,0f),Quaternion.identity);
                player.OnGameOverEvent += HandleGameOverTrigger;
                eventTrigger.TargetPlayer = player;
                healthUI.UpdateHealth();
                ChangeGameState(GameState.Playing);
                break;

            case GameState.Playing:
                player.isPossibleToMove = true;
                foreach (var item in roads)
                {
                    item.isEvent = false ;
                }
                enemySpawner.StartSpawnCar();
                player.GetComponent<BoxCollider2D>().isTrigger = false;
                pauseButton.gameObject.SetActive(true);
                panelForPause.SetActive(false);
                score.gameObject.SetActive(true);
                break;

            case GameState.Event:
                enemySpawner.StopSpawnCar();
                eventTrigger.OnRandomEvent();
                foreach (var item in roads)
                {
                    item.isEvent = true;
                }
                player.isPossibleToMove = false;
                player.GetComponent<BoxCollider2D>().isTrigger = true;
                break;
            case GameState.Pause:
                player.isPossibleToMove = false;
                pauseButton.OnPlayingGame += HandlePlayingTrigger;
                enemySpawner.StopSpawnCar();
                foreach (var item in roads)
                {
                    item.isEvent = true;
                }
                enemySpawner.StopCarSpeed();
                pauseButton.gameObject.SetActive(false);
                panelForPause.SetActive(true);
                score.gameObject.SetActive(false);
                break;

            case GameState.GameOver:
                enemySpawner.StopCarSpeed();
                enemySpawner.StopSpawnCar();
                player.isPossibleToMove = false;
                eventTrigger.enabled = false;
                foreach (var item in roads)
                {
                    item.isEvent = true;
                }
                panelForDeath.gameObject.SetActive(true);
                score.UpdateBestScore();
                break;
        }
    }

    private void HandleEventTrigger()
    {
        ChangeGameState(GameState.Event);
    }

    private void HandlePlayingTrigger()
    {
        ChangeGameState(GameState.Playing);
    }

    private void HandleGameOverTrigger()
    {
        ChangeGameState(GameState.GameOver);
    }

    private void HandlePauseTrigger()
    {
        ChangeGameState(GameState.Pause);
    }
    public void ChangeGameState(GameState newGameState)
    {
        currentgameState = newGameState;
        State();
    }
    public enum GameState
    {
        StartGame,
        Playing,
        Event,
        GameOver,
        Pause
    }


    public void RestartGame()
    {
        player.OnGameOverEvent -= HandleGameOverTrigger;
        eventTrigger.OnEventTime -= HandleEventTrigger;
        eventTrigger.OnGamePlaying -= HandlePlayingTrigger;
        pauseButton.OnClickPause -= HandlePauseTrigger;
        pauseButton.OnPlayingGame -= HandlePlayingTrigger;
        SceneManager.LoadScene("MainScene");
    }
    
}
