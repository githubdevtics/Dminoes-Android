using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultController : MonoBehaviour {

    public Text WinLose;
    public string Win, Lose , Tie;
    public List<Sprite> Number;
    public List<Image> Player, Oppo;
    public Text playerText, oppoText;
    public GameObject Name;
    public Text text1, text2;

    //kha coded for user show
    public Text playerName, playerScoreText;
    public Text bot1Name, bot1Score;
    public Text bot2Name, bot2Score;
    public Text bot3Name, bot3Score;

    public Image[] allImages;
    //for two player
    //public Image playerImage, bigBotImage;

    public Text rankText;
    public Image rankFillImage;

    public Text[] rankTextAll;
    public Image[] rankFillImageAll;

    public levelCalculator levelcalculator;

    int positionOfWining;


    public RectTransform rewardPanel;
    public Text rewardText;
    //public Image bot1BImage, bot1SImage, bot2Image, bot3Image;

    //public Text bot1RankText, bot2RankText, bot3RankText;
    //public Image bot1RankImage, bot2RankImage, bot3RankImage;

    public LeaderBoardManager leaderBoardManager;
    public AdsManager adsManager;

    public void Start()
    {
        
    }

    public void InitUI(bool isWin, int playerScore, int oppoScore)
    {
        WinLose.text = isWin ? Win : Lose;
		if (playerScore == oppoScore) {
			WinLose.text = Tie;
		}

        

        if (SceneManager.instance.PlayGameController.CurrentMaxPlayer == 4)
        {

            //load player image Google
            //allImages[0].sprite = leaderBoardManager.getUserImageLeaderBoard();
            //StartCoroutine(leaderBoardManager.getUserImageLeaderRoutine(allImages[0]));
            //allImages[0].sprite = leaderBoardManager.loadedSprite != null ? leaderBoardManager.loadedSprite : leaderBoardManager.sprite;


            bot2Name.transform.parent.gameObject.SetActive(true);
            bot3Name.transform.parent.gameObject.SetActive(true);

            //playerName.text = PlayerPrefs.GetString("nick-name", "Nick Name");
            //playerScoreText.text = SceneManager.instance.PlayGameController.totalPlayerScore.ToString();
            //bot1Name.text = SceneManager.instance.PlayGameController.Name1;
            //bot1Score.text = SceneManager.instance.PlayGameController.totalBot1Score.ToString();
            //bot2Name.text = SceneManager.instance.PlayGameController.Name2;
            //bot2Score.text = SceneManager.instance.PlayGameController.totalBot2Score.ToString();
            //bot3Name.text = SceneManager.instance.PlayGameController.Name3;
            //bot3Score.text = SceneManager.instance.PlayGameController.totalBot3Score.ToString();
            //text1.text = "You & " + SceneManager.instance.PlayGameController.Name1;
            //text2.text = SceneManager.instance.PlayGameController.Name2 + " & " + SceneManager.instance.PlayGameController.Name3;

            int[] scores = { SceneManager.instance.PlayGameController.totalPlayerScore,
            SceneManager.instance.PlayGameController.totalBot1Score,
            SceneManager.instance.PlayGameController.totalBot2Score,
            SceneManager.instance.PlayGameController.totalBot3Score};
            string p_Name = leaderBoardManager.getUserNameLeaderBoard();
            string[] names = {p_Name,
                SceneManager.instance.PlayGameController.Name1,
            SceneManager.instance.PlayGameController.Name2,
            SceneManager.instance.PlayGameController.Name3};

            float[] rankImageFillA = { levelcalculator.calculateLevel(),
                SceneManager.instance.PlayGameController.bot1RankImagFill,
                SceneManager.instance.PlayGameController.bot2RankImagFill,
                SceneManager.instance.PlayGameController.bot3RankImagFill
            };

            int[] rankNumberA = { levelcalculator.getLevel(),
                SceneManager.instance.PlayGameController.bot1Rank,
                SceneManager.instance.PlayGameController.bot2Rank,
                SceneManager.instance.PlayGameController.bot3Rank
            };
            Sprite[] imagesAll = { leaderBoardManager.loadedSprite,
            SceneManager.instance.PlayGameController.bot1Sprite,
            SceneManager.instance.PlayGameController.bot2Sprite,
            SceneManager.instance.PlayGameController.bot3Sprite};

            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = 0; j < scores.Length; j++)
                {
                    if (scores[i] > scores[j])
                    {
                        int tempInt = scores[i];
                        string tempString = names[i];
                        scores[i] = scores[j];
                        names[i] = names[j];
                        scores[j] = tempInt;
                        names[j] = tempString;

                        //Image tempImage = allImages[i];
                        //allImages[i] = allImages[j];
                        //allImages[j] = tempImage;

                        //Text tempRank = rankTextAll[i];
                        //rankTextAll[i] = rankTextAll[j];
                        //rankTextAll[j] = tempRank;

                        float tempImageRank = rankImageFillA[i];
                        rankImageFillA[i] = rankImageFillA[j];
                        rankImageFillA[j] = tempImageRank;

                        int tempRankN = rankNumberA[i];
                        rankNumberA[i] = rankNumberA[j];
                        rankNumberA[j] = tempRankN;

                        Sprite tempSp = imagesAll[i];
                        imagesAll[i] = imagesAll[j];
                        imagesAll[j] = tempSp;

                    }
                }
            }
            playerName.text = names[0];
            playerScoreText.text = scores[0].ToString();
            bot1Name.text = names[1];
            bot1Score.text = scores[1].ToString();
            bot2Name.text = names[2];
            bot2Score.text = scores[2].ToString();
            bot3Name.text = names[3];
            bot3Score.text = scores[3].ToString();

            rankFillImageAll[0].fillAmount = rankImageFillA[0];
            rankFillImageAll[1].fillAmount = rankImageFillA[1];
            rankFillImageAll[2].fillAmount = rankImageFillA[2];
            rankFillImageAll[3].fillAmount = rankImageFillA[3];

            rankTextAll[0].text = "" + rankNumberA[0];
            rankTextAll[1].text = "" + rankNumberA[1];
            rankTextAll[2].text = "" + rankNumberA[2];
            rankTextAll[3].text = "" + rankNumberA[3];

            allImages[0].sprite = imagesAll[0];
            allImages[1].sprite = imagesAll[1];
            allImages[2].sprite = imagesAll[2];
            allImages[3].sprite = imagesAll[3];


            //if (SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot1Score
            //    && SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot2Score
            //    && SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot3Score)
            //{
            //    playerName.transform.parent.gameObject.transform.SetParent(allDataObject.transform);

            //    if (SceneManager.instance.PlayGameController.totalBot1Score > SceneManager.instance.PlayGameController.totalBot2Score
            //    && SceneManager.instance.PlayGameController.totalBot1Score > SceneManager.instance.PlayGameController.totalBot3Score)
            //    {
            //        bot1Name.transform.parent.gameObject.transform.SetParent(allDataObject.transform);
            //        if (SceneManager.instance.PlayGameController.totalBot2Score > SceneManager.instance.PlayGameController.totalBot3Score)
            //        {
            //            bot2Name.transform.parent.gameObject.transform.SetParent(allDataObject.transform);
            //        }
            //        else
            //        {
            //            bot3Name.transform.parent.gameObject.transform.SetParent(allDataObject.transform);
            //        }
            //    }
            //    else
            //    {

            //    }
            //}


            if (SceneManager.instance.PlayGameController.totalPlayerScore >= SceneManager.instance.PlayGameController.targetScore)
            {

            }

            positionOfWining = System.Array.IndexOf(scores, SceneManager.instance.PlayGameController.totalPlayerScore);
            if (positionOfWining == 1)
            {
                PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 20);

                PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);

                if (SceneManager.instance.PlayGameController.type == 1)
                {
                    leaderBoardManager.addDrawWinEvent();
                    checkAchivementTotalDrawDomino();
                }
                else if (SceneManager.instance.PlayGameController.type == 2)
                {
                    leaderBoardManager.addAllFiveWinEvent();
                    checkAchivementTotallFiveDomino();
                }
                else if (SceneManager.instance.PlayGameController.type == 3)
                {
                    leaderBoardManager.addBlockWinEvent();
                    checkAchivementTotalBlockDomino();
                }

            }
            else if (positionOfWining == 2)
            {
                PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 10);

                PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);

                {
                    leaderBoardManager.addLoseEvent();
                }
            }
            else if (positionOfWining == 3)
            {
                PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 5);

                PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);

                {
                    leaderBoardManager.addLoseEvent();
                }
            }else
            {
                leaderBoardManager.addLoseEvent();

                PlayerPrefs.SetInt("lose-games", PlayerPrefs.GetInt("lose-games", 0) + 1);
            }



        }
        else
        {
            //Debug.LogError("Issue Testing" +
            //    SceneManager.instance.PlayGameController.totalPlayerScore +
            //    " Player then bot1"+
            //    SceneManager.instance.PlayGameController.totalBot1Score);
            if (SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot1Score)
            {
                positionOfWining = 0;
                //StartCoroutine(leaderBoardManager.getUserImageLeaderRoutine(playerImage));
                //playerImage.sprite = leaderBoardManager.loadedSprite != null ? leaderBoardManager.loadedSprite : leaderBoardManager.sprite;
                //playerName.text = PlayerPrefs.GetString("nick-name", "Nick Name");
                playerName.text = leaderBoardManager.getUserNameLeaderBoard();
                playerScoreText.text = SceneManager.instance.PlayGameController.totalPlayerScore.ToString();
                bot1Name.text = SceneManager.instance.PlayGameController.Name1;
                bot1Score.text = SceneManager.instance.PlayGameController.totalBot1Score.ToString();

                allImages[0].sprite = leaderBoardManager.loadedSprite;
                allImages[1].sprite = SceneManager.instance.PlayGameController.bot1Sprite;

            }
            else
            {
                positionOfWining = 1;
                //StartCoroutine(leaderBoardManager.getUserImageLeaderRoutine(bigBotImage));
                //bigBotImage .sprite = leaderBoardManager.loadedSprite != null ? leaderBoardManager.loadedSprite : leaderBoardManager.sprite;
                playerName.text = SceneManager.instance.PlayGameController.Name1;
                playerScoreText.text = SceneManager.instance.PlayGameController.totalBot1Score.ToString();
                //bot1Name.text = PlayerPrefs.GetString("nick-name", "Nick Name"); 
                bot1Name.text = leaderBoardManager.getUserNameLeaderBoard();
                bot1Score.text = SceneManager.instance.PlayGameController.totalPlayerScore.ToString();

                allImages[1].sprite = leaderBoardManager.loadedSprite;
                allImages[0].sprite = SceneManager.instance.PlayGameController.bot1Sprite;
            }

            bot2Name.transform.parent.gameObject.SetActive(false);
            bot3Name.transform.parent.gameObject.SetActive(false);
            //text1.text = "You";
            //text2.text = SceneManager.instance.PlayGameController.Name1;

            if (SceneManager.instance.PlayGameController.totalPlayerScore >= SceneManager.instance.PlayGameController.targetScore)
            {
                PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 10);

                PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);

                if (SceneManager.instance.PlayGameController.type == 1)
                {
                    leaderBoardManager.addDrawWinEvent();
                    checkAchivementTotalDrawDomino();
                }
                else if (SceneManager.instance.PlayGameController.type == 2)
                {
                    leaderBoardManager.addAllFiveWinEvent();
                    checkAchivementTotallFiveDomino();
                }
                else if (SceneManager.instance.PlayGameController.type == 3)
                {
                    leaderBoardManager.addBlockWinEvent();
                    checkAchivementTotalBlockDomino();
                }
            }
            else
            {
                leaderBoardManager.addLoseEvent();
                PlayerPrefs.SetInt("lose-games", PlayerPrefs.GetInt("lose-games", 0) + 1);
            }
        }

        //allImages[positionOfWining].sprite = leaderBoardManager.loadedSprite;
        
        

        achievementCheckTotalGamePlayed();

        leaderBoardManager.OnAddScoreToLeaderBorad(PlayerPrefs.GetInt("points", 0));

        loadRank(positionOfWining);

        adsManager.showIntersticial();

        


        foreach (var item in Player)
        {
            item.sprite = null;
        }

        string _level = playerScore.ToString();
        playerText.text = _level;
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Player[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}

        foreach (var item in Oppo)
        {
            item.sprite = null;
        }

        _level = oppoScore.ToString();
        oppoText.text = _level;
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Oppo[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}
    }

    void loadRank(int val)
    {
        //levelCalculator lc = GameObject.FindObjectOfType<levelCalculator>();
        rankText.text = levelcalculator.getLevel() + "";
        rankFillImageAll[val].fillAmount = levelcalculator.calculateLevel();
        rankTextAll[val].text = "" + levelcalculator.getLevel();
    }


    public void OnPlayAgain()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.resetTotal();

        SceneManager.instance.PlayGameController.RandomName();
        SceneManager.instance.PlayGameController.NewGame();
        gameObject.SetActive(false);
    }

    public void OnBackClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.HidePlayGame();
        SceneManager.instance.ModeController.ShowMode();
        gameObject.SetActive(false);
    }

    public void OnShareClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    void achievementCheckTotalGamePlayed()
    {
        
        int totalPlayed = PlayerPrefs.GetInt("total-played", 0) + 1;
        PlayerPrefs.SetInt("total-played", totalPlayed);
        if (totalPlayed == 10)
        {
            int amountTemp = Random.Range(10, 50);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_10);
        }else if (totalPlayed == 100)
        {
            int amountTemp = Random.Range(100, 500);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_100);
        }
        else if (totalPlayed == 500)
        {
            int amountTemp = Random.Range(500, 2500);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_500);
        }
        else if (totalPlayed == 1000)
        {
            int amountTemp = Random.Range(1000, 5000);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_1000);
        }
        else if (totalPlayed == 5000)
        {
            int amountTemp = Random.Range(5000, 25000);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_5000);
        }
        else if (totalPlayed == 10000)
        {
            int amountTemp = Random.Range(10000, 50000);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_10000);
        }
        else if (totalPlayed == 50000)
        {
            int amountTemp = Random.Range(50000, 250000);
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_bonus_50000);
        }
    }

    void checkAchivementTotalDrawDomino()
    {
        int totalPlayed = PlayerPrefs.GetInt("total-draw-played", 0) + 1;
        PlayerPrefs.SetInt("total-draw-played", totalPlayed);
        if (totalPlayed == 100)
        {
            int amountTemp = 3000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_100_draw_dominoes);
        }
        else if (totalPlayed == 500)
        {
            int amountTemp = 20000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_500_draw_dominoes);
        }
    }
    void checkAchivementTotalBlockDomino()
    {
        int totalPlayed = PlayerPrefs.GetInt("total-block-played", 0) + 1;
        PlayerPrefs.SetInt("total-block-played", totalPlayed);
        if (totalPlayed == 100)
        {
            int amountTemp = 3000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_100_block_dominoes);
        }
        else if (totalPlayed == 500)
        {
            int amountTemp = 20000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_500_block_dominoes);
        }
    }
    void checkAchivementTotallFiveDomino()
    {
        int totalPlayed = PlayerPrefs.GetInt("total-five-played", 0) + 1;
        PlayerPrefs.SetInt("total-five-played", totalPlayed);
        if (totalPlayed == 100)
        {
            int amountTemp = 3000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_100_all_fives_dominoes);
        }
        else if (totalPlayed == 500)
        {
            int amountTemp = 20000;
            PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + amountTemp);
            StartCoroutine(popShow(amountTemp));
            leaderBoardManager.OnCompletionAchievement(GPGSIds.achievement_win_500_all_fives_dominoes);
        }
    }


    IEnumerator popShow(int amount)
    {
        rewardText.text = amount + "";
        rewardPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        rewardPanel.gameObject.SetActive(false);
    }
}
