using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudio;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public void AudioClickUI()
    {
        sfxSource.PlayOneShot(clickAudio);
    }

}
