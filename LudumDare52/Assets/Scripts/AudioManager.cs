using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private AudioSource source;
    public AudioSource musicSource;
    public AudioClip[] levels;
    public AudioClip menuMusic;
    public AudioClip[] MagicSounds;
    public AudioClip[] ComboSounds;
    public AudioClip[] GameOverSounds;

    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;
    bool trackData;
    int lastPrintedIndex = -1;
    private float lastLoud = 0;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();

        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayOneShot(AudioClip _clip)
    {
        source.PlayOneShot(_clip);
        source.pitch = 1;
    }

    public void PlayOneShotRandomPitch(AudioClip _clip)
    {
        
        //source.PlayOneShot(_clip);
        source.pitch = Random.Range(0.8f, 1.2f);
        source.volume = 0.4f;
    }

    public void StartMusic(int _level)
    {
        musicSource.pitch = 1;
        musicSource.DOFade(0, 1.0f).OnComplete(()=>
        {
            //musicSource.volume = 0.3f;
            musicSource.DOFade(0.3f, 0.5f);
            musicSource.clip = levels[_level];
            musicSource.time = 0f;
            trackData = true;
            clipSampleData = new float[sampleDataLength];
            musicSource.Play();
        }
        );
     
        //musicSource.DOFade(1, 1);

    }

    public void GameOverMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.pitch = 0.6f;
        musicSource.DOFade(1, 1).SetDelay(1);
        musicSource.Play();
    }

    public void AdjustPitch(float _rate)
    {
        float normalized = _rate * 0.4f + 0.6f;

        musicSource.DOPitch( normalized, 0.1f);

    }
    public void StopMusic()
    {
        musicSource.DOFade(0, 1);
    }

}
