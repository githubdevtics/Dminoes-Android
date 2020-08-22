using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingController : MonoBehaviour {

    private CanvasGroup Loading;
    public Image SliderBar;

    public enum SwitchScene
    {
        Home,
        Level,
        PlayGame,
    }

    void Awake()
    {
        Loading = GetComponent<CanvasGroup>();
    }


    public void ShowLoading(SwitchScene switchTo, bool isLoading = false)
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        SceneManager.instance.LoadNewIntertitialBanner();
        SceneManager.instance.ShowBanner();
        Loading.alpha = 1;
        Loading.blocksRaycasts = true;
        this.gameObject.transform.localPosition = Vector2.zero;
        float timeLoading = switchTo == SwitchScene.Home ? 1f : 1f;

        if (isLoading)
        {
            SceneManager.instance.PlayGameController.NewGame();
            timeLoading = 1f;
        }
        LeanTween.value(0, 1, timeLoading).setOnUpdate(OnValueChange).setOnComplete(s =>
        {
            HideLoading(switchTo);
            if (switchTo != SwitchScene.PlayGame)
            {
                SceneManager.instance.ShowBanner();
            }
            else
            {
                //SceneManager.instance.Banner.HideBanner();
            }
        });
    }

    private void OnValueChange(float _value)
    {
        SliderBar.fillAmount = _value;
    }

    public void HideLoading(SwitchScene switchTo)
    {
        if(switchTo == SwitchScene.Home)
        {
            //SceneManager.instance.HomeController.ShowHome();
            SceneManager.instance.ModeController.ShowMode();
        }
        else if (switchTo == SwitchScene.Level)
        {
            SceneManager.instance.LevelController.ShowLevel();
        }
        else
        {
            SceneManager.instance.PlayGameController.ShowPlayGame(false);
        }

        Loading.alpha = 0;
        Loading.blocksRaycasts = false;
        this.gameObject.transform.localPosition = new Vector2(10000, 10000);
    }
}
