 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text textsForTutorial;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallForTutorial;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private AudioManager audioManager;
    public event Action OnFinishTutorial;
    private Coroutine arrowsCoroutineLeft;
    private Coroutine arrowsCoroutineRight;
    private Coroutine typingCoroutine;
    public int countGame = 0;


    private void StageOneTutorial()
    {
        panelTutorial.gameObject.SetActive(true);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
        TextChange("Тапай слева и справа, чтобы менять полосу");
        arrowsCoroutineRight =  StartCoroutine(AnimationForOneStage(3f,gameObjects[0],new Vector3(0.8f,-2f,0f),new Vector3(1.5f,-2f,0f)));
        arrowsCoroutineLeft = StartCoroutine(AnimationForOneStage(3f,gameObjects[1],new Vector3(-0.8f,-2f,0f),new Vector3(-1.5f,-2f,0f)));
    }
    private void StageTwoTutorial()
    { 
        if(arrowsCoroutineRight != null)
        {
            StopCoroutine(arrowsCoroutineRight);
        }
        if(arrowsCoroutineLeft != null)
        {
            StopCoroutine(arrowsCoroutineLeft);
        }
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        TextChange("Уворачивайся от машин");
        enemySpawner.StartSpawnCarForTutorial();
        wall.SetActive(false);
    }
    private void TextChange(string text)
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(AnimationForText(text));
    }
    private void StageThreeTutorial()
    {
        wall.SetActive(true);
        wallForTutorial.SetActive(false);
        TextChange("События могут помочь или помешать");
        textsForTutorial.transform.localPosition = new Vector3(0f,500f,0f);
        eventTrigger.SpawnGoodEventForTutorial();
    }

    private void StageFourTutorial()
    {
        TextChange("Некоторые события опасны");
        eventTrigger.SpawnBadEventForTutorial();
        Destroy(GameObject.Find("EventButtonAddHealthPlayer(Clone)"));
        eventTrigger.enabled = true;
    }

    private IEnumerator DelayStage(StageTutorial stage, float timeDuration)
    {
        float time = 0;
        float duration = timeDuration;
        while (time < duration)
        {
            time += Time.deltaTime;

            yield return null;
        }
        SetStage(stage);
    }

    private void StageFiveTutorial()
    {
        TextChange("Удачи!");
        textsForTutorial.transform.localPosition = new Vector3(0f,0f,0f);
        eventTrigger.enabled = false;
        StartStage(StageTutorial.StageSix,5);
        PlayerPrefs.SetInt("IsFirstGame", countGame = 1);
        PlayerPrefs.Save();
    }


    private void StageSixTutorial()
    {
       OnFinishTutorial?.Invoke();
       healthUI.DeleteHealth();
       panelTutorial.SetActive(false);
       
    }

    public void StartStage(StageTutorial stage, float time)
    {
        StartCoroutine(DelayStage(stage,time));
    }


    private IEnumerator AnimationForText(string text)
    {
        textsForTutorial.text = "";
        for (int i = 0; i < text.Length; i++)
        {  
            textsForTutorial.text += text[i];
            audioManager.AudioForText();
            yield return new WaitForSeconds(0.05f);
        }
    }
    private IEnumerator AnimationForOneStage(float duration, GameObject arrow, Vector3 startVector , Vector3 endVector)
    {
        float time = 0;
        float timeAnimation = duration;
        float timeEndVector = 0f;
        while(time < timeAnimation)
        {
            time += Time.deltaTime;
            arrow.transform.localPosition = Vector3.Lerp(startVector,endVector,time);
            if(arrow.transform.localPosition == endVector)
            {
                timeEndVector += Time.deltaTime;
                arrow.transform.localPosition = Vector3.Lerp(endVector,startVector,timeEndVector);
                if(arrow.transform.localPosition == startVector)
                {
                    time = 0f;
                    timeEndVector = 0f;
                    arrow.transform.localPosition = Vector3.Lerp(startVector,endVector,time);
                }
            }
            yield return null; 
        }
    }

    public void SetStage(StageTutorial stageTutorial)
    {
        switch (stageTutorial)
        {
            case StageTutorial.StageOne:
                StageOneTutorial();
                break;
            case StageTutorial.StageTwo:
                StageTwoTutorial();
                break;
            case StageTutorial.StageThree:
                StageThreeTutorial();
                break;
            case StageTutorial.StageFour:
                StageFourTutorial();
                break;
            case StageTutorial.StageFive:
                StageFiveTutorial();
                break;
            case StageTutorial.StageSix:
                StageSixTutorial();
                break;
        }

    }
    public enum StageTutorial
    {
        StageOne,
        StageTwo,
        StageThree,
        StageFour,
        StageFive,
        StageSix
    }
}
