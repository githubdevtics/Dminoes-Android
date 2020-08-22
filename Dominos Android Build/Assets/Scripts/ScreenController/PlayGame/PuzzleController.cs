using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

    public Image WinLose;
    public Sprite Win, Lose;
    public List<Image> Gold = new List<Image>();
    public GameObject Next, Replay, Retry;

    public void InitUI(bool isWin, int star)
    {
        WinLose.sprite = isWin ? Win : Lose;
        for(int i = 0; i < 3; i++)
        {
            if (isWin)
            {
                Gold[i].gameObject.SetActive(i < star);
            }
            else
            {
                Gold[i].gameObject.SetActive(false);
            }
        }

        if (isWin)
        {
            Next.SetActive(true);
            Replay.SetActive(true);
            Retry.SetActive(false);
        }
        else
        {
            Next.SetActive(false);
            Replay.SetActive(false);
            Retry.SetActive(true);
        }
    }

    public void OnRetryClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.NewGame();
        gameObject.SetActive(false);
    }

    public void OnNextClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        int level = SceneManager.instance.CurrentMapData.Level;
        SceneManager.instance.PlayGameController.HidePlayGame();
        if (SceneManager.instance.AllMapData.Count > level)
        {
            SceneManager.instance.CurrentMapData = SceneManager.instance.AllMapData[level];
            SceneManager.instance.LastLevel = SceneManager.instance.CurrentMapData.Level;
            SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.PlayGame, true);
        }
        else
        {
            SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.Level);
        }
        gameObject.SetActive(false);
    }

    public void OnMenuClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.HidePlayGame();
        SceneManager.instance.ModeController.ShowMode();
        gameObject.SetActive(false);
    }
}
