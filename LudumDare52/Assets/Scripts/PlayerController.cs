using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //miss
    //bad
    //good
    //perfect
    public float TimeWindow;
    public AudioClip Attack1;
    public AudioClip Attack2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))//special monsters
        {

        }
        else if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))//special monsters
        {

        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShootAnimal("cat");
            SoundEffectManager.instance.PlayOneShotRandomPitch(Attack1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShootAnimal("pig");
            SoundEffectManager.instance.PlayOneShotRandomPitch(Attack2);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

    }

    public void ShootAnimal(string _animal)
    {
        SpawnManager.instance.CheckForKill(_animal);
    }
}
