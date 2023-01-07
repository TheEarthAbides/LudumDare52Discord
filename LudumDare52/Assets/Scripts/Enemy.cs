using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    public AudioClip spawnSound;
    public AudioClip deathSound;

    public float aliveTimer;

    // Start is called before the first frame update

    public void Update()
    {
        aliveTimer += Time.deltaTime;
    }

    public void Spawn()
    {
        //SoundEffectManager.instance.PlayOneShot(spawnSound);

        GetComponent<MeshRenderer>().material.color = Color.white;
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);

        transform.DOScale(1, 0.7f).SetEase(Ease.OutQuad).OnComplete(()=>{ GetComponent<MeshRenderer>().material.color = Color.green; });

        transform.DOScale(0, 0.2f).SetEase(Ease.InQuad).SetDelay(1).OnComplete(()=> { gameObject.SetActive(false); aliveTimer =0;});
    }

    public void Die()
    {
        //SoundEffectManager.instance.PlayOneShot(deathSound);
        DOTween.Kill(this);
        gameObject.SetActive(false);
        aliveTimer = 0;
    }

    //
}
