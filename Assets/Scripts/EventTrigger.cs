using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private List<Events> events;
    public float startTime;
    public Player TargetPlayer;
    public Events randomEvent;
    public event Action OnEventTime;
    public event Action OnGamePlaying; 

    public float currentTime;
    private Events lastRandomEvent;
    [SerializeField] private Image imageEventTimer;
    [SerializeField] private Events eventForTutorialAddHealth;
    [SerializeField] private Events eventForTutorialInvertPlayer;


    private void Update()
    {
        if (currentTime <= 0)
        {
            if (randomEvent == null)
            {
                OnEventTime?.Invoke();
            }
            else if (randomEvent != null && randomEvent.isClicked == true)
            {
                if (randomEvent.timeEvent <= 0)
                {
                    randomEvent.OffEvent();
                    randomEvent = null;
                    currentTime = startTime;
                    imageEventTimer.gameObject.SetActive(false);
                }
                else if (randomEvent.timeEvent > 0)
                {
                    randomEvent.timeEvent -= Time.deltaTime;
                    imageEventTimer.fillAmount = randomEvent.timeEvent / randomEvent.startTimeEvent;
                }
            }
        }
        else if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }
 
    public void OnRandomEvent()
    {   
        randomEvent = GetRandomEvent();
        if(randomEvent.isNeedTimer == true)
        {
            imageEventTimer.gameObject.SetActive(true);
        }

        randomEvent.CreateButton(TargetPlayer);
        randomEvent.OnEventClick -= CallGameManager;
        randomEvent.OnEventClick += CallGameManager;
        lastRandomEvent = randomEvent;
    }

    private Events GetRandomEvent()
    {
        Events Event;
        do
        {
            Event = events[Random.Range(0,events.Count)];
        }
        while(lastRandomEvent == Event);
        return Event;
    }

    private void CallGameManager()
    {
        OnGamePlaying?.Invoke();
    }

    public void SpawnGoodEventForTutorial()
    {
        randomEvent = eventForTutorialAddHealth;
        randomEvent.CreateButton(TargetPlayer);
    }

    public void SpawnBadEventForTutorial()
    {
        randomEvent = eventForTutorialInvertPlayer;
        randomEvent.CreateButton(TargetPlayer);
        imageEventTimer.gameObject.SetActive(true);
    }
}
