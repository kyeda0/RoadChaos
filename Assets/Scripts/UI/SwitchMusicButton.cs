using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMusicButton : MonoBehaviour
{
    private int isClicked = 1;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private AudioSource musicSource  = null;

    private void Start()
    {
        checkMark.transform.localScale = new Vector3(0f,0f,1f);
        isClicked = PlayerPrefs.GetInt("IsClicked");
    }
    public void OnAndOffMusic()
    {
        if(isClicked == 1)
        {
            checkMark.transform.localScale = new Vector3(1f,1f,1f);
            isClicked = 0;
            PlayerPrefs.SetInt("IsClicked",isClicked);
            PlayerPrefs.Save();
            musicSource.Stop();
        }
        else
        {
            checkMark.transform.localScale = new Vector3(0f,0f,1f);
            isClicked = 1;
            PlayerPrefs.SetInt("IsClicked",isClicked);
            PlayerPrefs.Save();
            musicSource.Play();
        }

    }

}
