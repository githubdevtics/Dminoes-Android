using UnityEngine;
using System.Collections;

public class HomeController : MonoBehaviour {

    private CanvasGroup Home;
    public bool IsShowScreen;
    public GameObject QuitGame;
	public GameObject QuitGame2;

    void Awake()
    {
        Home = GetComponent<CanvasGroup>();
    }


    public void ShowHome()
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        IsShowScreen = true;
        Home.alpha = 1;
        Home.blocksRaycasts = true;
        this.gameObject.transform.localPosition = Vector2.zero;
        var IsSoundOn = !PlayerPrefs.HasKey("SOUND") || PlayerPrefs.GetInt("SOUND") == 1;
        SoundManager.instance.SetMusic(IsSoundOn ? 100f : 0f);
    }

    void Update()
    {
        if (IsShowScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
				SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
                if (QuitGame2.gameObject.activeSelf)
                {
                    HideQuitGame();
                }
                else
                    QuitGame2.SetActive(true);
            }

        }
    }

    public void HideHome()
    {
        IsShowScreen = false;
        Home.alpha = 0;
        Home.blocksRaycasts = false;
        this.gameObject.transform.localPosition = new Vector2(10000, 10000);
    }

    public void OnPlayClick()
    {
        if (SceneManager.instance.CanShowIntertitialBanner())
        {
            SceneManager.instance.LoadToLevelScene = true;
            SceneManager.instance.ShowIntertitialBanner();
        }
        else
        {
            LoadMode();
        }
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void LoadMode()
    {
        HideHome();
        SceneManager.instance.ModeController.ShowMode();
    }

    public void OnMoreGameClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
#if UNITY_ANDROID
        Application.OpenURL("market://search?id=" + Application.companyName + "");
#endif
    }

    public void HideQuitGame()
    {
        QuitGame.SetActive(false);
    }

	public void HideQuitGame2()
	{
		SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
		QuitGame2.SetActive(false);
	}

    public void OnQuitGameClick()
    {
		SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        Application.Quit();
    }

    public void OnRuleClick()
    {
		SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        HideHome();
        SceneManager.instance.RulesController.ShowMode();
    }
}
