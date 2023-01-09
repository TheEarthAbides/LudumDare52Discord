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

    public int locationIndex = 0;

    public List<Enemy> ActiveEnemies;


    private void Awake()
    {
        bassKeyFrames = new List<int>();
        instance = this;
        spawnLocs = new List<Vector3>();
        for(int i =0; i < LocationParent.childCount; i++)
        {
            spawnLocs.Add(LocationParent.GetChild(i).position);
        }
        GenerateLevel();

        
    }

    public void GenerateLevel()
    {
        //Level1 = new GamePlayFrame[300];
        List <GamePlayFrame> frames = new List<GamePlayFrame>();
        for(int i = 0; i < 300; i++)
        {
            GamePlayFrame frame = new GamePlayFrame();
            int selection = UnityEngine.Random.Range(0, 10);

            if(selection< 2)
            {
                frame.Rat = true;
            }
            else if(selection< 4)
            {
                frame.Pig = true;
            }
            else if (selection < 6)
            {
                frame.Raccoon = true;
            }
            else if (selection < 8)
            {
                frame.Worm = true;
            }

            frames.Add(frame);
        }

        Level1 = frames.ToArray();
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

                    CreateEnemy("rat", spawnLocs[locationIndex]);
                    //spawnLocs.RemoveAt(location);
                    locationIndex++;
                }

                if (currentLevel[currentFrameIndex].Pig)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("pig", spawnLocs[locationIndex]);
                    //spawnLocs.RemoveAt(location);
                    locationIndex++;

                }

                if (currentLevel[currentFrameIndex].Worm)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("worm", spawnLocs[locationIndex]);
                    //spawnLocs.RemoveAt(location);
                    locationIndex++;

                }

                if (currentLevel[currentFrameIndex].Raccoon)
                {
                    location = UnityEngine.Random.Range(0, spawnLocs.Count);

                    CreateEnemy("raccoon", spawnLocs[locationIndex]);
                    locationIndex++;

                    //spawnLocs.RemoveAt(location);
                }

                if (locationIndex >= spawnLocs.Count)
                {
                    locationIndex = 0;
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

        ActiveEnemies = new List<Enemy>();

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
            ActiveEnemies.Add(rats[ratIndex]);

            ratIndex++;
        }
        else if (_type == "pig")
        {

            if (pigIndex >= pigs.Length)
            {
                pigIndex = 0;
            }

            pigs[pigIndex].Spawn(_pos);
            ActiveEnemies.Add(pigs[pigIndex]);

            pigIndex++;

        }
        else if (_type == "worm")
        {

            if (wormIndex >= worms.Length)
            {
                wormIndex = 0;
            }

            worms[wormIndex].Spawn(_pos);
            ActiveEnemies.Add(worms[wormIndex]);

            wormIndex++;

        }
        else if (_type == "raccoon")
        {

            if (raccoonIndex >= raccoons.Length)
            {
                raccoonIndex = 0;
            }

            raccoons[raccoonIndex].Spawn(_pos);
            ActiveEnemies.Add(raccoons[raccoonIndex]);

            raccoonIndex++;

        }
    }

    public void RemoveEnemy(Enemy _enemy)
    {
        ActiveEnemies.Remove(_enemy);
    }

    public void CheckForKill(string _animal)
    {
        if (ActiveEnemies.Count <= 0) return;

        if(ActiveEnemies[0].type.ToString() == _animal)
        {
            float size = ActiveEnemies[0].transform.localScale.x;

            GameManager.instance.UpdateScore(size, ActiveEnemies[0].transform.position);

            PlayerController.instance.LaunchMissile(ActiveEnemies[0].transform.position);
            ActiveEnemies[0].Die();
        }
        else
        {
            GameManager.instance.UpdateScore(0, new Vector3(5, -3, 0));
        }
    }

}
