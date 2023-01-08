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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShootAnimal("rat");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShootAnimal("pig");

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShootAnimal("raccoon");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShootAnimal("worm");
        }

    }

    public void ShootAnimal(string _animal)
    {
        SpawnManager.instance.CheckForKill(_animal);
    }
}
