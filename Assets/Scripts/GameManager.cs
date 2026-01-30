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
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private AudioManager audioManager;
    private GameState currentgameState;
    void Start()
    {
        tutorialManager.countGame = PlayerPrefs.GetInt("IsFirstGame");
        player = Instantiate(playerOriginal, new Vector3(0f,-4,0f),Quaternion.identity);
        if(tutorialManager.countGame == 0)
        {
            ChangeGameState(GameState.Tutorial);
        }
        else if (tutorialManager.countGame == 1)
        {
            ChangeGameState(GameState.StartGame);
        }
        eventTrigger.OnEventTime += HandleEventTrigger;
        eventTrigger.OnGamePlaying += HandlePlayingTrigger;
        pauseButton.OnClickPause += HandlePauseTrigger;
        pauseButton.OnPlayingGame += HandlePlayingTrigger;
        tutorialManager.OnFinishTutorial += HandleStartTrigger;
        eventTrigger.TargetPlayer = player;
    }
    

    private void State()
    {
        switch (currentgameState)
        {
            case GameState.StartGame:
                enemySpawner.enemyCarFactories = new List<EnemyCarFactory>(Resources.LoadAll<EnemyCarFactory>("EnemyCarScriptObject"));
                score.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(true);
                player.OnGameOverEvent += HandleGameOverTrigger;
                eventTrigger.currentTime = eventTrigger.startTime;
                eventTrigger.enabled = true;
                player.health = 1;
                healthUI.UpdateHealth();
                tutorialManager.gameObject.SetActive(false);
                audioManager.musicSource.volume = 0.1f;
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
                eventTrigger.enabled = true;
                pauseButton.gameObject.SetActive(true);
                break;

            case GameState.Event:
                enemySpawner.StopSpawnCar();
                enemySpawner.StopCarSpeed();
                eventTrigger.OnRandomEvent();
                foreach (var item in roads)
                {
                    item.isEvent = true;
                }
                player.isPossibleToMove = false;
                player.GetComponent<BoxCollider2D>().isTrigger = true;
                pauseButton.gameObject.SetActive(false);
                break;
            case GameState.Pause:
                player.isPossibleToMove = false;
                enemySpawner.StopSpawnCar();
                foreach (var item in roads)
                {
                    item.isEvent = true;
                }
                enemySpawner.StopCarSpeed();
                enemySpawner.StopSpawnCar();
                pauseButton.gameObject.SetActive(false);
                panelForPause.SetActive(true);
                score.gameObject.SetActive(false);
                eventTrigger.enabled = false;
                player.GetComponent<BoxCollider2D>().isTrigger = true;
                break;

                case GameState.Tutorial:
                tutorialManager.SetStage(TutorialManager.StageTutorial.StageOne);
                eventTrigger.enabled = false;
                player.boxCollider2D.isTrigger = true;
                audioManager.musicSource.volume = 0.05f;
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

    private void HandleStartTrigger()
    {
        ChangeGameState(GameState.StartGame);
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
        Pause,
        Tutorial
    }


    public void RestartGame()
    {
        player.OnGameOverEvent -= HandleGameOverTrigger;
        eventTrigger.OnEventTime -= HandleEventTrigger;
        eventTrigger.OnGamePlaying -= HandlePlayingTrigger;
        pauseButton.OnClickPause -= HandlePauseTrigger;
        pauseButton.OnPlayingGame -= HandlePlayingTrigger;
        tutorialManager.OnFinishTutorial -= HandleStartTrigger;
        SceneManager.LoadScene("MainScene");
    }
    
}
