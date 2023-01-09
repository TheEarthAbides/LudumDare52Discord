using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    //miss
    //bad
    //good
    //perfect

    public static PlayerController instance;
    public float TimeWindow;
    private int spellIndex;
    public Transform[] Spells;
    public ParticleSystem ps;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spellIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShootAnimal("rat");
            AudioManager.instance.PlayOneShot(AudioManager.instance.MagicSounds[0]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShootAnimal("pig");
            AudioManager.instance.PlayOneShot(AudioManager.instance.MagicSounds[1]);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShootAnimal("raccoon");
            AudioManager.instance.PlayOneShot(AudioManager.instance.MagicSounds[2]);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShootAnimal("worm");
            AudioManager.instance.PlayOneShot(AudioManager.instance.MagicSounds[3]);

        }
    }

    public void ShootAnimal(string _animal)
    {
        SpawnManager.instance.CheckForKill(_animal);
    }

    public void LaunchMissile(Vector3 _pos)
    {
        if (spellIndex >= Spells.Length)
            spellIndex = 0;

        int num = spellIndex;
        Spells[spellIndex].gameObject.SetActive(true);
        Spells[spellIndex].transform.localPosition = Vector3.zero;

        Spells[spellIndex].transform.DOMove(_pos, 0.1f).OnComplete(() => { Spells[num].gameObject.SetActive(false); });

        spellIndex++;

        ps.Play();
    }
}
