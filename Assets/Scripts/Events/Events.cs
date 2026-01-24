using System;
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
    public AudioManager audioManager;
    public event Action OnEventClick;


    private void Start()
    {
        button.transform.localPosition = new Vector3(0f,0f);
        button.transform.localScale = new Vector3(1f, 1f);
        button.enabled = true;
    }

    public virtual void Activity()
    {
        OnEventClick?.Invoke();
        isClicked = true;
        MoveTheButtons();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
    }

    public void MoveTheButtons()
    {
        saveButton.transform.localScale = new Vector3(0f, 0f);
    }

    public virtual void  OffEvent()
    {
        if(saveButton!= null)
            Destroy(saveButton.gameObject);
    }
}