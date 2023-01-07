using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public float gameplayFrameTimer = 1.5f;
    public float currentFrameTime = 0;
    public int currentFrameIndex;

    public Enemy []  pigs;
    public Enemy []  cats;
    public Enemy []  dogs;
    public Enemy []  rats;
    public Enemy []  raccoons;
    public Enemy []  vultures;

    private int pigIndex;
    private int catIndex;
    private int dogIndex;
    private int ratIndex;
    private int raccoonIndex;
    private int vultureIndex;

    [Serializable]
    public class GamePlayFrame
    {
        public bool Cat;
        public bool Pig;
        public bool Dog;
        public bool Rat;
        public bool Raccoon;
        public bool Vulture;
    }



    public GamePlayFrame  [] Level1;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentFrameTime += Time.deltaTime;

        if (currentFrameTime > gameplayFrameTimer)
        {
            if (currentFrameIndex < Level1.Length)
            {
                if(Level1[currentFrameIndex].Cat)
                {
                    CreateEnemy("cat");
                }

                if (Level1[currentFrameIndex].Pig)
                {
                    CreateEnemy("pig");
                }
            }

            currentFrameIndex++;
            currentFrameTime = 0;
        }
    }

    void CreateEnemy(string _type)
    {
        if(_type == "cat")
        {

            if (catIndex >= cats.Length)
            {
                catIndex = 0;
            }

            cats[catIndex].Spawn();
            catIndex++;
        }
        else if (_type == "pig")
        {

            if (pigIndex >= pigs.Length)
            {
                pigIndex = 0;
            }

            pigs[pigIndex].Spawn();
            pigIndex++;
        }
    }

    public void CheckForKill(string _animal)
    {
        int oldestIndex = -1;
        float oldestTime = 0;

        if (_animal == "cat")
        {


            for (int i = 0; i < cats.Length; i++)
            {
                if (cats[i].aliveTimer > 0 && cats[i].aliveTimer > oldestTime)
                {
                    oldestTime = cats[i].aliveTimer;
                    oldestIndex = i;
                }
            }

            if(oldestIndex != -1)
            {
                cats[oldestIndex].Die();
                float size = cats[oldestIndex].transform.localScale.x;

                GameManager.instance.UpdateScore(size);
            }
        }
        if (_animal == "pig")
        {


            for (int i = 0; i < pigs.Length; i++)
            {
                if (pigs[i].aliveTimer > 0 && pigs[i].aliveTimer > oldestTime)
                {
                    oldestTime = pigs[i].aliveTimer;
                    oldestIndex = i;
                }
            }

            if (oldestIndex != -1)
            {
                pigs[oldestIndex].Die();
                float size = pigs[oldestIndex].transform.localScale.x;

                GameManager.instance.UpdateScore(size);
            }
        }

        if (oldestIndex == -1)//miss
        {
            Debug.Log("MISS");
        }

    }

    
}
