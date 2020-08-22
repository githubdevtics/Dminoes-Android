using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WinGameController : MonoBehaviour
{
    public Text ScoreText;
    public Text Time;
    public List<Image> ListStar = new List<Image>();
    public int Star = 0;
    int minutes = 0;
    int seconds = 0;

    public void InitStar(int star, int score, int curTime)
    {
        ScoreText.text = score.ToString();
        seconds = curTime % 60;
        minutes = curTime / 60;
        if (minutes >= 99)
            minutes = 99;
        Time.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        Star = star;
        for (int i = 0; i < 3;i++)
        {
            if(i < star)
            {
                ListStar[i].gameObject.SetActive(true);
            }
            else
            {
                ListStar[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnNextLevel()
    {
        int level = SceneManager.instance.CurrentMapData.Level;
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        this.gameObject.SetActive(false);
        SceneManager.instance.PlayGameController.HidePlayGame();
        if (SceneManager.instance.AllMapData.Count > level)
        {
            SceneManager.instance.CurrentMapData = SceneManager.instance.AllMapData[level];
            SceneManager.instance.LastLevel = SceneManager.instance.CurrentMapData.Level;
            SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.PlayGame);
        }
        else
        {
            SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.Level);
        }
    }

    public void OnBackClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        this.gameObject.SetActive(false);
        SceneManager.instance.PlayGameController.HidePlayGame();
        SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.Level);
    }

    public void OnRetryClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        this.gameObject.SetActive(false);
        SceneManager.instance.PlayGameController.HidePlayGame();
        SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.PlayGame);
    }

}
