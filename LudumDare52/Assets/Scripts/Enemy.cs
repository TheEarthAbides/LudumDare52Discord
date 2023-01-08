using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    public AudioClip spawnSound;
    public AudioClip deathSound;

    public float aliveTimer;
    bool dead;
    private Transform arrowTrans;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    private void Awake()
    {
        arrowTrans = transform.GetChild(0);
        spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        spriteRenderer.color = Color.white;
    }

    public void Update()
    {
        aliveTimer += Time.deltaTime;
    }

    public void Spawn(Vector3 _pos)
    {
        dead = false;
        //SoundEffectManager.instance.PlayOneShot(spawnSound);

        //GetComponent<MeshRenderer>().material.color = Color.white;
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.position = _pos;
        arrowTrans.DORotate(new Vector3(0, 0, 720), 1.1f,RotateMode.FastBeyond360).SetEase(Ease.OutQuad);
        StartCoroutine(PerfectIndicator());

        transform.DOScale(1, 1.2f).SetEase(Ease.OutQuad);

        transform.DOScale(0, 0.3f).SetEase(Ease.InQuad).SetDelay(1.4f).OnComplete(()=> 
        {
            if(!dead) GameManager.instance.UpdateScore(0, transform.position); gameObject.SetActive(false); aliveTimer = 0;
        });
    }

    public void Die()
    {
        //SoundEffectManager.instance.PlayOneShot(deathSound);
        DOTween.Kill(this);
        dead = true;
        transform.DOShakePosition(0.1f, 0.2f);
        transform.DOShakeRotation(0.1f, 1f).OnComplete(()=> { gameObject.SetActive(false); }) ;
        aliveTimer = 0;
    }

    private IEnumerator PerfectIndicator()
    {
        // PerfectIndicator signaling needs more juice
        yield return new WaitForSeconds(0.9f);
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = GnomeGame.Colors.gnomeFadeOutPurple;
    }
    //
}
