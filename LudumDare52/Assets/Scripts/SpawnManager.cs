using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public float gameplayFrameTimer;
    public float currentFrameTime = 0;
    public int currentFrameIndex;
    public Transform LocationParent;
    private Vector3 [] SpawnLocations;
    public List<Vector3> spawnLocs;
    public bool spawnEnemies;

    public Enemy []  pigs;
    public Enemy []  rats;
    public Enemy []  worms;
    public Enemy []  raccoons;

    private int pigIndex;
    private int ratIndex;
    private int wormIndex;
    private int raccoonIndex;
    public List<int> bassKeyFrames;

    [Serializable]
    public class GamePlayFrame
    {
        public bool Pig = false;
        public bool Rat = false;
        public bool Raccoon = false;
        public bool Worm = false;
    }


    public GamePlayFrame  [] Level1;
    public GamePlayFrame  [] Level2;
    public GamePlayFrame  [] Level3;

    public GamePlayFrame [] currentLevel;


    private void Awake()
    {
        bassKeyFrames = new List<int>();
        instance = this;
        spawnLocs = new List<Vector3>();
        for(int i =0; i < LocationParent.childCount; i++)
        {
            spawnLocs.Add(LocationParent.GetChild(i).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            bassKeyFrames.RemoveAt(bassKeyFrames.Count-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError("PUT KEYFRAME HERE: " + (currentFrameIndex - 4).ToString());
            bassKeyFrames.Add(currentFrameIndex - 4);
        }

        if (spawnEnemies)
        currentFrameTime += Time.deltaTime;

        if (currentFrameTime > gameplayFrameTimer)
        {
            if (currentFrameIndex < currentLevel.Length)
            {
                int location = 0;


                if (currentLevel[currentFrameIndex].Rat)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("rat", spawnLocs[location]);
                    spawnLocs.RemoveAt(location);
                }

                if (currentLevel[currentFrameIndex].Pig)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("pig", spawnLocs[location]);
                    spawnLocs.RemoveAt(location);
                }

                if (currentLevel[currentFrameIndex].Worm)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("worm", spawnLocs[location]);
                    spawnLocs.RemoveAt(location);
                }

                if (currentLevel[currentFrameIndex].Raccoon)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("raccoon", spawnLocs[location]);
                    spawnLocs.RemoveAt(location);
                }

                currentFrameIndex++;
                currentFrameTime = 0;
            }
            else
            {
                if(spawnEnemies)
                {
                    if (currentLevel != Level3)
                    {
                        GameManager.instance.LevelComplete();
                    }
                    else
                    {
                        GameManager.instance.WonGame();
                    }
                }
               
            }
        }
    }

    public void StartLevel(int level)
    {
        currentFrameIndex = 0;
        spawnEnemies = true;

        if(level == 0)
        {
            currentLevel = Level1;
        }
        else if (level == 1)
        {
            currentLevel = Level2;
        }
        else if (level == 2)
        {
            currentLevel = Level3;
        }

        currentFrameTime = -3;

    }

    public void StopLevel()
    {
        spawnEnemies = false;
    }

    void CreateEnemy(string _type, Vector3 _pos)
    {
        if(_type == "rat")
        {

            if (ratIndex >= rats.Length)
            {
                ratIndex = 0;
            }

            rats[ratIndex].Spawn(_pos);
            ratIndex++;
        }
        else if (_type == "pig")
        {

            if (pigIndex >= pigs.Length)
            {
                pigIndex = 0;
            }

            pigs[pigIndex].Spawn(_pos);
            pigIndex++;
        }
        else if (_type == "worm")
        {

            if (wormIndex >= worms.Length)
            {
                wormIndex = 0;
            }

            worms[wormIndex].Spawn(_pos);
            wormIndex++;
        }
        else if (_type == "raccoon")
        {

            if (raccoonIndex >= raccoons.Length)
            {
                raccoonIndex = 0;
            }

            raccoons[raccoonIndex].Spawn(_pos);
            raccoonIndex++;
        }
    }

    public void CheckForKill(string _animal)
    {
        int oldestIndex = -1;
        float oldestTime = 0;

        if (_animal == "rat")
        {
            for (int i = 0; i < rats.Length; i++)
            {
                if (rats[i].aliveTimer > 0 && rats[i].aliveTimer > oldestTime)
                {
                    oldestTime = rats[i].aliveTimer;
                    oldestIndex = i;
                }
            }

            if(oldestIndex != -1)
            {
                rats[oldestIndex].Die();

                float size = rats[oldestIndex].transform.localScale.x;

                GameManager.instance.UpdateScore(size, rats[oldestIndex].transform.position);
            }
        }
        else if (_animal == "pig")
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

                GameManager.instance.UpdateScore(size, pigs[oldestIndex].transform.position);
            }
        }
        else if (_animal == "worm")
        {
            for (int i = 0; i < worms.Length; i++)
            {
                if (worms[i].aliveTimer > 0 && worms[i].aliveTimer > oldestTime)
                {
                    oldestTime = worms[i].aliveTimer;
                    oldestIndex = i;
                }
            }

            if (oldestIndex != -1)
            {
                worms[oldestIndex].Die();
                float size = worms[oldestIndex].transform.localScale.x;

                GameManager.instance.UpdateScore(size, worms[oldestIndex].transform.position);
            }
        }
        else if (_animal == "raccoon")
        {
            for (int i = 0; i < raccoons.Length; i++)
            {
                if (raccoons[i].aliveTimer > 0 && raccoons[i].aliveTimer > oldestTime)
                {
                    oldestTime = raccoons[i].aliveTimer;
                    oldestIndex = i;
                }
            }

            if (oldestIndex != -1)
            {
                raccoons[oldestIndex].Die();
                float size = raccoons[oldestIndex].transform.localScale.x;

                GameManager.instance.UpdateScore(size, raccoons[oldestIndex].transform.position);
            }
        }

        if (oldestIndex == -1)//miss
        {
            GameManager.instance.UpdateScore(0, new Vector3(5, -3, 0));
        }

    }

    public void ActivateSpawn(Vector3 _pos)
    {
        if(!spawnLocs.Contains(_pos))
            spawnLocs.Add(_pos);
    }
}
