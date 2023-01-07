using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{

    public static SoundEffectManager instance;
    private AudioSource source;


    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public  void PlayOneShot(AudioClip _clip)
    {
        source.PlayOneShot(_clip);
        source.pitch = 1;
    }

    public void PlayOneShotRandomPitch(AudioClip _clip)
    {
        
        source.PlayOneShot(_clip);
        source.pitch = Random.Range(0.8f, 1.2f);
    }
}
