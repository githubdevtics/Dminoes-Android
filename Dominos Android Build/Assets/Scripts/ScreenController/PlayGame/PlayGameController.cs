using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PlayGameController : MonoBehaviour
{
    // for check hint - new logic
    public bool StartChecking = false;

    public Sprite BorderBlack, BorderYellow;
    public Sprite White, Black;
    private CanvasGroup PlayGame;
    public List<Sprite> DominosSprites = new List<Sprite>();
    public Image Sound;
    public Sprite SoundOn, SoundOff;
    public Image Bg;
    public List<Sprite> BgSprite = new List<Sprite>();
    public GameObject DominoesHolder;
    public GameObject TimeBar;
    public List<GameObject> PosTimeBar = new List<GameObject>();
    public List<GameObject> PassObject = new List<GameObject>();
    public List<Sprite> YouHim = new List<Sprite>();
    public Image You, Him;
    public GameObject AllFive;
    public Text AllFiveText;
    public int CurAllFive = 0;
	public bool ismoved = false; 
    public GridLayoutGroup ExtraBoxLayout;
    public List<Sprite> Number = new List<Sprite>();
    public List<Image> Me = new List<Image>();
    public List<Image> Oppo = new List<Image>();
    public List<Image> AnimAllFive = new List<Image>();
    public int MePoint = 0;
    public int OppoPoint = 0;
    public int TotalMe = 0;
    public int TotalOppo = 0;
    public int Round = 0;
    public GameObject CenterObj;
	public string localdrawdomino;
	public string localallfivedomino;

    public GameObject AnimAllFiveObj;

    public List<DominoController> AllDominos = new List<DominoController>();
    public int type = 0;
    private List<int> DominoesValue = new List<int>();

    public List<GameObject> PlayerObj = new List<GameObject>();
    public List<List<DominoController>> PlayerDominoes = new List<List<DominoController>>();
    public List<DominoController> RemainDominoes = new List<DominoController>();

    public GameObject Board, Center, Next, Center2;
    public List<HintController> Hint = new List<HintController>();

    public int FirstPlayer = 0;
	//public bool isClickableLeave = false;
    public int CurrentTurn = 0;
    public int CurrentMaxPlayer = 0;
    private Vector3 center, center2;

    private float widthDefault = 80f;
    private float heightDefault = 160f;

    private float widthPerDomino = 80f;
    private float heightPerDomino = 160f;

    public DominoController LeftDomino;
    public DominoController RightDomino;
    public DominoController UpDomino;
    public DominoController DownDomino;
    public DominoController CenterDomino;

    public DominoController CurrentDomino;
    public bool isBlock = false;
    private bool isVerticalOnly = false;
    private List<int> ActiveHint = new List<int>();
    private List<int> ScaleWidth = new List<int>()
    {
        3, 5, 7,9, 11, 13
        //4, 7,9, 12, 15
        //4, 7, 13
    };
    private List<float> ScalePerCent = new List<float>()
    {
        1f, 1f, 0.85f,0.75f, 0.5f, 0.4f        
        //1f, 0.85f,0.65f, 0.5f, 0.4f
        //1f, 0.75f, 0.4f
    };
    private List<float> ScalePlayerObj = new List<float>()
    {
        1f, 0.9f, 0.85f, 0.75f, 0.7f, 0.65f, 0.6f, 0.55f
    };


    private int CurrentScaleWidth = 0;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isUp = false;
    private bool isDown = false;

    private float minHeight = 0f;
    public int Type = 0;
    public GameObject ExtraBox;
    public CanvasGroup DialogExtra;

    public SettingController Setting;
    public ResultController Result;
    public PuzzleController Puzzle;
    public EndMatchController EndMatch;
    public int PlayerScore, OppoScore;
    private bool IsSoundOn;
    private bool isLoading = false;

    public float TimeAnim = 0.5f;

    public List<int> LeftStyle = new List<int>();
    public List<int> RightStyle = new List<int>();
    public List<int> UpStyle = new List<int>();
    public List<int> DownStyle = new List<int>();
    DominoController LeftSide;
    DominoController RightSide;
    DominoController UpSide;
    DominoController DownSide;
    public GameObject KeepObject;

    public const int MaxSide = 7;
    private bool endGameByPoint = false;
    bool isFirstMatch = true;
    public Image ClickingObj;
    public GameObject Icon;

    public string Name1, Name2, Name3;
    public List<string> NameBot = new List<string>();
    public bool IsShowScreen;

    public GameObject QuitGame;

    public int TypeGame = 0;

    //graphics to show data
    public RectTransform bot1Big, bot1Small, bot2, bot3;
    public Image playerTurnCircle, bot1BigTurnCircle, bot1SmallTurnCircle, bot2TurnCircle, bot3TurnCircle;
    public Text playerNameText, bot1BNameText, bot1SNameText, bot2NameText, bot3NameText;
    public Image  bot1BRankImage;
    public Text  bot1BRankText;
    public float bot1RankImagFill, bot2RankImagFill, bot3RankImagFill;
    public int bot1Rank, bot2Rank, bot3Rank;
    public Image bot1BImage, bot1SImage, Bot2Image, Bot3Image;
    public Sprite bot1Sprite, bot2Sprite, bot3Sprite;
    public Sprite[] allBotImages;

    //board Animation
    bool boardAnimating = false;
    Vector3 newTargetScale = Vector3.one;
    float newDominoWidth = 1f;
    float newDominoHight = 1f;

    //User Data
    public Text playerNickNameText;
    public Image playerAvatarImage;
    public Sprite[] playerAvatarSprites;
    public Text boneyardText;
    public GameObject boneyardGo;

    public Text playerScoreText;
    public Text bot1BigScoreText;
    public Text bot1SmallScoreText;
    public Text bot2ScoreText;
    public Text bot3ScoreText;

    public int targetScore;

    public Image playerTargetForFive, enemyTargetForFive;

    public Text rankText;
    public Image rankFillImage;

    public LeaderBoardManager leaderBoardManager;

    //kha edit
    public int player1Score = 0, bot1Score = 0, bot2Score = 0, bot3Score = 0;
    public int totalPlayerScore = 0, totalBot1Score = 0, totalBot2Score = 0, totalBot3Score;
    public void resetTotal()
    {
        totalPlayerScore = 0;
        totalBot1Score = 0;
        totalBot2Score = 0;
        totalBot3Score = 0;
    }
    public void updateTotal()
    {
        //if (type != 2)
        //{
            totalPlayerScore += player1Score;
            totalBot1Score += bot1Score;
            if (CurrentMaxPlayer == 4)
            {
                totalBot2Score += bot2Score;
                totalBot3Score += bot3Score;
            }
        //}
    }
    


    void Awake()
    {
        PlayGame = GetComponent<CanvasGroup>();
    }

    public void ShowPlayGame(bool isNormal = true)
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        KeepObject.transform.localScale = new Vector3(1/SceneManager.instance.Ratio, 1f, 1f);
        IsShowScreen = true;
        PlayGame.alpha = 1;
        PlayGame.blocksRaycasts = true;
        gameObject.transform.localPosition = Vector2.zero;
        QuitGame.SetActive(false);
        IsSoundOn = !PlayerPrefs.HasKey("SOUND") || PlayerPrefs.GetInt("SOUND") == 1;
        Sound.sprite = IsSoundOn ? SoundOn : SoundOff;
        SoundManager.instance.SetMusic(0f);
        ChangeBg();
        SceneManager.instance.IsInGame = true;

        if (isNormal)
        {
            isFirstMatch = true;
            FirstPlayer = 0;
            TotalMe = 0;
            TotalOppo = 0;

            resetTotal();

            Round = 0;
            //RandomName();
            NewGame();
        }
        ChangeTiles();
    }

    public void HidePlayGame()
    {
        IsShowScreen = false;
		PlayGame.alpha = 0;
        PlayGame.blocksRaycasts = false;
        gameObject.transform.localPosition = new Vector2(10000, 10000);
        SoundManager.instance.SetMusic(IsSoundOn ? 100f : 0f);
        LeanTween.cancel(this.gameObject);
        StopCoroutine("OnNextTurnWithDelay");
        StopCoroutine("DelayStartGame");
        StopAllCoroutines();
    }

    public void loadUserData()
    {
        player1Score = 0;
        bot1Score = 0;
        bot2Score = 0;
        bot3Score = 0;

        playerNickNameText.text = PlayerPrefs.GetString("nick-name", "Nick Name");
        playerAvatarImage.sprite = playerAvatarSprites[PlayerPrefs.GetInt("avatar-number", 0)];
        //Google Leaderboard
        playerNickNameText.text = leaderBoardManager.getUserNameLeaderBoard();
        playerAvatarImage.sprite = leaderBoardManager.loadedSprite != null ? leaderBoardManager.loadedSprite : leaderBoardManager.sprite;
        //StartCoroutine( leaderBoardManager.getUserImageLeaderRoutine(playerAvatarImage));

        targetScore = PlayerPrefs.GetInt("target-score", 50);

        playerScoreText.text = totalPlayerScore + "/" + targetScore;

        levelCalculator lc = GameObject.FindObjectOfType<levelCalculator>();
        rankText.text = lc.getLevel() + "";
        rankFillImage.fillAmount = lc.calculateLevel();

    }

    public void NewGame()
    {
        loadUserData();

        Result.gameObject.SetActive(false);
        EndMatch.gameObject.SetActive(false);
        Puzzle.gameObject.SetActive(false);
        ActiveExtra(false);
        PlayerObj[0].GetComponent<RectTransform>().sizeDelta = new Vector2(720f, 242f);
        PlayerObj[0].GetComponent<RectTransform>().localScale = Vector3.one;
        StopCoroutine("OnNextTurnWithDelay");
        StopCoroutine("DelayStartGame");
        StopCoroutine("Find");
        LeftStyle.Clear();
        LeftStyle.Add(0);
        RightStyle.Clear();
        RightStyle.Add(0);
        UpStyle.Clear();
        UpStyle.Add(0);
        DownStyle.Clear();
        DownStyle.Add(0);
        CurAllFive = 0;
		//isClickableLeave = false;
		if (FirstPlayer == 0) {
			ismoved = true; 
		} else {
			ismoved = false;
		}
        gameObject.transform.localPosition = Vector2.zero;
        ChangeSpeed();
        MePoint = TotalMe;
        OppoPoint = TotalOppo;
        foreach (var item in PassObject)
        {
            item.SetActive(false);
        }
        LeanTween.cancelAll();
        Type = SceneManager.instance.TypePlay;
        SetPoint();
        isLeft = false;
        isRight = false;
        isDown = false;
        isUp = false;
        CurrentScaleWidth = 0;
        Board.transform.localScale = Vector3.one;
        isVerticalOnly = false;
        isBlock = false;
        center = Center.transform.position;
        center2 = Center2.transform.position;
        widthPerDomino = Next.transform.position.x - center.x;
        heightPerDomino = widthPerDomino * 2;

        widthDefault = widthPerDomino;
        heightDefault = heightPerDomino;

        CurrentTurn = 0;
        type = SceneManager.instance.TypePlay;

       // RePosTimeBar();

        AnimAllFiveObj.transform.position = new Vector3(10000f, 10000f, 0f);

        DominosSprites.Clear();
        foreach (var item in SceneManager.instance.ModeController.MainDominoes)
            DominosSprites.Add(item);
        ExtraBoxLayout.enabled = true;
        if (PlayerPrefs.GetInt("SAVE_GAME" + TypeGame) == 0)
        {
			SceneManager.instance.ShowIntertitialBanner();
            CurrentMaxPlayer = SceneManager.instance.MaxPlayer;

            if (CurrentMaxPlayer == 2)
            {
                You.sprite = YouHim[0];
                Him.sprite = YouHim[1];
                You.SetNativeSize();
                Him.SetNativeSize();

                bot1Small.gameObject.SetActive(false);
                bot2.gameObject.SetActive(false);
                bot3.gameObject.SetActive(false);
                bot1Big.gameObject.SetActive(true);

                //bot1BigScoreText.text = TotalOppo + "/" + targetScore;
                bot1BigScoreText.text = totalBot1Score + "/" + targetScore;


            }
            else
            {
                You.sprite = YouHim[2];
                Him.sprite = YouHim[3];
                You.SetNativeSize();
                Him.SetNativeSize();

                
                bot1Small.gameObject.SetActive(true);
                bot2.gameObject.SetActive(true);
                bot3.gameObject.SetActive(true);
                bot1Big.gameObject.SetActive(false);

                bot1SmallScoreText.text = totalBot1Score + "/" + targetScore;
                bot2ScoreText.text = totalBot2Score + "/" + targetScore;
                bot3ScoreText.text = totalBot3Score + "/" + targetScore;
            }

            if (Type == 0)
            {
                You.sprite = YouHim[4];
                Him.sprite = YouHim[5];
                You.SetNativeSize();
                Him.SetNativeSize();

                int currentLevel = SceneManager.instance.LastLevel;
                if (currentLevel < 50)
                {
                    CurrentMaxPlayer = 2;
                }
                else if (currentLevel < 100)
                {
                    CurrentMaxPlayer = 4;
                }
                else
                {
                    var ran = Random.Range(0, 2);
                    CurrentMaxPlayer = ran == 0 ? 2 : 4;
                }

                foreach (var item in Me)
                {
                    item.sprite = null;
                }

                var point = currentLevel.ToString();
                for (int i = 0; i < point.Length; i++)
                {
                    Me[i].sprite = Number[int.Parse(point[i].ToString())];
                }
            }

            if (Type == 0 || Type == 2)
            {
                AllFive.SetActive(true);
                AllFiveText.text = CurAllFive + " points";
            }
            else
            {
                AllFive.SetActive(false);
            }
            Round++;
            if (Type != 0)
            {
                PlayerPrefs.SetInt("Round" + TypeGame, Round);
                PlayerPrefs.SetString("Name" + TypeGame, Name1 + "-" + Name2 + "-" + Name3);
            }

            if (Type == 1 || Type == 3)
            {
                MePoint = TotalMe;
                OppoPoint = TotalOppo;
                SetPoint();
                MePoint = 0;
                OppoPoint = 0;
            }
            InitValue();
            Sharkler();
			if (Round == 1 || Round == 0 || Type == 2)
            {
                CheckFirstPlayer();
            }
            else
            {
                maxDomino = null;
            }
			RePosTimeBar();
            //SaveGame();
            StopCoroutine("ShowPassObject");
            isBlock = true;
            StartCoroutine("DelayStartGame", 1f);

        }
        else
        {
            isLoading = true;
            NewGame();
        }
		// change-tan-1 - 1 line
        Round = PlayerPrefs.GetInt("Round" + TypeGame);
        //RandomName();
		if (Type != 0 && Round != 1 )
        {
            TotalMe = PlayerPrefs.GetInt("TotalMe" + TypeGame);
            TotalOppo = PlayerPrefs.GetInt("TotalOppo" + TypeGame);
		}


        boneyardGo.gameObject.SetActive(true);
        boneyardText.text = RemainDominoes.Count + "";

        if (type == 3)
        {
            boneyardGo.gameObject.SetActive(false);
        }
    }

    private DominoController maxDomino;
    void CheckFirstPlayer()
    {
        maxDomino = PlayerDominoes[0][0];
        var index = 0;
        foreach (var item in PlayerDominoes)
        {
            foreach (var domino in item)
            {
                if (domino.Up == domino.Down)
                {
                    if (maxDomino.Up == maxDomino.Down)
                    {
                        if (domino.Up > maxDomino.Up)
                        {
                            maxDomino = domino;
                            FirstPlayer = index;
                        }
                    }
                    else
                    {
                        maxDomino = domino;
                        FirstPlayer = index;
                    }
                }
                else
                {
                    if (maxDomino.Up == maxDomino.Down)
                    {

                    }
                    else
                    {
                        if (domino.Up + domino.Down > maxDomino.Up + maxDomino.Down)
                        {
                            maxDomino = domino;
                            FirstPlayer = index;
                        }
                    }
                }
            }
            index++;
        }
    }

    IEnumerator DelayStartGame(float time)
    {
        yield return new WaitForSeconds(time);
        ExtraBoxLayout.enabled = false;
        StartCoroutine("OnNextTurnWithDelay");
    }

    private void InitValue()
    {
        foreach (var item in AllDominos)
        {
            item.transform.localPosition = new Vector3(10000, 10000, 0f);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
        }

        DominoesValue.Clear();
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                DominoesValue.Add(i * 10 + j);
            }
        }

        for (int i = DominoesValue.Count - 1; i >= 0; i--)
        {
            var temp = Random.Range(0, DominoesValue.Count);
            AllDominos[i].InitDomino(temp, DominoesValue[temp] % 10, DominoesValue[temp] / 10);
            AllDominos[i].GetComponent<Button>().interactable = false;
            DominoesValue.RemoveAt(temp);
            DominosSprites.RemoveAt(temp);
        }

    }

    private void Sharkler()
    {
        ResetHint();
        LeftDomino = null;
        RightDomino = null;
        UpDomino = null;
        DownDomino = null;
        RemainDominoes.Clear();
        PlayerDominoes.Clear();
        for (int i = 0; i < CurrentMaxPlayer; i++)
        {
            PlayerDominoes.Add(new List<DominoController>());
        }
		Debug.Log ("PlayerDominosCount:" + PlayerDominoes.Count);
		for (int i = 0; i < 28; i++) {
			Debug.Log(AllDominos[i].Up + "-" + AllDominos[i].Down + "-----");
		}

        if (CurrentMaxPlayer == 2)
        {
			
            for (int i = 0; i < 7; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[0].transform);
                AllDominos[i].ShowDominos();
                AllDominos[i].isMine = true;
                PlayerDominoes[0].Add(AllDominos[i]);
            }


            for (int i = 7; i < 14; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[1].transform);
                AllDominos[i].rectTransform.anchoredPosition = Vector3.zero;
                PlayerDominoes[1].Add(AllDominos[i]);
            }

            for (int i = 14; i < 28; i++)
            {
                RemainDominoes.Add(AllDominos[i]);
                AllDominos[i].transform.SetParent(ExtraBox.transform);
                AllDominos[i].IsExtra = true;
                AllDominos[i].GetComponent<Button>().interactable = true;
            }
            

        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[0].transform);
                PlayerDominoes[0].Add(AllDominos[i]);
                AllDominos[i].isMine = true;
                AllDominos[i].ShowDominos();
            }
				
            for (int i = 5; i < 10; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[1].transform);
                AllDominos[i].rectTransform.anchoredPosition = Vector3.zero;
                PlayerDominoes[1].Add(AllDominos[i]);

            }



            for (int i = 10; i < 15; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[2].transform);
                AllDominos[i].rectTransform.anchoredPosition = Vector3.zero;
                //AllDominos[i].Rotate(90);
                PlayerDominoes[2].Add(AllDominos[i]);

            }

            for (int i = 15; i < 20; i++)
            {
                AllDominos[i].transform.SetParent(PlayerObj[3].transform);
                AllDominos[i].rectTransform.anchoredPosition = Vector3.zero;
                //AllDominos[i].Rotate(90);
                PlayerDominoes[3].Add(AllDominos[i]);
            }
				

            for (int i = 20; i < 28; i++)
            {
                RemainDominoes.Add(AllDominos[i]);
                AllDominos[i].transform.SetParent(ExtraBox.transform);
                AllDominos[i].IsExtra = true;
                AllDominos[i].GetComponent<Button>().interactable = true;
            }
            boneyardText.text = RemainDominoes.Count + "";
        }
    }

    IEnumerator OnNextTurnWithDelay()
    {
        //if (IsShowScreen)
        {
            bool isCheckedBlock = false;
            if (CheckNoMove())
            {
                if (FirstPlayer == 0)
                {
                    if (RemainDominoes.Count > 0 && Type != 3)
                    {
                        ActiveExtra(true);
                    }
                    else
                    {
                        StartCoroutine("ShowPassObject");
                    }
                }
                else
                {
                    if (Type != 3)
                    {
                        while (CheckNoMove() && RemainDominoes.Count > 0)
                        {
                            var random = Random.Range(0, RemainDominoes.Count);
                            PlayerDominoes[FirstPlayer].Add(RemainDominoes[random]);
                            RemainDominoes[random].IsExtra = false;
                            RemainDominoes[random].transform.SetParent(CenterObj.transform);
                            RemainDominoes[random].transform.localPosition = CenterObj.transform.position;
                            if (FirstPlayer >= 2)
                            {
                                //RemainDominoes[random].Rotate(90f);
                            }
                            var isWaiting = false;
                            LeanTween.move(RemainDominoes[random].gameObject, PlayerObj[FirstPlayer].transform.position, TimeAnim * 2).setOnComplete(s =>
                               {
                                   if (RemainDominoes.Count > random)
                                   {
                                       RemainDominoes[random].transform.SetParent(PlayerObj[FirstPlayer].transform);
                                       //RemainDominoes[random].rectTransform.anchoredPosition = Vector3.zero; //kha
                                       RemainDominoes[random].GetComponent<Button>().interactable = false;
                                       AddDomino(RemainDominoes[random]);
                                       RemainDominoes.RemoveAt(random);
                                   }
                                   isWaiting = true;
                               });
                            while (!isWaiting)
                                yield return null;
                        }
                    }

                    if (CheckNoMove())
                    {
                        isCheckedBlock = true;
                       StartCoroutine("ShowPassObject");
                    }
                }
            }

            if (CurrentTurn > 0)
                isVerticalOnly = false;

            if (!isLoading && !isCheckedBlock)
                isBlock = false;

			if ((CurrentTurn == 0 && (Round == 1 || Round == 0)) || (CurrentTurn == 0) && (Type == 2) )
            {
                var index = 0;
                DominoController temp = new DominoController();
                foreach (var item in PlayerDominoes[FirstPlayer])
                {
                    if (item.Up == item.Down)
                    {
                        index++;
                        temp = item;
                    }
                    if (maxDomino != null && item.name == maxDomino.name)
                    {
                        temp = item;
                        index = 0;
                        break;
                    }
                }

				if ((index == 1 && temp != null) || ((Round == 1 || Round == 0) || (Type == 2) && temp != null && maxDomino != null && maxDomino.Up == maxDomino.Down))
                {
                    //foreach (var item in PlayerDominoes[FirstPlayer])
                    //{
                    //    item.GetComponent<Button>().interactable = true;
                    //}
                    CenterDomino = temp;

                    LeftSide = CenterDomino;
                    RightSide = CenterDomino;
                    UpSide = CenterDomino;
                    DownSide = CenterDomino;

                    MoveDominoes(temp, center2, 90f);
                    if (!isLoading)
                    {
                        SavePerDominoClick(CenterDomino, 0);
                    }
                    CurrentDomino = temp;
                    LeftDomino = temp;
                    LeftDomino.ConnectPoint = temp.Up;
                    RightDomino = temp;
                    RightDomino.ConnectPoint = temp.Up;
                    UpDomino = temp;
                    UpDomino.ConnectPoint = temp.Up;
                    DownDomino = temp;
                    DownDomino.ConnectPoint = temp.Up;
                }
                else if (FirstPlayer == 0)
                {
                    foreach (var item in PlayerDominoes[FirstPlayer])
                    {
                        item.GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    AiMove();
                }
            }
            else if (FirstPlayer == 0)
            {
                foreach (var item in PlayerDominoes[FirstPlayer])
                {
                    item.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                AiMove();
            }
				

        }

        boneyardText.text = RemainDominoes.Count + "";
    }

    IEnumerator ShowPassObject()
    {
        isBlock = true;
        PassObject[FirstPlayer].SetActive(true);
        yield return new WaitForSeconds(1f);
        PassObject[FirstPlayer].SetActive(false);
        if (PlayerDominoes[FirstPlayer].Count == 0)
        {
            ShowEnd();
        }
        else
        {
            CheckNextTurn();
        }
    }

    private void MoveDominoes(DominoController temp, Vector2 pos, float zIndex)
    {
        temp.IsChecked = true;
        temp.Chess.color = new Color(1, 1, 1, 1);
        isBlock = true;
        temp.transform.SetParent(Board.transform);
        temp.ShowDominos();
        temp.Rotate(zIndex);
        PlayerDominoes[FirstPlayer].Remove(temp);
        SetPlayerObj();
        var time = isLoading ? 0f : TimeAnim;
        
        LeanTween.move(temp.gameObject, pos, time * 2).setOnComplete(s =>
        {	
			if (FirstPlayer == 0) {
					ismoved = true ; 		
			}	
            CheckStyle(temp);
            RecenterNormal();
            ReScaleBoard();
            if (isLoading)
                isBlock = false;
            if (PlayerDominoes[FirstPlayer].Count == 0)
            {
                ShowEnd();
            }
            else
            {
                CurrentDomino = temp;
                CheckNextTurn();
            }
        });

        LeanTween.scale(temp.gameObject, Vector3.one, time);
    }

    private void CheckStyle(DominoController temp)
    {
        if (temp.transform.position.x < LeftSide.transform.position.x)
        {
            if ((isLeft && LeftStyle.Count - 1 == 0)
                || (isRight && RightStyle.Count - 1 == 2)
                || (isDown && DownStyle.Count - 1 == 1)
                || (isUp && UpStyle.Count - 1 == 3))
            {
                LeftSide = temp;
            }
        }

        if (temp.transform.position.x > RightSide.transform.position.x)
        {
            if ((isLeft && LeftStyle.Count - 1 == 2)
                || (isRight && RightStyle.Count - 1 == 0)
                || (isDown && DownStyle.Count - 1 == 3)
                || (isUp && UpStyle.Count - 1 == 1))
            {
                RightSide = temp;
            }
        }

        if (temp.transform.position.y < DownSide.transform.position.y)
        {
            if ((isLeft && LeftStyle.Count - 1 == 3)
                || (isRight && RightStyle.Count - 1 == 1)
                || (isDown && DownStyle.Count - 1 == 0)
                || (isUp && UpStyle.Count - 1 == 2))
            {
                DownSide = temp;
            }
        }

        if (temp.transform.position.y > UpSide.transform.position.y)
        {
            if ((isLeft && LeftStyle.Count - 1 == 1)
                || (isRight && RightStyle.Count - 1 == 3)
                || (isDown && DownStyle.Count - 1 == 2)
                || (isUp && UpStyle.Count - 1 == 0))
            {
                UpSide = temp;
            }
        }

        if (isLeft)
        {
            var length = LeftStyle.Count - 1;

            if (temp.Up == temp.Down)
            {
                if (length == 0)
                {
                    LeftStyle[length] += 1;
                }
                else
                {
                    LeftStyle[length] += 2;
                }
            }
            else
            {
                LeftStyle[length] += 2;
            }

            if ((length == 0 && LeftStyle[0] >= 5) 
                || (length == 1 && LeftStyle[1] >= 6)
                || (length == 2 && LeftStyle[2] >= 11)
                || (length == 3))
            {
                LeftStyle.Add(0);
            }

            LeftDomino = temp;
            var zIndex = Hint[0].transform.GetChild(0).transform.eulerAngles.z;
			if ((length == 0 && zIndex == 180f) || (zIndex == 90f && length == 1) || (length == 2 && zIndex == 0f) || (length == 3 && zIndex == 90f) || (length == 4 && zIndex == 180f))
            {
                LeftDomino.ConnectPoint = CurrentDomino.Up;
            }
            else
            {
                LeftDomino.ConnectPoint = CurrentDomino.Down;
            }

        }
        else if (isRight)
        {
            var length = RightStyle.Count - 1;

            if (temp.Up == temp.Down)
            {
                if (length == 0)
                {
                    RightStyle[length] += 1;
                }
                else
                {
                    RightStyle[length] += 2;
                }
            }
            else
            {
                RightStyle[length] += 2;
            }

            if(length == 0 && RightStyle[length] >= 5)
            {
                RightStyle.Add(0);
            }
            else if (length == 1 && RightStyle[length] >= 6)
            {
                RightStyle.Add(0);
            }
            else if (length == 2 && RightStyle[length] >= 11)
            {
                RightStyle.Add(0);
            }
            else if (length == 3)
            {
                RightStyle.Add(0);
            }

            RightDomino = temp;
            var zIndex = Hint[1].transform.GetChild(0).transform.eulerAngles.z;
			if ((length == 0 && zIndex == 180f) || (zIndex == 90f && length == 1) || (length == 2 && zIndex == 0f) || (length == 3 && zIndex == 90f)|| (length == 4 && zIndex == 180f))
            {
                RightDomino.ConnectPoint = CurrentDomino.Down;
            }
            else
            {
                RightDomino.ConnectPoint = CurrentDomino.Up;
            }
        }
        else if (isUp)
        {
            var length = UpStyle.Count - 1;

            if (temp.Up == temp.Down)
            {
                if (length == 0)
                {
                    UpStyle[length] += 1;
                }
                else
                {
                    UpStyle[length] += 2;
                }
            }
            else
            {
                UpStyle[length] += 2;
            }

            if ((length == 0 && UpStyle[0] >= 4)
                || (length == 1 && UpStyle[1] >= 8)
                || (length == 2 && UpStyle[2] >= 11)
                )
            {
                UpStyle.Add(0);
            }

            UpDomino = temp;
            var zIndex = Hint[2].transform.GetChild(0).transform.eulerAngles.z;
            if ((length == 0 && zIndex == 90f) || (length == 1 && zIndex == 0f) || (length == 2 && zIndex == 270f) || (length == 3 && zIndex == 180f))
            {
                UpDomino.ConnectPoint = CurrentDomino.Up;
            }
            else
            {
                UpDomino.ConnectPoint = CurrentDomino.Down;
            }
        }
        else if (isDown)
        {
            var length = DownStyle.Count - 1;

            if (temp.Up == temp.Down)
            {
                if (length == 0)
                {
                    DownStyle[length] += 1;
                }
                else
                {
                    DownStyle[length] += 2;
                }
            }
            else
            {
                DownStyle[length] += 2;
            }

            if ((length == 0 && DownStyle[0] >= 4)
                || (length == 1 && DownStyle[1] >= 8)
                || (length == 2 && DownStyle[2] >= 11)
                )
            {
                DownStyle.Add(0);
            }

            DownDomino = CurrentDomino;
            var zIndex = Hint[3].transform.GetChild(0).transform.eulerAngles.z;
           	if ((length == 0 && zIndex == 90f) || (length == 1 && zIndex == 0f) || (length == 2 && zIndex == 270f) || (length == 3 && zIndex == 180f))
            {
                DownDomino.ConnectPoint = CurrentDomino.Down;
            }
            else
            {
                DownDomino.ConnectPoint = CurrentDomino.Up;
            }
        }
        else
        {
            LeftStyle[0] += 1;
            RightStyle[0] += 1;
            UpStyle[0] += 1;
            DownStyle[0] += 1;
        }
    }

    private void CheckNextTurn()
    {
        CurrentTurn++;
		if (Type == 0 || Type == 2 )
		{
            //Debug.LogError("Current Tern =>" + (CurrentTurn - 1));
            CurAllFive = CalculatorPoint();
			AllFiveText.text = CurAllFive + " points";	
			if (!CheckNoMove()) {
				AddScoreAllFive(CurAllFive);
			}
		}
        FirstPlayer += CurrentMaxPlayer / 2;
		if (FirstPlayer >= CurrentMaxPlayer) {
			if (CurrentMaxPlayer == 2) {
				FirstPlayer = 0;
			} else {
				FirstPlayer = (FirstPlayer + 0) % 2;
			}
		} else {
			if (CurrentMaxPlayer == 4) {
				if (FirstPlayer == 3) {
					FirstPlayer = 2;
				} else if (FirstPlayer == 2) {
					FirstPlayer = 3;
				}
				//FirstPlayer = (FirstPlayer + 0) % 2;
			}
		} 
        StopCoroutine("ShowPassObject");
		if (FirstPlayer == 0 )
		{
			ismoved = true;
		}	
        RePosTimeBar();
        if (CheckEndGame())
        {
            endGameByPoint = true;
            ShowEnd();
        }
        else
        {
            StartCoroutine("DelayStartGame", 0.5f); ;
        }



        //Debug.LogError("Current Tern =>" + FirstPlayer);
        int tempTurn = (FirstPlayer - 1) < 0 ? (CurrentMaxPlayer - 1): (FirstPlayer - 1);
        //Debug.LogError("Current Tern =>" + tempTurn);
        //var listName = PlayerPrefs.GetString("Name" + TypeGame).Split('-');
        //Name1 = listName[0];
        //Name2 = listName[1];
        //Name3 = listName[2];
        //if (CurAllFive % 5 == 0)
        //{
        //    if (tempTurn == 2)
        //    {
        //        Debug.LogError(playerNameText.text.ToString());
                
        //    }
        //    else if (tempTurn == 0)
        //    {
        //        Debug.LogError(Name3);
        //    }
        //    else if (tempTurn == 1)
        //    {
        //        Debug.LogError(Name1);
        //    }
        //    else if (tempTurn == 3)
        //    {
        //        Debug.LogError(Name2);
        //    }
        //}
    }

    private void AddScoreAllFive(int score)
    {

        Vector3 targetPosition = playerScoreText.transform.position;

        if (score != 0 && score % 5 == 0)
        {
            bool isMe = false;
            if (CurrentMaxPlayer == 2)
            {
                if (FirstPlayer == 0)
                {
                    MePoint += score;
                    isMe = true;

                    totalPlayerScore +=  score;
                    playerScoreText.text = totalPlayerScore + "/" + targetScore;
                    //playerScoreText.text = MePoint + "/" + targetScore;
                }
                else
                {
                    OppoPoint += score;

                    totalBot1Score += score;

                    bot1BigScoreText.text = totalBot1Score + "/" + targetScore;

                    targetPosition = bot1BigScoreText.transform.position;
                }
            }
            else
            {
                if (FirstPlayer < 2)
                {
                    MePoint += score;
                    isMe = true;

                    

                    if (FirstPlayer == 0)
                    {
                        totalPlayerScore += score;
                    }
                    else
                    {
                        totalBot1Score += score;

                        targetPosition = bot1SmallScoreText.transform.position;
                    }
                    playerScoreText.text = totalPlayerScore + "/" + targetScore;
                    bot1SmallScoreText.text = totalBot1Score + "/" + targetScore;


                }
                else
                {
                    OppoPoint += score;

                    

                    if (FirstPlayer == 2)
                    {
                        totalBot2Score += score;
                        targetPosition = bot2ScoreText.transform.position;
                    }
                    else
                    {
                        totalBot3Score += score;
                        targetPosition = bot3ScoreText.transform.position;
                    }
                    bot2ScoreText.text = totalBot2Score + "/" + targetScore;

                    bot3ScoreText.text = totalBot3Score + "/" + targetScore;
                }
            }

            foreach (var item in AnimAllFive)
            {
                item.sprite = null;
            }

            if ((Type == 0 && isMe) || Type == 2)
            {
                var point2 = score.ToString();
                for (int i = 0; i < point2.Length; i++)
                {
                    AnimAllFive[i].sprite = Number[int.Parse(point2[i].ToString())];
                }

                AnimAllFiveObj.transform.position = CurrentDomino.transform.position;
                //var target = isMe ? Me[0].gameObject : Oppo[0].gameObject;
                var target = isMe ? playerTargetForFive.gameObject : enemyTargetForFive.gameObject;
                if (Type == 0)
                    target = Oppo[0].gameObject;
			
				LeanTween.move (AnimAllFiveObj, targetPosition, TimeAnim*3 ).setOnComplete (s => {
					AnimAllFiveObj.transform.position = new Vector3 (10000f, 10000f, 0f);
					SetPoint ();
				});
                //kha//
                int tempTurn = (FirstPlayer - 1) < 0 ? (CurrentMaxPlayer - 1) : (FirstPlayer - 1);
                Debug.LogError(FirstPlayer);
                //int tempTurn = FirstPlayer;
                if (tempTurn == 2)
                {
                    Debug.LogError(tempTurn + "--" + playerNameText.text.ToString() + "--" + score);

                }
                else if (tempTurn == 0)
                {
                    Debug.LogError(tempTurn +"--"+ Name3 +"--"+ score);
                }
                else if (tempTurn == 1)
                {
                    Debug.LogError(tempTurn + "--" + Name1 + "--" + score);
                }
                else if (tempTurn == 3)
                {
                    Debug.LogError(tempTurn + "--" + Name2 + "--" + score);
                }
            }

            
        }
        //Debug.LogError("Current Tern =>" + CurrentTurn);
    }
		
    private void ShowEnd()
    {
        isFirstMatch = false;
        StopCoroutine("DelayStartGame");
        StopCoroutine("OnNextTurnWithDelay");
        bool isWin = false;
        bool forceWin = false;
		bool isWinRate = false;
		bool isTie = false;
        int player = MePoint, oppo = OppoPoint;
        int score = 0;

        //kha edit
        //int player1Score = 0, bot1Score = 0, bot2Score = 0, bot3Score = 0;


        if (Type == 2)
        {
            player = 0;
            oppo = 0;
        }

        if (CurrentMaxPlayer == 4)
        {
            //foreach (var item in PlayerDominoes[0]) {
            //    PlayerScore += (item.Up + item.Down);
            //}
            //if (type == 1)
            //{
            //    if (CurrentMaxPlayer == 4)
            //    {
            //        if (PlayerDominoes[0].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[1])
            //            {
            //                playerScore += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[2])
            //            {
            //                playerScore += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[3])
            //            {
            //                playerScore += (item.Up + item.Down);
            //            }
            //        }
            //        else if (PlayerDominoes[1].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[0])
            //            {
            //                bot1Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[2])
            //            {
            //                bot1Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[3])
            //            {
            //                bot1Score += (item.Up + item.Down);
            //            }
            //        }
            //        else if (PlayerDominoes[2].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[0])
            //            {
            //                bot2Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[1])
            //            {
            //                bot2Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[3])
            //            {
            //                bot2Score += (item.Up + item.Down);
            //            }
            //        }
            //        else if (PlayerDominoes[3].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[0])
            //            {
            //                bot3Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[1])
            //            {
            //                bot3Score += (item.Up + item.Down);
            //            }
            //            foreach (var item in PlayerDominoes[2])
            //            {
            //                bot3Score += (item.Up + item.Down);
            //            }
            //        }

            //        Debug.LogError(playerScore + "-" + bot1Score + "-" + bot2Score + "-" + bot3Score);
            //    }
            //    else
            //    {
            //        if (PlayerDominoes[0].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[1])
            //            {
            //                playerScore += (item.Up + item.Down);
            //            }
                        
            //        }
            //        else if (PlayerDominoes[1].Count <= 0)
            //        {
            //            foreach (var item in PlayerDominoes[0])
            //            {
            //                bot1Score += (item.Up + item.Down);
            //            }
                        
            //        }
            //        Debug.LogError(playerScore + "-" + bot1Score);
            //    }
            //}
            //else { }


            for (int i = 0; i < PlayerDominoes.Count; i++)
            {
                if (PlayerDominoes[i].Count == 0)
                {
                    if (i < 2)
                    {
                        isWin = true;
                        forceWin = true;
                    }
                    else
                    {
                        forceWin = true;
                    }
                }
            }

            for (int i = 0; i < PlayerDominoes.Count; i++)
            {
                foreach (var item in PlayerDominoes[i])
                {
                    score = item.Up + item.Down;
                    if (i < 2)
                    {
                        player += score;
                    }
                    else
                    {
                        oppo += score;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0 && PlayerDominoes[i].Count == 0)
                {
                    isWin = true;
                    forceWin = true;
                }
                else if (i == 1 && PlayerDominoes[i].Count == 0)
                {
                    forceWin = true;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (var item in PlayerDominoes[i])
                {
                    score = item.Up + item.Down;
                    if (i < 1)
                    {
                        player += score;
                    }
                    else
                    {
                        oppo += score;
                    }
                }
            }
        }

		if (!forceWin) {
			if (player < oppo) {
				isWin = true;
			} else if (player == oppo) {
				isTie = true;
			} else {
				isWin = false; 
			}
           
		} 


        int addPoint = 0;
        if (endGameByPoint)
        {
			if (isWin) {
				addPoint = oppo - player;
			} else if (isTie) {
				addPoint = 0;
			} else if (!isWin) {
				addPoint = player - oppo;
			}
	
        }
        else
        {
            if (isWin)
            {
                addPoint = oppo;
            }
			else
            {
                addPoint = player;
            }

        }

        if (Type != 2 && Type != 0)
        {
            MePoint = oppo;
            OppoPoint = player;
        }

        if (endGameByPoint)
        {
            endGameByPoint = false;
            if (isWin)
            {
                FirstPlayer = 0;
			} else if (isWin && CurrentMaxPlayer == 4) {
				int x = UnityEngine.Random.Range (1, 3);
				if (x == 1) {
					FirstPlayer = 0;
				} else if (x == 2) {
					FirstPlayer = 1;
				}
			}
            else if (!isWin && CurrentMaxPlayer == 4)
            {
				int x = UnityEngine.Random.Range (1, 3);
				if (x == 1) {
					FirstPlayer = 2;
				} else if (x == 2) {
					FirstPlayer = 3;
				}
            }
            else
            {
                FirstPlayer = 1;
            }
			if (addPoint == 0) {
				int x = UnityEngine.Random.Range (0, 4);
				FirstPlayer = x; 
			}
        }

        if (Type != 0)
        {
            if (Type != 2)
            {
                if (isWin)
                {
                    TotalMe += addPoint;
                }
                else
                {
                    TotalOppo += addPoint;
                }
            }
            else
            {
                if (isWin)
                {
                    TotalMe = MePoint + addPoint;
                    TotalOppo = OppoPoint;
                }
                else
                {
                    TotalOppo = OppoPoint + addPoint;
                    TotalMe = MePoint;
                }
            }

            MePoint = player;
            OppoPoint = oppo;


            //Kha added
            if (isWin)
            {
                if (CurrentMaxPlayer == 4)
                {
                    if (PlayerDominoes[1].Count <= 0)
                    {
                        if (type == 1)
                        {
                            bot1Score = OppoPoint;
                        }
                        else if (type == 3)
                        {
                            bot1Score = Mathf.Abs(MePoint - OppoPoint);
                        }
                        foreach (var item in PlayerDominoes[0])
                        {
                            if (type == 1)
                            {
                                bot1Score += (item.Up + item.Down);
                            }
                            else if (type == 3)
                            {
                                bot1Score = Mathf.Abs(bot1Score - (item.Up + item.Down)) ;
                            }
                        }
                        if (type == 2)
                        {
                            foreach (List<DominoController> DC in PlayerDominoes)
                            {
                                foreach (var item in DC)
                                {
                                    bot1Score += (item.Up + item.Down);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (type == 1)
                        {
                            player1Score = OppoPoint;
                        }
                        else if (type == 3)
                        {
                            player1Score = Mathf.Abs(MePoint - OppoPoint);
                        }
                        foreach (var item in PlayerDominoes[1])
                        {
                            if (type == 1)
                            {
                                player1Score += (item.Up + item.Down);
                            }
                            else if (type == 3)
                            {
                                player1Score = Mathf.Abs(player1Score - (item.Up + item.Down));
                            }
                        }
                        if (type == 2)
                        {
                            foreach (List<DominoController> DC in PlayerDominoes)
                            {
                                foreach (var item in DC)
                                {
                                    player1Score += (item.Up + item.Down);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        player1Score = OppoPoint;
                    }
                    else if (type == 3)
                    {
                        player1Score = Mathf.Abs(MePoint - OppoPoint);
                    }
                    if (type == 2)
                    {
                        foreach (List<DominoController> DC in PlayerDominoes)
                        {
                            foreach (var item in DC)
                            {
                                player1Score += (item.Up + item.Down);
                            }
                        }
                    }
                }

            }
            else
            {
                if (CurrentMaxPlayer == 4)
                {
                    if (PlayerDominoes[3].Count <= 0)
                    {
                        if (type == 1)
                        {
                            bot3Score = MePoint;
                        }
                        else if (type == 3)
                        {
                            bot3Score = Mathf.Abs(OppoPoint - MePoint) ;
                        }
                        foreach (var item in PlayerDominoes[2])
                        {
                            if (type == 1)
                            {
                                bot3Score += (item.Up + item.Down);
                            }
                            else if (type == 3)
                            {
                                bot3Score = Mathf.Abs(bot3Score - (item.Up + item.Down));
                            }
                        }

                        if (type == 2)
                        {
                            foreach (List<DominoController> DC in PlayerDominoes)
                            {
                                foreach (var item in DC)
                                {
                                    bot3Score += (item.Up + item.Down);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (type == 1)
                        {
                            bot2Score = MePoint;
                        }
                        else if (type == 3)
                        {
                            bot2Score = Mathf.Abs(OppoPoint - MePoint);
                        }
                        foreach (var item in PlayerDominoes[3])
                        {
                            if (type == 1)
                            {
                                bot2Score += (item.Up + item.Down);
                            }
                            else if (type == 3)
                            {
                                bot2Score = Mathf.Abs(bot2Score - (item.Up + item.Down));
                            }
                        }
                        if (type == 2)
                        {
                            foreach (List<DominoController> DC in PlayerDominoes)
                            {
                                foreach (var item in DC)
                                {
                                    bot2Score += (item.Up + item.Down);
                                }
                            }
                        }
                    }
                }
                else
                {
                    bot1Score = Mathf.Abs(OppoPoint - MePoint) ;
                    if (type == 2)
                    {
                        foreach (List<DominoController> DC in PlayerDominoes)
                        {
                            foreach (var item in DC)
                            {
                                bot1Score += (item.Up + item.Down);
                            }
                        }
                    }
                }
            }
            Debug.LogError(string.Format("PlayerScore = {0} \nbot1Score = {1} \nbot2Score = {2} \nbot3Score = {3} \nOppoScore = {4} \nMePoint = {5}\ntype = {6}", 
                player1Score, bot1Score, bot2Score, bot3Score,OppoScore, MePoint,type));


            //Debug.LogError($"{TotalMe} < {targetScore} || {TotalOppo} < {targetScore}");
            //if (Round != 5)
            //if (TotalMe < targetScore || TotalOppo < targetScore)
            //{
            //    EndMatch.InitUI(MePoint, OppoPoint, TotalMe, TotalOppo);
            //    EndMatch.gameObject.SetActive(true);
            //}
            //else
            //{
            //    Result.InitUI((TotalMe > TotalOppo) || (TotalMe == TotalOppo && isWin), TotalMe, TotalOppo);
            //    Result.gameObject.SetActive(true);
            //}
            //if (TotalMe >= targetScore || TotalOppo >= targetScore)
            updateTotal();
            if (totalPlayerScore >= targetScore || totalBot1Score >= targetScore || totalBot2Score >= targetScore || totalBot3Score >= targetScore)
            {
                Result.InitUI((TotalMe > TotalOppo) || (TotalMe == TotalOppo && isWin), TotalMe, TotalOppo);
                Result.gameObject.SetActive(true);
            }
            else
            {
                EndMatch.InitUI(MePoint, OppoPoint, TotalMe, TotalOppo);
                EndMatch.gameObject.SetActive(true);
            }
            MePoint = TotalMe;
            OppoPoint = TotalOppo;
            SetPoint();
        }
        else
        {
            SetPoint();
            //if (PlayerDominoes[0].Count != 0)
            //    isWin = false;
            var star = SaveGame(MePoint, isWin);
            TotalMe = MePoint;
            Puzzle.InitUI(isWin, star);
            Puzzle.gameObject.SetActive(true);
        }
		if (TotalMe >= TotalOppo)
			isWinRate = true; 
        SetBestScore(TotalMe, isWin , isTie);
        //if (Round == 5 || Type == 0) khalil edit
        //if ( (TotalMe >= targetScore || TotalOppo >= targetScore) || Type == 0)
        if ( (totalPlayerScore >= targetScore || totalBot1Score >= targetScore || totalBot2Score >= targetScore || totalBot3Score >= targetScore) || Type == 0)
        {
            ResetSave(TypeGame);
            TotalMe = 0;
            TotalOppo = 0;

            

            // kha editRound = 0;
            Round = 0;
            //RandomName();
            PlayerPrefs.SetInt("Round" + TypeGame, 0);
			PlayerPrefs.SetInt("TotalMe" + TypeGame, TotalMe);
			PlayerPrefs.SetInt("TotalOppo" + TypeGame, TotalOppo);
			int tantest = PlayerPrefs.GetInt (SceneManager.RATE_DATA);
			if (Type == 0 && isWin && PlayerPrefs.GetInt(SceneManager.RATE_DATA) == 0 ) {
				//int currentlevel = SceneManager.instance.CurrentMapData.Level;
				//if ( currentlevel % 3 == 0) {
				//SceneManager.instance.ShowRatePuzzle();
				//}
			} 
			if (isWinRate && PlayerPrefs.GetInt(SceneManager.RATE_DATA) == 0 && Type !=0 )
            {
                //SceneManager.instance.ShowRate();
            }  
        }
        else
        {
            PlayerPrefs.SetInt("TotalMe" + TypeGame, TotalMe);
            PlayerPrefs.SetInt("TotalOppo" + TypeGame, TotalOppo);
			PlayerPrefs.SetInt("PLAYER" + TypeGame, FirstPlayer);
            ResetPart(TypeGame);
        }
		//isClickableLeave = true; 
    }

    private void SetPoint()
    {
        foreach (var item in Oppo)
        {
            item.sprite = null;
        }

        var point2 = Type == 0 ? MePoint.ToString() : OppoPoint.ToString();
        for (int i = 0; i < point2.Length; i++)
        {
            Oppo[i].sprite = Number[int.Parse(point2[i].ToString())];
        }

        if (Type != 0)
        {
            foreach (var item in Me)
            {
                item.sprite = null;
            }

            var point = MePoint.ToString();
            for (int i = 0; i < point.Length; i++)
            {
                Me[i].sprite = Number[int.Parse(point[i].ToString())];
            }
        }
    }

    private int SaveGame(int score, bool isWin)
    {
        //SoundManager.instance.SoundOn(SoundManager.SoundIngame.Win);
        // SceneManager.instance.ShowIntertitialBanner();

        int star = 1;
        if (CurrentMaxPlayer == 2)
        {
            if (score >= 20)
                star = 3;
            else if (score >= 10)
                star = 2;
        }
        else
        {
            if (score >= 20)
                star = 3;
            else if (score >= 10)
                star = 2;
        }
        if (isWin)
        {
            if (star > SceneManager.instance.CurrentMapData.Star)
            {
                SceneManager.instance.CurrentMapData.Score = score;
                SceneManager.instance.CurrentMapData.Star = star;
            }
            SceneManager.instance.SavePoint();
        }
        return star;
    }

    public void OnDominoClick(DominoController temp, bool isOffSound = false)
    {
        if (!isLoading && FirstPlayer == 0
            && !isOffSound)
            SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        if (isBlock || temp.IsChecked)
            return;

        if (isVerticalOnly && temp.Up != temp.Down)
            return;
        ResetHint();
        isLeft = false;
        isRight = false;
        isDown = false;
        isUp = false;

        if (!temp.IsExtra)
        {
            if (CurrentTurn == 0)
            {
                MoveDominoes(temp, center2,
                    temp.Up == temp.Down ? 90f : 0f);

                if (!isLoading)
                {
                    SavePerDominoClick(temp, 0);
                }

                CenterDomino = temp;
                LeftDomino = temp;
                LeftDomino.ConnectPoint = temp.Up;
                RightDomino = temp;
                RightDomino.ConnectPoint = temp.Down;

				if ((Type == 0 || Type == 2) && temp.Up == temp.Down) {
					UpDomino = temp;
					UpDomino.ConnectPoint = temp.Up;
					DownDomino = temp;
					DownDomino.ConnectPoint = temp.Down;
				} else {
					UpDomino = null;
					DownDomino = null;
				}

                LeftSide = CenterDomino;
                RightSide = CenterDomino;
                UpSide = CenterDomino;
                DownSide = CenterDomino;
            }
            else
            {
                CheckDomino(temp);
                if (FirstPlayer == 0)
                {
                    // ClickingObj.transform.SetParent(temp.transform);
                    ClickingObj.transform.position = temp.transform.position;
                    ClickingObj.transform.localScale = PlayerObj[0].transform.localScale;
                };
            }
        }
        else
        {
            ClickingObj.transform.position = new Vector3(10000, 10000, 0);
            temp.IsExtra = false;
            PlayerDominoes[0].Add(temp);
            temp.isMine = true;
            temp.transform.SetParent(DominoesHolder.transform);
            temp.ShowDominos();
            AddDomino(temp);
            isBlock = true;
            LeanTween.move(temp.gameObject, PlayerObj[0].transform.position, TimeAnim).setOnComplete(s =>
            {
                temp.transform.SetParent(PlayerObj[0].transform);
                temp.transform.SetAsFirstSibling();
                temp.transform.localScale = Vector3.one;
                SetPlayerObj();
                isBlock = false;
            });
            RemainDominoes.Remove(temp);
            boneyardText.text = RemainDominoes.Count + "";

            if (!CheckNoMove())
            {
                ActiveExtra(false);
            }
            else if (CheckNoMove() && RemainDominoes.Count == 0)
            {
                ActiveExtra(false);
                CheckNextTurn();
            }
        }
    }

    private void CheckDomino(DominoController temp, bool isReCenter = true)
    {
        ResetHint();
        CurrentDomino = temp;

        if (CheckMatching(temp, LeftDomino))
        {
            

            var rotate = 0f;

            var length = LeftStyle.Count - 1;
            if (length == 0)
            {
                if (LeftDomino.Up == LeftDomino.Down)
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino / 2 * 3, LeftDomino.transform.position.y, 0);
                }
                else
                {
                    if (temp.Up != temp.Down)
                        Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino * 2, LeftDomino.transform.position.y, 0);
                    else
                        Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino / 2 * 3, LeftDomino.transform.position.y, 0);
                }
                if (temp.Up == temp.Down)
                {
                    
                    rotate = 90f;
                }
                else if (temp.Down == LeftDomino.ConnectPoint)
                {
                    rotate = 180f;
                }
            }
            else if (length == 1)
            {
                if (LeftStyle[1] == 0)
                {
                    if (LeftDomino.Up == LeftDomino.Down)
                    {
                        Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x, LeftDomino.transform.position.y + heightPerDomino, 0);
                    }
                    else
                    {
                        Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino / 2, LeftDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                    }
                }
                else
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x, LeftDomino.transform.position.y + heightPerDomino, 0);
                }
                rotate = 90f;
                if (temp.Up == LeftDomino.ConnectPoint)
                    rotate = -90f;
            }
            else if (length == 2)
            {
                

                if (LeftStyle[2] == 0)
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x + widthPerDomino / 2, LeftDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x + widthPerDomino * 2, LeftDomino.transform.position.y, 0);
                }
                
                rotate = 180f;
                if (temp.Down == LeftDomino.ConnectPoint)
                {
                    rotate = 0f;
                }
            }
            else if (length == 3)
            {
                if (LeftStyle[3] == 0)
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x + widthPerDomino / 2, LeftDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x, LeftDomino.transform.position.y + heightPerDomino, 0);
                }
                rotate = -90f;
                if (temp.Down == LeftDomino.ConnectPoint)
                    rotate = 90f;
            }
            else
            {
                if (LeftStyle[4] == 0)
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino / 2, LeftDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[0].transform.position = new Vector3(LeftDomino.transform.position.x - widthPerDomino * 2, LeftDomino.transform.position.y, 0);
                }
                rotate = 180f;
                if (temp.Up == LeftDomino.ConnectPoint)
                {
                    rotate = 0f;
                }
            }

            //if (LeftStyle.Count == 2 && temp.Up == temp.Down)
            //{
            //    Debug.LogError(temp.Down + "- L -" + temp.Up);
            //    if (rotate == -90)
            //    {
            //        rotate = 0;

            //        //Hint[1].GetComponent<RectTransform>().sizeDelta= new Vector3(Hint[1].transform.localScale.x, 80);
            //        Hint[1].transform.position = new Vector3(
            //            Hint[1].transform.position.x,
            //            LeftDomino.transform.position.y - (heightPerDomino/2),
            //            //Hint[1].transform.position.y - (heightPerDomino/2),
            //            //(Hint[1].transform.position.y + (LeftDomino.transform.position.y - Hint[1].transform.position.y) / 2),
            //            0) ;
            //    }
            //}

            Hint[0].transform.GetChild(0).eulerAngles = new Vector3(0, 0, rotate);
            SetImageHint(Hint[0].gameObject, temp);
            ActiveHint[0] = 1;
        }

        if (CheckMatching(temp, RightDomino, true))
        {
            var rotate = 0f;
            var length = RightStyle.Count - 1;

            if (length == 0)
            {
                if (RightDomino.Up == RightDomino.Down)
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino / 2 * 3, RightDomino.transform.position.y, 0);
                }
                else
                {
                    if (temp.Up != temp.Down)
                        Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino * 2, RightDomino.transform.position.y, 0);
                    else
                        Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino / 2 * 3, RightDomino.transform.position.y, 0);
                }
                if (temp.Up == temp.Down)
                    rotate = 90f;
                else if (temp.Up == RightDomino.ConnectPoint || temp.Up == RightDomino.Up)
                    rotate = 180f;
            }
            else if (length == 1)
            {
                if (RightStyle[1] == 0)
                {
                    if (RightDomino.Up == RightDomino.Down)
                    {
                        Hint[1].transform.position = new Vector3(RightDomino.transform.position.x, RightDomino.transform.position.y - heightPerDomino, 0);
                    }
                    else
                    {
                        Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino / 2, RightDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                    }
                }
                else
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x, RightDomino.transform.position.y - heightPerDomino, 0);
                }
                rotate = 90f;
                if (temp.Down == RightDomino.ConnectPoint)
                    rotate = -90f;
            }
            else if (length == 2)
            {
                

                if (RightStyle[2] == 0)
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x - widthPerDomino / 2, RightDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x - widthPerDomino * 2, RightDomino.transform.position.y, 0);
                }
                rotate = 180f;
                if (temp.Up == RightDomino.ConnectPoint)
                {
                    rotate = 0f;
                }
            }
            else if (length == 3)
            {
                if (RightStyle[3] == 0)
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x - widthPerDomino / 2, RightDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x, RightDomino.transform.position.y - heightPerDomino, 0);
                }
                rotate = -90f;
                if (temp.Up == RightDomino.ConnectPoint)
                    rotate = 90f;
            }
            else
            {
                if (RightStyle[4] == 0)
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino / 2, RightDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                }
                else
                {
                    Hint[1].transform.position = new Vector3(RightDomino.transform.position.x + widthPerDomino * 2, RightDomino.transform.position.y, 0);
                }
                rotate = 180f;
                if (temp.Down == RightDomino.ConnectPoint)
                {
                    rotate = 0f;
                }
            }

            //if (RightStyle.Count == 2 && temp.Up == temp.Down)
            //{
            //    Debug.LogError(temp.Down + "- R -" + temp.Up);
            //    if (rotate == -90)
            //    {
            //        rotate = 0;
            //        Hint[1].transform.position = new Vector3(
            //            Hint[1].transform.position.x,
            //            RightDomino.transform.position.y + heightPerDomino/2,
            //            //Hint[1].transform.position.y + (heightPerDomino),
            //            //(Hint[1].transform.position.y - (RightDomino.transform.position.y - Hint[1].transform.position.y) / 2),
            //            0);
            //    }
            //}

            Hint[1].transform.GetChild(0).eulerAngles = new Vector3(0, 0, rotate);
            SetImageHint(Hint[1].gameObject, temp);
            ActiveHint[1] = 1;
        }

        if (Type == 0 || Type == 2)
        {
            if (UpDomino != null && CheckMatching(temp, UpDomino))
            {
                
                var rotate = 0f;
                var length = UpStyle.Count - 1;
                if (length == 0)
                {
                    if (UpDomino.Up == UpDomino.Down)
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x, UpDomino.transform.position.y + heightPerDomino, 0);
                    }
                    else
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x, UpDomino.transform.position.y + heightPerDomino, 0);
                    }

                    rotate = 90f;
                    if (temp.Up == UpDomino.ConnectPoint)
                        rotate = -90f;
                }
                else if (length == 1)
                {
                    if (UpStyle[1] == 0)
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x + widthPerDomino / 2, UpDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                    }
                    else
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x + widthPerDomino * 2, UpDomino.transform.position.y, 0);
                    }
                    rotate = 180f;
                    if (temp.Down == UpDomino.ConnectPoint)
                    {
                        rotate = 0f;
                    }
                }
                else if (length == 2)
                {
                    if (UpStyle[2] == 0)
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x + widthPerDomino / 2, UpDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                    }
                    else
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x, UpDomino.transform.position.y - heightPerDomino, 0);
                    }
                    rotate = 90f;
                    if (temp.Down == UpDomino.ConnectPoint)
                        rotate = -90f;
                }
                else
                {
                    if (UpDomino.Up == UpDomino.Down)
                    {
                        Hint[2].transform.position = new Vector3(UpDomino.transform.position.x - widthPerDomino / 2 * 3, UpDomino.transform.position.y, 0);
                    }
                    else
                    {
                        if (temp.Up != temp.Down)
                            Hint[2].transform.position = new Vector3(UpDomino.transform.position.x - widthPerDomino * 2, UpDomino.transform.position.y, 0);
                        else
                            Hint[2].transform.position = new Vector3(UpDomino.transform.position.x - widthPerDomino / 2 * 3, UpDomino.transform.position.y, 0);
                    }
                    rotate = 0f;
                    if (temp.Down == UpDomino.ConnectPoint)
                        rotate = 180f;
                }

                Hint[2].transform.GetChild(0).eulerAngles = new Vector3(0, 0, rotate);
                SetImageHint(Hint[2].gameObject, temp);
                ActiveHint[2] = 1;
            }

            if (DownDomino != null && CheckMatching(temp, DownDomino))
            {
                var rotate = 0f;
                var length = DownStyle.Count - 1;
                if (length == 0)
                {
                    if (DownStyle[0] == 0)
                    {
                        if (DownDomino.Up == DownDomino.Down)
                        {
                            Hint[3].transform.position = new Vector3(DownDomino.transform.position.x, DownDomino.transform.position.y - heightPerDomino, 0);
                        }
                        else
                        {
                            Hint[3].transform.position = new Vector3(DownDomino.transform.position.x + widthPerDomino / 2, DownDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                        }
                    }
                    else
                    {
                        Hint[3].transform.position = new Vector3(DownDomino.transform.position.x, DownDomino.transform.position.y - heightPerDomino, 0);
                    }
                    rotate = 90f;
                    if (temp.Down == DownDomino.ConnectPoint)
                        rotate = 270f;
                }
                else if (length == 1)
                {
                    if (DownStyle[1] == 0)
                    {
                        Hint[3].transform.position = new Vector3(DownDomino.transform.position.x - widthPerDomino / 2, DownDomino.transform.position.y - heightPerDomino / 4 * 3, 0);
                    }
                    else
                    {
                        Hint[3].transform.position = new Vector3(DownDomino.transform.position.x - widthPerDomino * 2, DownDomino.transform.position.y, 0);
                    }
                    rotate = 180f;
                    if (temp.Up == DownDomino.ConnectPoint)
                    {
                        rotate = 0f;
                    }
                }
                else if (length == 2)
                {
                    if (DownStyle[2] == 0)
                    {
                        Hint[3].transform.position = new Vector3(DownDomino.transform.position.x - widthPerDomino / 2, DownDomino.transform.position.y + heightPerDomino / 4 * 3, 0);
                    }
                    else
                    {
                        Hint[3].transform.position = new Vector3(DownDomino.transform.position.x, DownDomino.transform.position.y + heightPerDomino, 0);
                    }
                    rotate = 90f;
                    if (temp.Up == DownDomino.ConnectPoint)
                        rotate = 270;
                }
                else
                {
                    if (DownDomino.Up == DownDomino.Down)
                    {
						Hint[3].transform.position = new Vector3(DownDomino.transform.position.x + widthPerDomino / 2, DownDomino.transform.position.y + ((heightPerDomino / 4) * 3), 0);
                    }
                    else
                    {
                        if (temp.Up != temp.Down)
							Hint[3].transform.position = new Vector3(DownDomino.transform.position.x + widthPerDomino / 2, DownDomino.transform.position.y + ((heightPerDomino / 4) * 3), 0);
                        else
							Hint[3].transform.position = new Vector3(DownDomino.transform.position.x + widthPerDomino / 2, DownDomino.transform.position.y + ((heightPerDomino / 4) * 3), 0);
                    }
					rotate = 180f;
                    //if (temp.Up == DownDomino.ConnectPoint || temp.Up == DownDomino.Up)
                       // rotate = 180f;
                }
                Hint[3].transform.GetChild(0).eulerAngles = new Vector3(0, 0, rotate);
                SetImageHint(Hint[3].gameObject, temp);
                ActiveHint[3] = 1;
            }
        }

        foreach (var item in Hint)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y, 0);
        }

        if (isReCenter)
            ReCenter();
    }

    private void ResetHint()
    {
        StopCoroutine("Find");
        ActiveHint.Clear();
        for (int i = 0; i < 4; i++)
            ActiveHint.Add(0);
        foreach (var item in Hint)
        {
            item.Hint.sprite = BorderBlack;
            item.transform.localPosition = new Vector3(5000, 5000, 0);
            item.transform.GetChild(0).transform.localScale = Vector3.one;
        }
    }

    private void SetImageHint(GameObject hint, DominoController domino)
    {
        // hint.GetComponentInChildren<Image>().sprite = domino.Chess.sprite;
        hint.transform.GetChild(0).transform.localScale = new Vector3(ScalePerCent[CurrentScaleWidth], ScalePerCent[CurrentScaleWidth], 1f);
    }

    public void OnThisHintClick(GameObject obj)
    {
		
		ismoved = false; 
        if (!isLoading && FirstPlayer == 0)
        {
            SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        }
        ClickingObj.transform.position = new Vector3(10000f, 10000f, 0f);
        if (CurrentDomino != null)
        {
            var zIndex = obj.transform.GetChild(0).transform.eulerAngles.z;
            MoveDominoes(CurrentDomino, obj.transform.position, zIndex);
            var name = obj.name;
            if (!isLoading)
            {
                SavePerDominoClick(CurrentDomino, int.Parse(name));
            }
            if (name == "1")
            {
                isLeft = true;
            }
            else if (name == "2")
            {
                isRight = true;
            }
            else if (name == "3")
            {
                isUp = true;
            }
            else
            {
                isDown = true;
            }
        }
        ResetHint();
    }

    private bool CheckMatching(DominoController temp, DominoController temp2, bool checkRight = false)
    {

        if (temp2.name != CenterDomino.name || !checkRight)
        {
            if (temp2.ConnectPoint == temp.Up || temp2.ConnectPoint == temp.Down)
                return true;
        }
        else if (CenterDomino.name == RightDomino.name && checkRight)
        {
			if (temp2.Up == temp.Up || temp2.Up == temp.Down)
                return true;
        }
        return false;
    }

    private void AiMove()
    {
		ismoved = false; 
        foreach (var item in PlayerDominoes[0])
        {
            item.GetComponent<Button>().interactable = false;
        }

        for (int j = PlayerDominoes[FirstPlayer].Count - 1; j >= 0; j--)
        {
            var item = PlayerDominoes[FirstPlayer][j];
            OnDominoClick(item);
            bool isFound = false;
            var temp = new List<int>();

            for (int i = 0; i < ActiveHint.Count; i++)
            {
                if (ActiveHint[i] == 1)
                {
                    if (i == 0)
                    {
                        temp.Add(LeftStyle.Count);
                    }
                    else if (i == 1)
                    {
                        temp.Add(RightStyle.Count);
                    }
                    else if (i == 2)
                    {
                        temp.Add(UpStyle.Count);
                    }
                    else
                    {
                        temp.Add(DownStyle.Count);
                    }
                    isFound = true;
                }
                else
                {
                    temp.Add(999);
                }
            }

            if (isFound)
            {
                int minima = 999;
                int mindex = 0;

                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i] < minima)
                    { minima = temp[i]; mindex = i; }
                }

                if (ActiveHint[mindex] == 1)
                {
                    if (CurrentTurn == 0)
                    {
                        CenterDomino = item;
                        LeftDomino = item;
                        LeftDomino.ConnectPoint = item.Up;
                        RightDomino = item;
                        RightDomino.ConnectPoint = item.Down;
                        CurrentDomino = item;
                        if ((Type == 0 || Type == 2) && item.Up == item.Down)
                        {
                            UpDomino = item;
                            UpDomino.ConnectPoint = item.Up;
                            DownDomino = item;
                            DownDomino.ConnectPoint = item.Down;
                        }

                        LeftSide = CenterDomino;
                        RightSide = CenterDomino;
                        UpSide = CenterDomino;
                        DownSide = CenterDomino;
                    }

                    OnThisHintClick(Hint[mindex].gameObject);
                    break;
                }
            }

            if (isFound)
                break;
        }
    }

    private void ReCenter()
    {
        var index = 0;
        foreach (var item in ActiveHint)
        {
            index += item;
        }
        if (index == 0)
            return;

        var left = ActiveHint[0] == 1 && Hint[0].transform.position.x < LeftSide.transform.position.x ? Hint[0].transform.position.x : LeftSide.transform.position.x;
        var right = ActiveHint[1] == 1 && Hint[1].transform.position.x > RightSide.transform.position.x ? Hint[1].transform.position.x : RightSide.transform.position.x;
        var move = (left + right) / 2;
        foreach (var item in AllDominos)
        {
            if (item.IsChecked)
            {
                item.transform.position = new Vector2(item.transform.position.x - move, item.transform.position.y);
            }
        }

        List<float> temp = new List<float>();

        temp.Add(UpSide.transform.position.y);
        temp.Add(DownSide.transform.position.y);
        temp.Add(CenterDomino.transform.position.y);

        var top = Mathf.Max(temp.ToArray());
        var bot = Mathf.Min(temp.ToArray());
        var move2 = (top + bot - center.y) / 2;
        foreach (var item in AllDominos)
        {
            if (item.IsChecked)
            {
                item.transform.position = new Vector2(item.transform.position.x, item.transform.position.y - move2);
            }
        }

        CheckDomino(CurrentDomino, false);
    }

    private void RecenterNormal()
    {
        var left = LeftSide.transform.position.x;
        var right = RightSide.transform.position.x;
        var move = (left + right) / 2;
        foreach (var item in AllDominos)
        {
            if (item.IsChecked)
            {
                item.transform.position = new Vector2(item.transform.position.x - move, item.transform.position.y);
            }
        }
    }

    private void ReScaleBoard()
    {
        List<int> temp = new List<int>();
        temp.Add(LeftStyle[0] + RightStyle[0]);
        if (LeftStyle.Count > 1)
        {
            temp.Add(LeftStyle[1] + DownStyle[0]);
        }
        if (RightStyle.Count > 1)
        {
            temp.Add(RightStyle[1] + UpStyle[0]);
        }
        temp.Add(UpStyle[0] + DownStyle[0]);

        foreach (var item in LeftStyle)
        {
            temp.Add(item);
        }

        foreach (var item in RightStyle)
        {
            temp.Add(item);
        }

        foreach (var item in UpStyle)
        {
            temp.Add(item);
        }

        foreach (var item in DownStyle)
        {
            temp.Add(item);
        }


        //Kha code
        //DominoController[] boardChildes = Board.gameObject.GetComponentsInChildren<DominoController>();
        //if (boardChildes.Length > 0)
        //{
        //    RectTransform[] dominoesRect = new RectTransform[boardChildes.Length];
        //    for (int i = 0; i < boardChildes.Length; i++)
        //    {
        //        dominoesRect[i] = boardChildes[i].gameObject.GetComponent<RectTransform>();
        //    }
        //    //float minX = 0;
        //    //float maxX = 0;
        //    //float minY = 0;
        //    //float maxY = 0;
        //    //float minX = dominoesRect[0].anchoredPosition.x;
        //    //float maxX = dominoesRect[0].anchoredPosition.x;
        //    //float minY = dominoesRect[0].anchoredPosition.y;
        //    //float maxY = dominoesRect[0].anchoredPosition.y;
        //    ////Debug.LogError(minX + " -- " + minY);
        //    //foreach (RectTransform dcTemp in dominoesRect)
        //    //{
        //    //    if (minX > dcTemp.anchoredPosition.x)
        //    //    {
        //    //        minX = dcTemp.anchoredPosition.x;
        //    //    }
        //    //    if (maxX < dcTemp.anchoredPosition.x)
        //    //    {
        //    //        maxX = dcTemp.anchoredPosition.x;
        //    //    }
        //    //    if (minY > dcTemp.anchoredPosition.y)
        //    //    {
        //    //        minY = dcTemp.anchoredPosition.y;
        //    //    }
        //    //    if (maxY < dcTemp.anchoredPosition.y)
        //    //    {
        //    //        maxY = dcTemp.anchoredPosition.y;
        //    //    }
        //    //}

        //    //int numberOfHorizontalTiles =  (int)((Mathf.Abs(minX) + Mathf.Abs(maxX)) / 160);
        //    //int numberOfVerticalTiles = (int)((Mathf.Abs(minY) + Mathf.Abs(maxY)) / 160);
        //    //Debug.LogError("H Tiles -> " + numberOfHorizontalTiles);
        //    //Debug.LogError("V Tiles -> " + numberOfVerticalTiles);
        //    //Debug.LogError("H Tiles -> " + (from x in dominoesRect select x.anchoredPosition.y).Distinct().Count());
        //    //var q = from x in dominoesRect
        //    //        group x.anchoredPosition.y by x.anchoredPosition.y into g
        //    //        let count = g.Count()
        //    //        orderby count descending
        //    //        select new { Value = g.Key, Count = count };
        //    var q = from x in dominoesRect
        //            group x by x.anchoredPosition.y into g
        //            let count = g.Count()
        //            orderby count descending
        //            select new { Value = g.Key, Count = count };

        //    List<int> numAr = new List<int>();
        //    foreach (var distinctList in q)
        //    {
        //        numAr.Add(distinctList.Count);
        //        //Debug.LogError($"Count: {distinctList.Count}; List: ({string.Join(", ", distinctList.Value)})");
        //    }

        //    //Debug.LogError("H Tiles -> " + Mathf.Max( numAr.ToArray()));
        //    int numberOfHorizontalTiles = Mathf.Max(numAr.ToArray());
        //    //Debug.LogError("V Tiles -> " + numberOfVerticalTiles);
        //    var v = from x in dominoesRect
        //            group x by x.anchoredPosition.x into g
        //            let count = g.Count()
        //            orderby count descending
        //            select new { Value = g.Key, Count = count };

        //    List<int> numArV = new List<int>();
        //    foreach (var distinctList in v)
        //    {
        //        numArV.Add(distinctList.Count);
        //        //Debug.LogError($"Count: {distinctList.Count}; List: ({string.Join(", ", distinctList.Value)})");
        //    }

        //    //Debug.LogError("Y Tiles -> " + numArV.ToArray()[0]);
        //    int numberOfVerticalTiles = Mathf.Max(numArV.ToArray());
        //    Debug.LogError("Y Tiles -> " + numberOfVerticalTiles + "\n" + "H Tiles -> " + numberOfHorizontalTiles);
        //}

        // kha code end

        //foreach (int v in temp)
        //{
        //    Debug.LogError(v);
        //}
        //Debug.LogError("max is =" + Mathf.Max(temp.ToArray()));
        // i have added 100 condition
        //if (ScaleWidth[CurrentScaleWidth] < Mathf.Max(temp.ToArray())  && CurrentScaleWidth < 3)  orignal
        if (ScaleWidth[CurrentScaleWidth] < Mathf.Max(temp.ToArray())  && CurrentScaleWidth < ScaleWidth.Count)
        {
            CurrentScaleWidth++;
            //Board.transform.localScale = new Vector3(ScalePerCent[CurrentScaleWidth], ScalePerCent[CurrentScaleWidth], 1f);
            //widthPerDomino = widthDefault * ScalePerCent[CurrentScaleWidth];
            //heightPerDomino = heightDefault * ScalePerCent[CurrentScaleWidth];

            
            newTargetScale = new Vector3(ScalePerCent[CurrentScaleWidth], ScalePerCent[CurrentScaleWidth], 1f);
            newDominoWidth = widthDefault * ScalePerCent[CurrentScaleWidth];
            newDominoHight = heightDefault * ScalePerCent[CurrentScaleWidth];
            boardAnimating = true;
            //handleBoardAnimation()
        }
    }

   

    private bool CheckEndGame()
    {
        if (CurrentTurn > 0 && ((RemainDominoes.Count == 0 && Type != 3) || Type == 3))
        {
            foreach (var item in PlayerDominoes)
            {
                foreach (var domino in item)
                {
                    if (CheckMatching(domino, LeftDomino) || CheckMatching(domino, RightDomino, true))
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool CheckNoMove()
    {
        if (CurrentTurn > 0)
        {
            foreach (var domino in PlayerDominoes[FirstPlayer])
            {
                if (CheckMatching(domino, LeftDomino) || CheckMatching(domino, RightDomino, true))
                {
                    return false;
                }

                if (Type == 0 || Type == 2)
                {
                    if ((UpDomino != null && CheckMatching(domino, UpDomino))
                        || (DownDomino != null && CheckMatching(domino, DownDomino)))
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    public void OnSoundClick()
    {
        IsSoundOn = !IsSoundOn;
        Sound.sprite = IsSoundOn ? SoundOn : SoundOff;
        PlayerPrefs.SetInt("SOUND", IsSoundOn ? 1 : 0);
        SoundManager.instance.SetSound(IsSoundOn ? 100f : 0f);
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void ChangeBg()
    {
        Bg.sprite = BgSprite[PlayerPrefs.GetInt("BG")];
    }

    public void ChangeSpeed()
    {
        var temp = PlayerPrefs.HasKey("ANIM") ? PlayerPrefs.GetInt("ANIM") : 1;
        if (temp == 0)
        {
            //TimeAnim = 1f;
            TimeAnim = 0.2f;
        }
        else if (temp == 1)
        {
            //TimeAnim = 0.5f;
            TimeAnim = 0.25f;
        }
        else
        {
            TimeAnim = 0.25f;
        }
    }

    public void ChangeTiles()
    {
        if (PlayerDominoes.Count == 0)
            return;

        SceneManager.instance.ModeController.GetMode();
        DominosSprites.Clear();
        foreach (var item in SceneManager.instance.ModeController.MainDominoes)
            DominosSprites.Add(item);

        for (int i = DominosSprites.Count - 1; i >= 0; i--)
        {
            AllDominos[i].ChangeBg();
            DominosSprites.RemoveAt(AllDominos[i].Index);
        }
        foreach (var item in PlayerDominoes[0])
        {
            item.ShowDominos();
        }

        foreach (var item in AllDominos)
        {
            if (item.IsChecked)
            {
                item.ChangeUI();
            }
        }
    }

    private void RePosTimeBar()
    {
        turnCircleHandler();
        TimeBar.transform.position = PosTimeBar[FirstPlayer].transform.position;
        if (FirstPlayer > 1)
        {
            TimeBar.transform.localEulerAngles = new Vector3(0, 0, 90f);
        }
        else
        {
            TimeBar.transform.localEulerAngles = Vector3.zero;
        }
    }
    private void turnCircleHandler()
    {
        //Debug.LogError(FirstPlayer);
        bot1BigTurnCircle.gameObject.SetActive(false);
        bot1SmallTurnCircle.gameObject.SetActive(false);
        bot2TurnCircle.gameObject.SetActive(false);
        bot3TurnCircle.gameObject.SetActive(false);
        playerTurnCircle.gameObject.SetActive(false);

        if (CurrentMaxPlayer == 2)
        {
            if (FirstPlayer == 0)
            {
                playerTurnCircle.gameObject.SetActive(true);
            }
            else if (FirstPlayer == 1)
            {
                bot1BigTurnCircle.gameObject.SetActive(true);
            }
        }
        else
        {
            if (FirstPlayer == 0)
            {
                playerTurnCircle.gameObject.SetActive(true);
            }
            else if (FirstPlayer == 1)
            {
                bot2TurnCircle.gameObject.SetActive(true);
            }
            else if (FirstPlayer == 2)
            {
                bot1SmallTurnCircle.gameObject.SetActive(true);
            }
            else if (FirstPlayer == 3)
            {
                bot3TurnCircle.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        //Khalil Animation
        if (boardAnimating)
        {
            Board.transform.localScale = Vector3.MoveTowards(Board.transform.localScale, newTargetScale, 0.1f);
            widthPerDomino = Mathf.MoveTowards(widthPerDomino, newDominoWidth, 0.1f);
            heightPerDomino = Mathf.MoveTowards(heightPerDomino, newDominoHight, 0.1f);
            //widthPerDomino = widthDefault * ScalePerCent[CurrentScaleWidth];
            //heightPerDomino = heightDefault * ScalePerCent[CurrentScaleWidth];

            if (Board.transform.localScale.Equals(newTargetScale))
            {
                widthPerDomino = newDominoWidth;
                heightPerDomino = newDominoHight;

                boardAnimating = false;
            }
        }

		//if (FirstPlayer == 0 && ismoved) {
		//	Setting2.GetComponent<Button> ().interactable = true;
		//} else if (FirstPlayer != 0 && isClickableLeave) {
			//Setting2.GetComponent<Button> ().interactable = true;
		//} else {
			//Setting2.GetComponent<Button> ().interactable = false;
		//}
        if (IsShowScreen && !EndMatch.gameObject.activeSelf && !Result.gameObject.activeSelf 
            && !Puzzle.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                if (!QuitGame.gameObject.activeSelf && !Setting.gameObject.activeSelf)
                {
                    //ShowQuitGame();
					OnSettingClick();
                } 
                else if (QuitGame.gameObject.activeSelf)
                {
                    OnCancelQuitGame();
                } 
                else if (Setting.gameObject.activeSelf)
                {
                    Setting.OnCloseSetting();
                }
            }
        }
    }

    public void OnSettingClick()
    {
        Time.timeScale = 0;
        Setting.gameObject.SetActive(true);
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    public void LoadGame()
    {
        loadUserData();

        if (PlayerPrefs.GetInt("SAVE_GAME" + TypeGame) == 1)
        {
            ResetHint();
            isFirstMatch = false;
            PlayerObj[0].GetComponent<RectTransform>().sizeDelta = new Vector2(720f, 242f);
            PlayerObj[0].GetComponent<RectTransform>().localScale = Vector3.one;
            CurrentMaxPlayer = PlayerPrefs.GetInt("PLAYERNUM" + TypeGame);
            SceneManager.instance.MaxPlayer = CurrentMaxPlayer;
            if (CurrentMaxPlayer == 2)
            {
                You.sprite = YouHim[0];
                Him.sprite = YouHim[1];
                You.SetNativeSize();
                Him.SetNativeSize();

                bot1Small.gameObject.SetActive(false);
                bot2.gameObject.SetActive(false);
                bot3.gameObject.SetActive(false);
                bot1Big.gameObject.SetActive(true);
            }
            else
            {

                bot1Small.gameObject.SetActive(true);
                bot2.gameObject.SetActive(true);
                bot3.gameObject.SetActive(true);
                bot1Big.gameObject.SetActive(false);

                You.sprite = YouHim[2];
                Him.sprite = YouHim[3];
                You.SetNativeSize();
                Him.SetNativeSize();
            }
			MePoint = 0;
			OppoPoint = 0;
            Type = PlayerPrefs.GetInt("TYPE" + TypeGame);
			FirstPlayer = PlayerPrefs.GetInt("PLAYER" + TypeGame);
            SceneManager.instance.TypePlay = Type;
            SetPoint();
            if (Type == 0)
            {
                SceneManager.instance.GetCurrentMapData(PlayerPrefs.GetInt("LEVEL"));
                SceneManager.instance.LastLevel = SceneManager.instance.CurrentMapData.Level;
                You.sprite = YouHim[4];
                Him.sprite = YouHim[5];
                You.SetNativeSize();
                Him.SetNativeSize();

                foreach (var item in Me)
                {
                    item.sprite = null;
                }

                var point = SceneManager.instance.LastLevel.ToString();
                for (int i = 0; i < point.Length; i++)
                {
                    Me[i].sprite = Number[int.Parse(point[i].ToString())];
                }
            }
            else
            {
				
                var listName = PlayerPrefs.GetString("Name" + TypeGame).Split('-');
                Name1 = listName[0];
                Name2 = listName[1];
                Name3 = listName[2];
            }

            if (Type == 1 || Type == 3)
            {
                MePoint = TotalMe;
                OppoPoint = TotalOppo;
                SetPoint();
                MePoint = 0;
                OppoPoint = 0;
            }

            if (Type == 0 || Type == 2)
            {

                AllFive.SetActive(true);
                AllFiveText.text = CurAllFive + " points";
            }
            else
            {
                AllFive.SetActive(false);
            }

            var all = PlayerPrefs.GetString("AllDomino" + TypeGame);
            var part2 = all.Split(';');

            if (part2.Length > 1)
            {
                for (int i = AllDominos.Count - 1; i >= 0; i--)
                {
                    var domino = part2[i].Split('-');
                    AllDominos[i].Index = int.Parse(domino[0]);
                    AllDominos[i].Up = int.Parse(domino[1]);
                    AllDominos[i].Down = int.Parse(domino[2]);
                    AllDominos[i].InitDomino(AllDominos[i].Index, AllDominos[i].Up, AllDominos[i].Down);
                    DominosSprites.RemoveAt(AllDominos[i].Index);
                }

                LeftDomino = null;
                RightDomino = null;
                UpDomino = null;
                DownDomino = null;
                RemainDominoes.Clear();

                PlayerDominoes.Clear();
                for (int i = 0; i < CurrentMaxPlayer; i++)
                {
                    PlayerDominoes.Add(new List<DominoController>());
                }

                for (int i = 0; i < CurrentMaxPlayer; i++)
                {
                    var temp = PlayerPrefs.GetString("Domino" + TypeGame + i);
			

                    var part = temp.Split(';');
                    foreach (var item in part)
                    {
                        var domino = item.Split('-');
                        if (domino[0].Length > 0)
                        {
                            var up = int.Parse(domino[0]);
                            var down = int.Parse(domino[1]);
                            foreach (var dominos in AllDominos)
                            {
                                if (dominos.Up == up && dominos.Down == down)
                                {
                                    PlayerDominoes[i].Add(dominos);
                                    break;
                                }
                            }
                        }
                    }
                }

                foreach (var item in PlayerDominoes[0])
                {
                    item.transform.SetParent(PlayerObj[0].transform);
                    item.ShowDominos();
                    item.isMine = true;
                    item.IsRemain = false;
                }
                SetPlayerObj();

                foreach (var item in PlayerDominoes[1])
                {
                    item.transform.SetParent(PlayerObj[1].transform);
                    item.rectTransform.anchoredPosition = Vector3.zero;
                    Debug.LogError("1--");
                    item.IsRemain = false;
                }

                if (CurrentMaxPlayer == 4)
                {
                    foreach (var item in PlayerDominoes[2])
                    {
                        item.Rotate(90);
                        item.transform.SetParent(PlayerObj[2].transform);
                        item.rectTransform.anchoredPosition = Vector3.zero;
                        Debug.LogError("2--");
                        item.IsRemain = false;
                    }

                    foreach (var item in PlayerDominoes[3])
                    {
                        item.Rotate(90);
                        item.transform.SetParent(PlayerObj[3].transform);
                        item.rectTransform.anchoredPosition = Vector3.zero;
                        Debug.LogError("3--");
                        item.IsRemain = false;
                    }
                }

                foreach (var item in AllDominos)
                {
                    if (item.IsRemain)
                    {
                        RemainDominoes.Add(item);
                        item.transform.SetParent(ExtraBox.transform);
                        item.IsExtra = true;
                        item.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        item.GetComponent<Button>().interactable = false;
                    }
                }
                StartCoroutine(DelayStep());
            }
            else
            {
                isLoading = false;
                ResetSave(TypeGame);
                NewGame();
            }
        }

        boneyardGo.gameObject.SetActive(true);
        boneyardText.text = RemainDominoes.Count + "";

        if (type == 3)
        {
            boneyardGo.gameObject.SetActive(false);
        }
    }

    IEnumerator DelayStep()
    {
        yield return new WaitForEndOfFrame();
        var all = PlayerPrefs.GetString("CUR_DOMINO" + TypeGame);
        var part = all.Split(';');
        foreach (var item in part)
        {
            var domino = item.Split('-');
            if (domino[0].Length > 0)
            {
                var up = int.Parse(domino[0]);
                var down = int.Parse(domino[1]);
                var target = int.Parse(domino[2]);
                foreach (var dominos in AllDominos)
                {
                    if (dominos.Up == up && dominos.Down == down)
                    {
                        CurrentDomino = dominos;
                        OnDominoClick(dominos);
                        yield return new WaitForEndOfFrame();
                        if (target != 0 && (ActiveHint[target - 1] == 1))
                        {
                            OnThisHintClick(Hint[target - 1].gameObject);
                        }
                        RePosTimeBar();
                        break;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
        isLoading = false;
        if (PlayerDominoes[FirstPlayer].Count == 0)
        {
            ShowEnd();
        }
        else
        {
			StartCoroutine("DelayStartGame", 0.1f);
        }
    }

    public void SaveGame()
    {
		PlayerPrefs.SetInt("SAVE_GAME" + TypeGame, 1);
        PlayerPrefs.SetInt("TURN" + TypeGame, CurrentTurn);
		PlayerPrefs.SetInt("PLAYER" + TypeGame, FirstPlayer);
        PlayerPrefs.SetInt("PLAYERNUM" + TypeGame, CurrentMaxPlayer);
        PlayerPrefs.SetInt("TYPE" + TypeGame, Type);
        if (Type == 0)
        {
            PlayerPrefs.SetInt("LEVEL", SceneManager.instance.CurrentMapData.Level);
        }

        for (int i = 0; i < CurrentMaxPlayer; i++)
        {
            var temp = "";
            foreach (var item in PlayerDominoes[i])
            {
                temp += item.Up + "-" + item.Down + ";";
            }
            PlayerPrefs.SetString("Domino" + TypeGame + i, temp);
        }
        var temp2 = "";
        foreach (var item in AllDominos)
        {
            temp2 += item.Index + "-" + item.Up + "-" + item.Down + ";";
        }
        PlayerPrefs.SetString("AllDomino" + TypeGame, temp2);

    }

    public void AddDomino(DominoController cur)
    {
        var temp = PlayerPrefs.GetString("Domino" + TypeGame + FirstPlayer);
        temp += cur.Up + "-" + cur.Down + ";";
        PlayerPrefs.SetString("Domino" + TypeGame + FirstPlayer, temp);
    }

    public void SavePerDominoClick(DominoController cur, int target)
    {
        var temp = PlayerPrefs.GetString("CUR_DOMINO" + TypeGame);
        temp += cur.Up + "-" + cur.Down + "-" + target + ";";
        PlayerPrefs.SetString("CUR_DOMINO" + TypeGame, temp);
        PlayerPrefs.SetInt("TURN" + TypeGame, CurrentTurn);
    }

    public void ResetSave(int preType)
    {
        PlayerPrefs.SetInt("SAVE_GAME" + preType, 0);
        PlayerPrefs.SetString("CUR_DOMINO" + preType, "");
		PlayerPrefs.SetString("Domino" + preType + "0", "");
		PlayerPrefs.SetString("Domino" + preType + "1", "");
		PlayerPrefs.SetString("Domino" + preType + "2", "");
		PlayerPrefs.SetString("Domino" + preType + "3", "");

		
    }

    public void ResetPart(int preType)
    {
        PlayerPrefs.SetString("CUR_DOMINO" + preType, "");
        PlayerPrefs.SetString("Domino" + preType + "0", "");
        PlayerPrefs.SetString("Domino" + preType + "1", "");
        PlayerPrefs.SetString("Domino" + preType + "2", "");
        PlayerPrefs.SetString("Domino" + preType + "3", "");
        PlayerPrefs.SetString("AllDomino" + preType, "");
    }

    private int CalculatorPoint()
    {
        int result = 0;
        List<DominoController> temp = new List<DominoController>();
        temp.Add(LeftDomino);
        if (LeftDomino.name != RightDomino.name)
        {
            temp.Add(RightDomino);
        }

        if (UpDomino != null && UpDomino.name != LeftDomino.name && UpDomino.name != RightDomino.name)
        {
            temp.Add(UpDomino);
        }

        if (DownDomino != null)
        {
            if (DownDomino.name != LeftDomino.name && DownDomino.name != RightDomino.name && DownDomino.name != UpDomino.name)
            {
                temp.Add(DownDomino);
            }
        }

        if (temp.Count > 2)
        {
            for (int i = temp.Count - 1; i >= 0; i--)
            {
                if (temp[i].name == CenterDomino.name)
                {
                    temp.RemoveAt(i);
                    break;
                }
            }
        }

        foreach (var item in temp)
        {
            if (CurrentTurn == 1 && item.Up != item.Down)
            {
                return item.Up + item.Down;
            }
            else
            {
                if (CenterDomino.Up != CenterDomino.Down && item.name == RightDomino.name && RightDomino.name == CenterDomino.name)
                {
                    result += item.ConnectPoint == item.Up ? item.Down : item.Up;
                }
                else
                {
                    result += item.Up == item.Down ? item.Up * 2 : item.ConnectPoint;
                }
            }
        }

        return result;
    }

    void SetBestScore(int score, bool isWin,bool isTie)
    {
        var temp = "";
        var mode = "";
        if (Type == 1)
        {
            mode = "DRAW_SCORE";
        }
        else if (Type == 2)
        {
            mode = "ALLFIVE_SCORE";
        }
        else if (Type == 3)
        {
            mode = "BLOCK_SCORE";
        }
        else
        {
            mode = "PUZZLE_SCORE";
        }

        temp = PlayerPrefs.GetString(mode);
        var best = 0;
        var total = 0;
        var win = 0;
        var lose = 0;
		var tie = 0;
        if (temp != "")
        {
            var parts = temp.Split('-');
            best = int.Parse(parts[0]);
            total = int.Parse(parts[1]);
            win = int.Parse(parts[2]);
            lose = int.Parse(parts[3]);
			tie = int.Parse (parts [4]);
        }
        if (score > best)
            best = score;
        total++;
		if (isTie) {
			tie++;
		} else if (isWin) {
			win++; 
		} else if (!isWin) {
			lose++;
		}
			

		PlayerPrefs.SetString(mode, best + "-" + total + "-" + win + "-" + lose + "-" + tie);

    }

    void SetPlayerObj()
    {

        var maxSize = PlayerDominoes[0].Count - 8;
        if (maxSize < 0)
            maxSize = 0;

        if (maxSize >= ScalePlayerObj.Count)
            maxSize = ScalePlayerObj.Count - 1;

        var scale = ScalePlayerObj[maxSize];
        PlayerObj[0].GetComponent<RectTransform>().sizeDelta = new Vector2(720f / scale, 242f);
        PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        //if ()
        //{ }
        //Debug.LogError(PlayerObj[0].GetComponentsInChildren<DominoController>().Length);
        //PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(scale - 0.1f , scale - 0.1f , scale - 0.1f);
        if (PlayerObj[0].GetComponentsInChildren<DominoController>().Length >= 8)
        {
            PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1f);
        }
        if (PlayerObj[0].GetComponentsInChildren<DominoController>().Length >= 9)
        {
            PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1f);
        }
        if (PlayerObj[0].GetComponentsInChildren<DominoController>().Length >= 11)
        {
            PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        if (PlayerObj[0].GetComponentsInChildren<DominoController>().Length >= 13)
        {
            PlayerObj[0].GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }

    public void RandomName()
    {
        int length = NameBot.Count;
        List<int> temp = new List<int>();
        for(int i = 0; i < length; i++)
        {
            temp.Add(i);
        }
        var ran = Random.Range(0, temp.Count);
        Name1 = NameBot[temp[ran]];
        temp.RemoveAt(ran);

        ran = Random.Range(0, temp.Count);
        Name2 = NameBot[temp[ran]];
        temp.RemoveAt(ran);

        ran = Random.Range(0, temp.Count);
        Name3 = NameBot[temp[ran]];
        temp.RemoveAt(ran);

        //Rank logic
        //playerRankImage.fillAmount = Random.Range(0.1f, 1.0f);
        //playerRankText.text = "" + Random.Range(1, 9);

        {
            //In case maxPPllayer 2 has late Response
            bot1Sprite = allBotImages[Random.Range(0, allBotImages.Length)];
            bot1BImage.sprite = bot1Sprite;
            bot1BNameText.text = Name1.ToString();

            bot1RankImagFill = Random.Range(0.1f, 1.0f);
            bot1BRankImage.fillAmount = bot1RankImagFill;
            bot1Rank = Random.Range(1, 9);
            bot1BRankText.text = "" + bot1Rank;
        }


        //    if (CurrentMaxPlayer == 2)
        //{
            
            //bot1BNameText.text = Name1.ToString();

            //bot1RankImagFill = Random.Range(0.1f, 1.0f);
            //bot1BRankImage.fillAmount = bot1RankImagFill;
            //bot1Rank = Random.Range(1, 9);
            //bot1BRankText.text = "" + bot1Rank;


            //bot1Sprite = allBotImages[Random.Range(0,allBotImages.Length)];
            //bot1BImage.sprite = bot1Sprite;
            //Debug.LogError("Test bot image");
        //}
        //else
        //{
            bot1SNameText.text = Name1.ToString();
            bot2NameText.text = Name2.ToString();
            bot3NameText.text = Name3.ToString();

            //bot1RankImagFill = Random.Range(0.1f, 1.0f);
            //bot1Rank = Random.Range(1, 9);
            bot2RankImagFill = Random.Range(0.1f, 1.0f);
            bot2Rank = Random.Range(1, 9);
            bot3RankImagFill = Random.Range(0.1f, 1.0f);
            bot3Rank = Random.Range(1, 9);

            //bot1Sprite = allBotImages[Random.Range(0, allBotImages.Length)];
            bot1SImage.sprite = bot1Sprite;
            bot2Sprite = allBotImages[Random.Range(0, allBotImages.Length)];
            Bot2Image.sprite = bot2Sprite;
            bot3Sprite = allBotImages[Random.Range(0, allBotImages.Length)];
            Bot3Image.sprite = bot3Sprite;
        //}

    }

    public int GetHintStatus()
    {
        int temp = 0;
        int res = 0;
        var minValue = float.MaxValue;
        var gameObj = DragAndDropItem.icon;
        HintController pre = null;
        if (gameObj != null && gameObj.activeSelf)
        {
            for (int i = 0; i < ActiveHint.Count; i++)
            {
                if (ActiveHint[i] == 1)
                {
                    Hint[i].Hint.sprite = BorderBlack;
                    temp++;
                    var min = Mathf.Sqrt(Mathf.Pow((Hint[i].gameObject.transform.position.x - gameObj.gameObject.transform.position.x), 2)
                            + Mathf.Pow((Hint[i].gameObject.transform.position.y - gameObj.gameObject.transform.position.y), 2));

                    if (min < minValue)
                    {
                        if (pre != null)
                        {
                            pre.Hint.sprite = BorderBlack;
                        }
                        minValue = min;
                        res = i;
                        Hint[i].Hint.sprite = BorderYellow;
                        pre = Hint[i];
                    }
                }
            }
        }
        return temp > 0 ? res : -1;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            if (CurrentDomino != null)
            {
                CurrentDomino.OnDropAfterPause();
            }
        }
    }

    public void ActiveExtra(bool isActive)
    {
        DialogExtra.alpha = isActive ? 1 : 0;
        DialogExtra.blocksRaycasts = isActive;
        DialogExtra.interactable = isActive;
        ClickingObj.transform.position = new Vector3(10000f, 10000f, 0f);
    }

    public void ShowQuitGame()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        QuitGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnQuitGameClick()
    {
        Setting.QuitGame();
    }

    public void OnCancelQuitGame()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        QuitGame.SetActive(false);
        Time.timeScale = 1;
    }

    public void FindAllHint()
    {
        StopCoroutine("Find");
        StartCoroutine("Find");
    }

    IEnumerator Find()
    {
        while(StartChecking)
        {
            yield return null;
            GetHintStatus();
        }
    }
}
