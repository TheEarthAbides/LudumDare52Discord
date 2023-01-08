using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    public CanvasGroup TitleGroup;
    public CanvasGroup LevelCompleteGroup;
    public CanvasGroup GameOverGroup;
    public CanvasGroup WonGameGroup;
    public TMPro.TextMeshProUGUI points;
    public TMPro.TextMeshProUGUI combo;
    public Image FeverMeterFill;

    private void Awake()
    {
        instance = this;
    }
    
    public void FadeTitle()
    {
        TitleGroup.DOFade(0, 1).OnComplete(() => { TitleGroup.gameObject.SetActive(false); }) ;
    }

    public void LevelComplete()
    {
        LevelCompleteGroup.gameObject.SetActive(true);
        LevelCompleteGroup.alpha = 0;
        LevelCompleteGroup.DOFade(1, 1);
    }

    public void LevelCompleteFade()
    {
        LevelCompleteGroup.DOFade(0, 1).OnComplete(() => { LevelCompleteGroup.gameObject.SetActive(false); });
    }

    public void GameOver()
    {
        GameOverGroup.gameObject.SetActive(true);
        GameOverGroup.alpha = 0;
        GameOverGroup.DOFade(1, 1);
    }

    public void GameOverFade()
    {
        GameOverGroup.DOFade(0, 1).OnComplete(() => { GameOverGroup.gameObject.SetActive(false); });
    }

    public void WonGame()
    {
        WonGameGroup.gameObject.SetActive(true);
        WonGameGroup.alpha = 0;
        WonGameGroup.DOFade(1, 1);
    }

    public void WonGameFade()
    {
        WonGameGroup.DOFade(0, 1).OnComplete(() => { WonGameGroup.gameObject.SetActive(false); });
    }

    public void UpdatePoints(int _points)
    {
        points.text = _points.ToString();
    }

    public void UpdateCombo(int _combo)
    {
        combo.text = _combo.ToString();
        if(_combo == 0)
        {
            combo.color = Color.white;
        }
        else
        {
            combo.color = Color.green;
            combo.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.05f);

        }
    }

    public void UpdateFeverMeter(float _fever)
    {
        FeverMeterFill.DOFillAmount(_fever, 0.1f);
    }
}
