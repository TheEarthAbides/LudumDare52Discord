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

    public void Spawn(Vector3 _pos)
    {
        //SoundEffectManager.instance.PlayOneShot(spawnSound);

        //GetComponent<MeshRenderer>().material.color = Color.white;
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.position = _pos;

       
        transform.DOScale(1, 1.2f).SetEase(Ease.OutQuad).OnComplete(()=>{ /*GetComponent<MeshRenderer>().material.color = Color.green;*/ });

        transform.DOScale(0, 0.3f).SetEase(Ease.InQuad).SetDelay(1.4f).OnComplete(()=> {  SpawnManager.instance.ActivateSpawn(transform.position); if(gameObject.activeInHierarchy) GameManager.instance.UpdateScore(0, transform.position); gameObject.SetActive(false); aliveTimer = 0;
        });
    }

    public void Die()
    {
        //SoundEffectManager.instance.PlayOneShot(deathSound);
        SpawnManager.instance.ActivateSpawn(transform.position);
        DOTween.Kill(this);
        gameObject.SetActive(false);
        aliveTimer = 0;
    }

    //
}
