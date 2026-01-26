using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Events: MonoBehaviour
{
    [SerializeField] private Button button;
    public float startTimeEvent;
    public Button saveButton;
    public Player targetPlayer;
    public EnemySpawner enemySpawner;
    public float timeEvent;
    public bool isClicked = false;
    public bool isNeedTimer;
    public AudioManager audioManager;
    public event Action OnEventClick;

// добавить Best Score in menu
    private void Start()
    {
        button.transform.localPosition = Vector3.zero;
        button.enabled = true;
    }

    public virtual void Activity()
    {
        OnEventClick?.Invoke();
        isClicked = true;
        MoveTheButtons();
        audioManager.AudioClickUI();
    }
    

    public void CreateButton(Player player)
    {
        targetPlayer = player;
        saveButton = Instantiate(button);
        saveButton.transform.SetParent(GameObject.Find("Canvas").transform,false);
        saveButton.onClick.AddListener(Activity);
        timeEvent = startTimeEvent;
        isClicked = false;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.musicSource.volume = 0.05f;
        audioManager.AudioForEvent();
        saveButton.GetComponent<Events>().StartCoroutine(AnimationButton(saveButton.transform,Vector3.zero,Vector3.one,new Color(1f,1f,1f,0f),new Color(1f,1f,1f,1f)));
    }

    public void MoveTheButtons()
    {
        saveButton.GetComponent<Events>().StartCoroutine(AnimationButton(saveButton.transform,Vector3.one,Vector3.zero,new Color(1f,1f,1f,1f),new Color(1f,1f,1f,0f)));
        audioManager.musicSource.volume = 0.1f;
    }

    public virtual void  OffEvent()
    {
        if(saveButton != null)
            Destroy(saveButton.gameObject);

    }

    public IEnumerator AnimationButton(Transform saveButton,Vector3 startVector,Vector3 endVector,Color startColor, Color endColor)
    {
        float time = 0;
        float duration = 0.3f;
        while(time < duration)
        {
            time += Time.deltaTime;
            float t = time/duration;

            saveButton.localScale = Vector3.Lerp(startVector , endVector,t);
            saveButton.GetComponent<Image>().color = Color.Lerp(startColor,endColor,t);
            yield return null;
        }

        saveButton.localScale = endVector;
        saveButton.GetComponent<Image>().color = endColor;
    }
}