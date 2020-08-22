using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseGameController : MonoBehaviour
{
    public List<Sprite> Number;
    public Image CheckBox, Checker;
    public Sprite BoxOn, BoxOff;

    public void OnClickTipChecker()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.SetTip();
    }

	public void OnResumeClick()
    {
        this.gameObject.SetActive(false);
        SceneManager.instance.HideBanner();
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
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
