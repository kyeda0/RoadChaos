using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudio;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    private int isClicked = 1;
    [SerializeField] private GameObject checkMark;
    private float musicVol;
    private void Start()
    { 
        musicVol = PlayerPrefs.GetFloat("AudioMusic", musicVol);
        musicSource.volume = musicVol;
    }

    private void Update()
    {
    }

    public void AudioClickUI()
    {
        sfxSource.PlayOneShot(clickAudio);
    }
    public void OnAndOffMusic()
    {
        if(isClicked == 1)
        {
            checkMark.transform.localScale = new Vector3(1f,1f,1f);
            isClicked = 0;
            PlayerPrefs.SetFloat("AudioMusic", musicVol = 0);
            musicSource.volume = musicVol;
            PlayerPrefs.Save();
            //  musicSource.Pause();
        }
        else if (isClicked == 0)
        {
            checkMark.transform.localScale = new Vector3(0f,0f,1f);
            isClicked = 1;
            PlayerPrefs.SetFloat("AudioMusic", musicVol = 0.2f);
            musicSource.volume = musicVol;
            PlayerPrefs.Save();
          //  musicSource.UnPause();
        }
    }

}
