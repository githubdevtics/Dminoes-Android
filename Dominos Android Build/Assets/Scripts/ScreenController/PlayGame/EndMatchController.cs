using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchController : MonoBehaviour {

    

    public List<Sprite> Number;
    public List<Image> Player, Player1, Oppo, Oppo1;
    public Text playerText, player1Text, oppoText, oppoText1;
    public GameObject Name;
    public Text text1, text2;
    public Text rankText;
    public Image rankFillImage;
    public Image playerImage;


    // kha add
    public GameObject playerDominoesParent, bot1SDominoesParent, bot1BDominoesParent, bot2DominoesParent, bot3DominoesParent;
    public GameObject bot1SDP, bot1BDP, bot2DP, bot3DP;
    public GameObject endGameDominoesPrefabs;
    public Image bot1BImage, bot1SImage, bot2Image, bot3Image;
    //public Text bot1BRankText, bot1SRankText, bot2RankText, bot3RankText;
    //public Image bot1BRankImage, botSRankImage, bot2RankImage, bot3RankImage;


    public Text playerNameText, bot1BNameText, bot1SNameText, bot2NameText, bot3NameText;
    public Text playerScoreText, bot1BScoreText, bot1SScoreText, bot2ScoreText, bot3ScoreText;

    public LeaderBoardManager leaderBoardManager;
    public AdsManager adsManager;

    void loadInfoData()
    {
        

        playerNameText.text = SceneManager.instance.PlayGameController.playerNameText.text.ToString();
        playerScoreText.text = SceneManager.instance.PlayGameController.totalPlayerScore + "/" + SceneManager.instance.PlayGameController.targetScore;
        if (SceneManager.instance.PlayGameController.CurrentMaxPlayer == 2)
        {
            bot1BNameText.text = SceneManager.instance.PlayGameController.Name1;
            bot1BScoreText.text = SceneManager.instance.PlayGameController.totalBot1Score + "/" + SceneManager.instance.PlayGameController.targetScore;

            bot1BImage.sprite = SceneManager.instance.PlayGameController.bot1Sprite;
        }
        else
        {
            bot1SNameText.text = SceneManager.instance.PlayGameController.Name1;
            bot1SScoreText.text = SceneManager.instance.PlayGameController.totalBot1Score + "/" + SceneManager.instance.PlayGameController.targetScore;
            bot1SImage.sprite = SceneManager.instance.PlayGameController.bot1Sprite;

            bot2NameText.text = SceneManager.instance.PlayGameController.Name2;
            bot2ScoreText.text = SceneManager.instance.PlayGameController.totalBot2Score + "/" + SceneManager.instance.PlayGameController.targetScore;
            bot2Image.sprite = SceneManager.instance.PlayGameController.bot2Sprite;

            bot3NameText.text = SceneManager.instance.PlayGameController.Name3;
            bot3ScoreText.text = SceneManager.instance.PlayGameController.totalBot3Score + "/" + SceneManager.instance.PlayGameController.targetScore;
            bot3Image.sprite = SceneManager.instance.PlayGameController.bot3Sprite;
        }
    }
    void showEndDominous()
    {

        bot1SDP.gameObject.SetActive(false);
        bot1BDP.gameObject.SetActive(false);
        bot2DP.gameObject.SetActive(false);
        bot3DP.gameObject.SetActive(false);

        while (playerDominoesParent.transform.childCount > 0)
        {
            Transform child = playerDominoesParent.transform.GetChild(0);
            Destroy(child.gameObject);
            child.SetParent(null);
        }
        
        for (int i = 0; i < SceneManager.instance.PlayGameController.PlayerDominoes[0].Count; i++)
        {
            GameObject go = Instantiate(endGameDominoesPrefabs) as GameObject;
            go.GetComponent<Image>().sprite = SceneManager.instance.PlayGameController.PlayerDominoes[0][i].GetUI();
            go.transform.SetParent(playerDominoesParent.transform);
            go.transform.localScale = Vector3.one;

        }
        Debug.LogError(SceneManager.instance.PlayGameController.PlayerDominoes.Count);




        if (SceneManager.instance.PlayGameController.CurrentMaxPlayer == 2)
        {
            while (bot1BDominoesParent.transform.childCount > 0)
            {
                Transform child = bot1BDominoesParent.transform.GetChild(0);
                Destroy(child.gameObject);
                child.SetParent(null);
            }

            for (int i = 0; i < SceneManager.instance.PlayGameController.PlayerDominoes[1].Count; i++)
            {
                GameObject go = Instantiate(endGameDominoesPrefabs) as GameObject;
                go.GetComponent<Image>().sprite = SceneManager.instance.PlayGameController.PlayerDominoes[1][i].GetUI();
                go.transform.SetParent(bot1BDominoesParent.transform);
                go.transform.localScale = Vector3.one;
            }
            bot1BDP.gameObject.SetActive(true);
            
        }
        else
        {
            while (bot1SDominoesParent.transform.childCount > 0)
            {
                Transform child = bot1SDominoesParent.transform.GetChild(0);
                Destroy(child.gameObject);
                child.SetParent(null);
            }

            for (int i = 0; i < SceneManager.instance.PlayGameController.PlayerDominoes[1].Count; i++)
            {
                GameObject go = Instantiate(endGameDominoesPrefabs) as GameObject;
                go.GetComponent<Image>().sprite = SceneManager.instance.PlayGameController.PlayerDominoes[1][i].GetUI();
                go.transform.SetParent(bot1SDominoesParent.transform);
                go.transform.localScale = Vector3.one;
            }
            bot1SDP.gameObject.SetActive(true);


            while (bot2DominoesParent.transform.childCount > 0)
            {
                Transform child = bot2DominoesParent.transform.GetChild(0);
                Destroy(child.gameObject);
                child.SetParent(null);
            }

            for (int i = 0; i < SceneManager.instance.PlayGameController.PlayerDominoes[2].Count; i++)
            {
                GameObject go = Instantiate(endGameDominoesPrefabs) as GameObject;
                go.GetComponent<Image>().sprite = SceneManager.instance.PlayGameController.PlayerDominoes[2][i].GetUI();
                go.transform.SetParent(bot2DominoesParent.transform);
                go.transform.localScale = Vector3.one;
            }
            bot2DP.gameObject.SetActive(true);

            while (bot3DominoesParent.transform.childCount > 0)
            {
                Transform child = bot3DominoesParent.transform.GetChild(0);
                Destroy(child.gameObject);
                child.SetParent(null);
            }

            for (int i = 0; i < SceneManager.instance.PlayGameController.PlayerDominoes[3].Count; i++)
            {
                GameObject go = Instantiate(endGameDominoesPrefabs) as GameObject;
                go.GetComponent<Image>().sprite = SceneManager.instance.PlayGameController.PlayerDominoes[3][i].GetUI();
                go.transform.SetParent(bot3DominoesParent.transform);
                go.transform.localScale = Vector3.one;
            }
            bot3DP.gameObject.SetActive(true);

        }

    }
    //kha

    public void InitUI(int playerScore, int oppoScore, int total1, int total2)
    {
        //StartCoroutine(leaderBoardManager.getUserImageLeaderRoutine(playerImage));
        playerImage.sprite = leaderBoardManager.loadedSprite != null ? leaderBoardManager.loadedSprite : leaderBoardManager.sprite;


        Name.SetActive(true);
        if (SceneManager.instance.PlayGameController.CurrentMaxPlayer == 4)
        {
            text1.text = "You & " + SceneManager.instance.PlayGameController.Name1;
            text2.text = SceneManager.instance.PlayGameController.Name2 + " & " + SceneManager.instance.PlayGameController.Name3;
        }
        else
        {
            text1.text = "You";
            text2.text = SceneManager.instance.PlayGameController.Name1;
        }

        //foreach (var item in Player)
        //{
        //    item.sprite = null;
        //}

        //string _level = total1.ToString();
        //playerText.text = _level;
        playerText.text = total1.ToString();
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Player[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}

        //foreach (var item in Oppo)
        //{
        //    item.sprite = null;
        //}

        //_level = total2.ToString();
        //oppoText.text = _level;
        oppoText.text = total2.ToString();
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Oppo[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}

        //foreach (var item in Player1)
        //{
        //    item.sprite = null;
        //}

        //_level = playerScore.ToString();
        //player1Text.text = _level;
        player1Text.text = playerScore.ToString();
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Player1[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}

        //foreach (var item in Oppo1)
        //{
        //    item.sprite = null;
        //}

        //_level = oppoScore.ToString();
        //oppoText1.text = _level;
        oppoText1.text = oppoScore.ToString();
        //for (int i = 0; i < _level.Length; i++)
        //{
        //    Oppo1[i].sprite = Number[int.Parse(_level[i].ToString())];
        //}


        //if (playerScore < oppoScore)
        //{
        //    //player won
        //    PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("lose-games", PlayerPrefs.GetInt("lose-games", 0) + 1);
        //}
        //if (SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot1Score &&
        //    SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot2Score &&
        //    SceneManager.instance.PlayGameController.totalPlayerScore > SceneManager.instance.PlayGameController.totalBot3Score)
        //{
        //    PlayerPrefs.SetInt("win-games", PlayerPrefs.GetInt("win-games", 0) + 1);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("lose-games", PlayerPrefs.GetInt("lose-games", 0) + 1);
        //}


        //kha add
        showEndDominous();
        loadInfoData();

        loadRank();
        //SceneManager.instance.PlayGameController.updateTotal();
        Debug.LogError(SceneManager.instance.PlayGameController.totalPlayerScore);

        adsManager.showIntersticial();

    }

    void loadRank()
    {
        levelCalculator lc = GameObject.FindObjectOfType<levelCalculator>();
        rankText.text = lc.getLevel() + "";
        rankFillImage.fillAmount = lc.calculateLevel();
    }

    public void OnPlayAgain()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.NewGame();
        gameObject.SetActive(false);
    }

}
