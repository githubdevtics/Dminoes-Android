using UnityEngine;
using System.Collections.Generic;
using System.IO;
using AOT;
public class SceneManager : MonoBehaviour
{
    const int LevelMax = 300;
    public List<string> Tips = new List<string>();

    public const string USER_DATA = "USER_DATA";
    public const string TIP_DATA = "TIP_DATA";
    public const string RATE_DATA = "RATE_DATA";
    public const string LEVEL_RATING = "LEVEL_RATING";

    private float widthSize = 0f;

    public MapData CurrentMapData;
    public List<MapData> AllMapData = new List<MapData>();
    public List<string> MapsData = new List<string>();
    public int MaxLevel = 0;
    public int LastLevel = 0;

    //public AdMobBanner Banner;
    //public AdMobBannerInterstitial InterStitial;
    public float TimeShowInterstitial = 90f;
    private float _preTimeShowAds = 0f;
    bool isShowInterstitial = true;

    public int TypePlay = 0;
    public int MaxPlayer = 2;

    public RateGameController Rate;
    private float TimeShowRate = 0f;

    public bool IsInGame = false;
    public float Ratio = 0;
    public bool IsContinue = false;

    public static SceneManager instance;
    void Awake()
    {
        MaxLevel = 0;
        //var temp = Screen.height / 1280f;
        //Ratio = Screen.width / (720f * temp);
        Ratio = 1f;
        SceneManager.instance = this;
        TimeShowRate = 0f;
        if (!PlayerPrefs.HasKey(RATE_DATA))
        {
            PlayerPrefs.SetInt(RATE_DATA, 0);
            PlayerPrefs.SetInt(LEVEL_RATING, 0);
        }

        if (!PlayerPrefs.HasKey(USER_DATA))
        {
            CreateNewDataUser();
        }
        GetMapData();
        LoadUserData();

        //GoogleMobileAd.OnInterstitialClosed += OnHideIntertitialBanner;
        //GoogleMobileAd.OnInterstitialLoaded += OnLoadIntertitialBannerSuccess;
    }

    void FixedUpdate()
    {
        if (TimeShowRate < 10000)
        {
            TimeShowRate += Time.deltaTime;
        } else
        {
            TimeShowRate = 0;
        }
    }

    public void ShowRate()
    {
        if (TimeShowRate >= 250f)
        {
            TimeShowRate = 0;
            Rate.gameObject.SetActive(true);
        }
    }

	public void ShowRatePuzzle()
	{
		if (TimeShowRate >= 200f)
		{
			TimeShowRate = 0;
			Rate.gameObject.SetActive(true);
		}
	}

    bool isLoadBanner = false;
    public void OnLoadIntertitialBannerSuccess()
    {
        isLoadBanner = true;
    }

    public int CurrentTotalStar()
    {
        int _currentStar = 0;
        foreach(var item in AllMapData)
        {
            _currentStar += item.Star;
        }

        return _currentStar;
    }

    public void LoadNewIntertitialBanner()
    {
        //GoogleMobileAd.LoadInterstitialAd();
    }

    public enum ObjectManager
    {
        MainMenu,
        Message,
        PlayGame,
        Option,
    }

    public enum ObjectDialog
    {
        WinLose,
    }


    public enum TypeInit
    {
        Immediately,
        Delays,
    }

    private GameObject preScene;
    private GameObject preDialog;

    public LoadingController LoadingController;
    public HomeController HomeController;
    public LevelController LevelController;
    public PlayGameController PlayGameController;
    public ModeController ModeController;
    public RulesController RulesController;

    public LeaderBoardManager leaderBoardManager;


    void Start()
    {
        // Start game UI
        LoadingController.ShowLoading(LoadingController.SwitchScene.Home);
        HomeController.HideHome();
        LevelController.HideLevel();
        PlayGameController.HidePlayGame();
        ModeController.HideMode();
        RulesController.HideMode();
        var IsSoundOn = !PlayerPrefs.HasKey("SOUND") || PlayerPrefs.GetInt("SOUND") == 1;
        SoundManager.instance.SetMusic(IsSoundOn ? 100f : 0f);
    }

    void CreateNewDataUser()
    {
        string userData = "";
        // => lock => star => score
        for (int j = 0; j < MaxLevel; j++)
        {
            if (j != 0)
            {
                userData += "+0-0-0";
            }
            else
            {
                userData += "+1-0-0";
            }
        }
        PlayerPrefs.SetString(USER_DATA, userData);
    }

    void LoadUserData()
    {
        try
        {
            string dataLoader = PlayerPrefs.GetString(USER_DATA);

            var level = dataLoader.Split('+');
            for (int j = 1; j < level.Length; j++)
            {
                var data = level[j].Split('-');
                MapData newLevel = new MapData(j, int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));
                AllMapData.Add(newLevel);
                if(!newLevel.IsLock)
                {
                    LastLevel = newLevel.Level;
                }
            }

            for (int i = level.Length; i <= MaxLevel;i++)
            {
                int isLock = 0;
                if(AllMapData[i - 2].IsLock == false 
                    && AllMapData[i - 2].Score > 0)
                {
                    isLock = 1;
                }
                MapData newLevel = new MapData(i, isLock, 0, 0);
                AllMapData.Add(newLevel);
            }
        }
        catch
        {
            CreateNewDataUser();
            LoadUserData();
        }
    }

    public void SavePoint()
    {
        for (int i = 0; i < AllMapData.Count; i++)
        {
            if (AllMapData[i].Level == CurrentMapData.Level)
            {
                AllMapData[i] = CurrentMapData;
                if(i + 1 < AllMapData.Count)
                {
                    AllMapData[i + 1].IsLock = false;
                }
                break;
            }
        }
        SaveData();
    }

    void SaveData()
    {
        string userData = "";
        // => lock => star => score
        for (int j = 0; j < AllMapData.Count; j++)
        {
            int isLock = 1;
            if (AllMapData[j].IsLock)
                isLock = 0;

            userData += "+" + isLock + "-" + 
                AllMapData[j].Star + "-" + AllMapData[j].Score;
        }
        PlayerPrefs.SetString(USER_DATA, userData);
        if (CurrentMapData != null && CurrentMapData.Level >= PlayerPrefs.GetInt("LevelMax"))
        {
            PlayerPrefs.SetInt("LevelMax", CurrentMapData.Level);
        }
    }

    void GetMapData()
    {
        try
        {
            MapsData.Clear();
            MaxLevel = LevelMax;
            for (int i = 0; i < LevelMax; i++)
            {
                MapsData.Add(i.ToString());
            }

        }
        catch(IOException e)
        {
        }
    }

    public bool IsOffTip()
    {
        if(!PlayerPrefs.HasKey(TIP_DATA))
        {
            PlayerPrefs.SetString(TIP_DATA, "FALSE");
            return true;
        }

        if (PlayerPrefs.GetString(TIP_DATA) == "TRUE")
        {
            return true;
        }

        return false;
    }

    public void SetTip()
    {
        if (IsOffTip())
        {
            PlayerPrefs.SetString(TIP_DATA, "FALSE");
        }
        else
        {
            PlayerPrefs.SetString(TIP_DATA, "TRUE");
        }
    }

    public void ShowBanner()
    {
        //Banner.ShowBanner();
    }

    public void HideBanner()
    {
        //Banner.HideBanner();
    }

    public void ShowIntertitialBanner()
    {
        if (isLoadBanner && isShowInterstitial)
        {
            //Banner.HideBanner();
            //InterStitial.ShowBanner();
			LoadNewIntertitialBanner();
        }
    }

    public bool CanShowIntertitialBanner()
    {
        return isLoadBanner && isShowInterstitial;
    }

    public bool LoadToLevelScene = false;

    public void OnHideIntertitialBanner()
    {
        ShowBanner();
        isShowInterstitial = false;
        isLoadBanner = false;
        LoadNewIntertitialBanner();
        if(LoadToLevelScene)
        {
            LoadToLevelScene = false;
            HomeController.HideHome();
            ModeController.ShowMode();
        }
    }

    void Update()
    {
        if(!isShowInterstitial)
        {
            _preTimeShowAds += Time.deltaTime;
            if(_preTimeShowAds >= TimeShowInterstitial)
            {
                _preTimeShowAds = 0;
                isShowInterstitial = true;
            }
        }
    }

    public int TotalStar()
    {
        int star = 0;

        foreach (var data in AllMapData)
            star += data.Star;

        return star;
    }

    public void GetCurrentMapData(int level)
    {
        foreach(var item in AllMapData)
        {
            if (item.Level == level)
            {
                CurrentMapData = item;
            }
        }
    }
}
