using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int uiFeedbackIndex;
    public UIFeedback[] UIFeedbacks;
    public int currentLevel;
    public int currentStreak;
    public int currentPoints;
    public int PerfectPoints;
    public int GoodPoints;
    public int BadPoints;
    public int MissPoints;

    public AudioClip[] SuccessSound;
    public AudioClip[] MissSound;
    public GnomeFeedback gnomeFeedback;

    private void Awake()
    {
        instance = this;
    }


    public void UpdateScore(float size, Vector3 _pos)
    {
        if(uiFeedbackIndex >= UIFeedbacks.Length)
        {
            uiFeedbackIndex = 0;
        }

        if (size < 0.4f)
        {
            UIFeedbacks[uiFeedbackIndex].Spawn("miss", _pos);
            currentPoints += MissPoints;
            UIManager.instance.UpdatePoints(currentPoints);

            currentStreak = 0;
            UIManager.instance.UpdateCombo(currentStreak);
            int clip = Random.Range(0, MissSound.Length);

            AudioManager.instance.PlayOneShot(MissSound[clip]);
            gnomeFeedback.ShowNegative();



        }
        else if (size < 0.6f)
        {
            Debug.Log("Bad");
            UIFeedbacks[uiFeedbackIndex].Spawn("bad", _pos);
            currentPoints += BadPoints;
            UIManager.instance.UpdatePoints(currentPoints);

            currentStreak = 0;
            UIManager.instance.UpdateCombo(currentStreak);

        }
        else if (size < 0.9f)
        {
            Debug.Log("Good");
            UIFeedbacks[uiFeedbackIndex].Spawn("good", _pos);
            currentPoints += GoodPoints;
            UIManager.instance.UpdatePoints(currentPoints);

            currentStreak = 0;
            UIManager.instance.UpdateCombo(currentStreak);

        }
        else
        {
            Debug.Log("Perfect");
            UIFeedbacks[uiFeedbackIndex].Spawn("perfect", _pos);
            currentPoints += PerfectPoints;
            UIManager.instance.UpdatePoints(currentPoints);

            currentStreak++;
            UIManager.instance.UpdateCombo(currentStreak);

            int clip = Random.Range(0, SuccessSound.Length);
            //AudioManager.instance.PlayOneShot(SuccessSound[clip]);
        }

        if(currentStreak > 0 && currentStreak% 10 == 0)
        {
            AudioManager.instance.PlayOneShot(AudioManager.instance.ComboSounds[Random.Range(0, AudioManager.instance.ComboSounds.Length)]);
            gnomeFeedback.ShowPositive();
        }

        uiFeedbackIndex++;
    }

    public void StartGame()
    {
        currentLevel = 0;
        UIManager.instance.FadeTitle();
        SpawnManager.instance.StartLevel(currentLevel);

        AudioManager.instance.StartMusic(currentLevel);
        UIManager.instance.UpdateCombo(0);
        UIManager.instance.UpdatePoints(0);
    }

    public void LevelComplete()
    {
        UIManager.instance.LevelComplete();
        SpawnManager.instance.StopLevel();
        AudioManager.instance.StopMusic();

    }

    public void GameOver()
    {
        UIManager.instance.GameOver();
        SpawnManager.instance.StopLevel();
        AudioManager.instance.StopMusic();

    }

    public void NextLevel()
    {
        currentLevel++;
        currentStreak = 0;
        UIManager.instance.UpdateCombo(0);

        SpawnManager.instance.locationIndex = 0;
        SpawnManager.instance.StartLevel(currentLevel);
        UIManager.instance.LevelCompleteFade();
        AudioManager.instance.StartMusic(currentLevel);

    }

    public void RestartGame()
    {
        SpawnManager.instance.locationIndex = 0;

        currentStreak = 0;
        UIManager.instance.UpdateCombo(0);

        UIManager.instance.GameOverFade();
        currentLevel = 0;
        SpawnManager.instance.StartLevel(currentLevel);
        AudioManager.instance.StartMusic(currentLevel);


    }

    public void WonGame()
    {
        UIManager.instance.WonGame();
        SpawnManager.instance.StopLevel();
        AudioManager.instance.StopMusic();


    }

    public void WonGameRestart()
    {
        UIManager.instance.WonGameFade();
        currentLevel = 0;
        SpawnManager.instance.StartLevel(currentLevel);
        AudioManager.instance.StartMusic(currentLevel);

    }
}
