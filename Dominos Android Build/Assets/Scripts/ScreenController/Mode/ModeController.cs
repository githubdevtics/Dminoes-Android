using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour {

    private CanvasGroup Mode;
    public List<Sprite> MainDominoes = new List<Sprite>();
    public List<Sprite> DominosSprites1 = new List<Sprite>();
    public List<Sprite> DominosSprites2 = new List<Sprite>();
    public List<Sprite> DominosSprites3 = new List<Sprite>();
    public List<Sprite> TitleMode = new List<Sprite>();
    public Image Title;
    public GameObject SelectPlayer;
    public GameObject Setting, Continue, Score, QuitGame;
    public ScoreController ScoreControl;
    public bool IsShowScreen;
    public ProfileHandler profileHandler;

    void Awake()
    {
        Mode = GetComponent<CanvasGroup>();
    }

    public void ShowMode()
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        Mode.alpha = 1;
        Mode.blocksRaycasts = true;
        gameObject.transform.localPosition = Vector2.zero;
        SelectPlayer.SetActive(false);
        Continue.transform.localEulerAngles = Vector3.zero;
        Continue.SetActive(false);
        Setting.SetActive(false);
        Score.SetActive(false);
        QuitGame.SetActive(false);
        GetMode();
        SoundManager.instance.SetMusic(0f);
        IsShowScreen = true;

        GetComponent<ProfileHandler>().updateLevelInfo();
        profileHandler.updateLevelInfo();
    }

    public void HideMode()
    {
        IsShowScreen = false;
        Mode.alpha = 0;
        Mode.blocksRaycasts = false;
        gameObject.transform.localPosition = new Vector2(10000, 10000);
    }

    void Update()
    {
        if (IsShowScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Continue.activeSelf)
                {
                    Continue.SetActive(false);
                    SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
                }
                else if (SelectPlayer.activeSelf)
                {
                    OnCloseSelect();
                }
                else if (Setting.activeSelf)
                {
                    Setting.SetActive(false);
                    SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
                }
                else if (Score.activeSelf)
                {
                    Score.SetActive(false);
                    SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
                }
                else if (QuitGame.activeSelf)
                {
                    HideQuitGame();
                }
                else
                {
                    OnBackClick();
                }
            }
        }
    }

    public void GetMode()
    {
        MainDominoes.Clear();
        var mode = PlayerPrefs.GetInt("TILES");
        if (mode == 0)
        {
            foreach(var item in DominosSprites1)
            {
                MainDominoes.Add(item);
            }
        }
        else if (mode == 1)
        {
            foreach (var item in DominosSprites3)
            {
                MainDominoes.Add(item);
            }
        }
        else
        {
            foreach (var item in DominosSprites2)
            {
                MainDominoes.Add(item);
            }
        }
    }

    private int preType = 0;
    public void OnPlayClick(int type)
    {
        Title.sprite = TitleMode[type];
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        preType = type;
        if (PlayerPrefs.GetInt("SAVE_GAME" + type) == 1)
        {
            //Continue.SetActive(true);
            //LeanTween.rotateY(Continue, 1f, 0.05f);
           // return;
        }

        SceneManager.instance.TypePlay = type;
        if (type == 0)
        {
            PlayerPrefs.SetInt("Round" + type, 0);
            PlayerPrefs.SetInt("TotalMe" + type, 0);
            PlayerPrefs.SetInt("TotalOppo" + type, 0);
            SceneManager.instance.MaxPlayer = 2;
            HideMode();
            SceneManager.instance.LevelController.ShowLevel();
        }
        else
        {
            SelectPlayer.SetActive(true);
        }
    }

    public void OnNewGameClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.ResetSave(preType);
        PlayerPrefs.SetInt("Round"+ preType, 0);
        PlayerPrefs.SetInt("TotalMe" + preType, 0);
        PlayerPrefs.SetInt("TotalOppo" + preType, 0);
        OnPlayClick(preType);
        SceneManager.instance.PlayGameController.TypeGame = preType;
        Continue.SetActive(false);
    }

    public void OnContinueClick()
    {
        IsShowScreen = false;
		SceneManager.instance.ShowIntertitialBanner();
        SceneManager.instance.PlayGameController.TypeGame = preType;
        SceneManager.instance.LoadingController.ShowLoading(LoadingController.SwitchScene.PlayGame, true);
        HideMode();
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        Continue.SetActive(false);
        // SceneManager.instance.IsContinue = true;
    }

    public void OnSelectPlayer(bool isTwo)
    {
        SceneManager.instance.PlayGameController.TypeGame = preType;
        SceneManager.instance.MaxPlayer = isTwo ? 2 : 4;
        HideMode();
        SceneManager.instance.PlayGameController.RandomName();
        SceneManager.instance.PlayGameController.ShowPlayGame();
        SelectPlayer.SetActive(false);
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void OnCloseSelect()
    {
        SelectPlayer.SetActive(false);
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void OnCloseSetting()
    {
        Setting.SetActive(false);
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void OnBackClick()
    {
        HideMode();
        SceneManager.instance.HomeController.ShowHome();
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void OnSettingClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        Setting.SetActive(true);
    }

    public void OnScoreClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        ScoreControl.InitBoard();
        Score.SetActive(true);
    }

    public void ShowQuitGame()
    {
        Setting.SetActive(false);
        QuitGame.SetActive(true);
    }

    public void HideQuitGame()
    {
        QuitGame.SetActive(false);
        Setting.SetActive(true);
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }
}
