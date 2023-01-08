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

    public void StopMusic()
    {
        musicSource.DOFade(0, 1);
    }

    void Update()
    {
        if(trackData)
        {
            currentUpdateTime += Time.deltaTime;

            if (currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0f;
                musicSource.clip.GetData(clipSampleData, musicSource.timeSamples); //I read 1024 samples, w$$anonymous$$ch is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
                clipLoudness = 0f;

                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            }

            //Debug.LogError("clip loud: " + clipLoudness.ToString());

            if (clipLoudness > 0.22f)
            {

                //Debug.LogError("clip loud: " + clipLoudness.ToString());
                if(lastPrintedIndex != SpawnManager.instance.currentFrameIndex)
                    //Debug.LogError("index: " + (SpawnManager.instance.currentFrameIndex - 3).ToString());

                lastPrintedIndex = SpawnManager.instance.currentFrameIndex;

            }


            lastLoud = clipLoudness;

        }


    }

}
