using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] private AudioClip audioForEvent;
    [SerializeField] private AudioClip audioForText;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    private bool isClicked ;
    [SerializeField] private GameObject checkMark;
    private void Start()
    { 
        isClicked = PlayerPrefs.GetInt("AudioIsClick",1) == 1;
        OnAndOffMusic();
    }

    public void AudioClickUI()
    {
        sfxSource.PlayOneShot(clickAudio);
    }

    public void  ToggleMusic()
    {
        isClicked = !isClicked;
        PlayerPrefs.SetInt("AudioIsClick", isClicked ? 1:0);
        PlayerPrefs.Save();
        OnAndOffMusic();
    }

    public void AudioForEvent()
    {
        sfxSource.PlayOneShot(audioForEvent);
    }

    public void AudioForText()
    {
        sfxSource.PlayOneShot(audioForText);
    }
    public void OnAndOffMusic()
    {
        checkMark.SetActive(isClicked);

        if(isClicked == false)
        {
            musicSource.Play();
        }
        else if (isClicked == true)
        {
            musicSource.Pause();
        }
    }

}
