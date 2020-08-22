using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{

    public Sprite sprite;
    public Sprite loadedSprite = null;

    public Image userImage,userImage2;
    public Text nickName, NickName2;

    public ProfileHandler profileHandler;

    public Sprite[] allSprite;
    //public Sprite DownloadedImge;
    // Start is called before the first frame update
    void Start()
    {


        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();


        if (PlayerPrefs.GetString("autoloadLogin", "").Equals("load"))
        {
            LogIn();
        }
    }

    public void LogIn()
    {
        PlayerPrefs.SetString("autoloadLogin", "load");
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);

                getUserScoreLeaderBoard();

                loadImagesAndName();

                profileHandler.changesAfterGoogleLogIN();


                
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }

    public string getUserNameLeaderBoard()
    {
        try
        {
            if (Social.localUser.authenticated)
                return Social.localUser.userName;
            else
                return PlayerPrefs.GetString("nick-name", "Nick Name");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return PlayerPrefs.GetString("nick-name", "Nick Name");
        }
    }

    //public Sprite getUserImageLeaderBoard()
    //{
    //    try
    //    {
    //        if (Social.localUser.authenticated)
    //            return Sprite.Create(Social.localUser.image,
    //                new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    //        else
    //            return sprite;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError(ex.Message);
    //        return sprite;
    //    }
    //}

    public IEnumerator getUserImageLeaderRoutine(Image current_imag)
    {

        //if (loadedSprite != null)
        //{
        //    yield return new WaitForFixedUpdate();
        //    current_imag.sprite = loadedSprite;
        //}
        //else
        //{
        loadedSprite = allSprite[0];
        if (Social.localUser.authenticated)
            {
                Texture2D tex;
                while (Social.localUser.image == null)
                {
                    yield return null;
                }
                tex = Social.localUser.image;
                if (tex != null)
                {
                    loadedSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
                    current_imag.sprite = loadedSprite;
                }
                else
                {
                    loadedSprite = allSprite[0];
                }
            }
            else
            {
                int value = PlayerPrefs.GetInt("avatar-number", 0);
                current_imag.sprite = allSprite[value];
            }

        //}
    }

    public void getUserScoreLeaderBoard()
    {
        long score = 0;
        string mStatus = "";
        if (Social.localUser.authenticated)
        {
            Debug.LogError("My score status Method ma---  ");

            //PlayGamesPlatform.Instance.LoadScores(
            //     GPGSIds.leaderboard_score_leaderboards,
            //     LeaderboardStart.PlayerCentered,
            //     1,
            //     LeaderboardCollection.Public,
            //     LeaderboardTimeSpan.AllTime,
            //     (LeaderboardScoreData data) =>
            //     {
            //         score = data.PlayerScore.value;
            //         //Debug.Log(data.Valid);
            //         //Debug.Log(data.Id);
            //         //Debug.Log(data.PlayerScore);
            //         //Debug.Log(data.PlayerScore.userID);
            //         //Debug.Log(data.PlayerScore.formattedValue);
            //     }
            //     );

            PlayGamesPlatform.Instance.LoadScores(
           GPGSIds.leaderboard_score_leaderboards,
           LeaderboardStart.PlayerCentered,
           1,
           LeaderboardCollection.Public,
           LeaderboardTimeSpan.AllTime,
           (data) =>
           {
               mStatus = "Leaderboard data valid: " + data.Valid;
               mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
           });

            Debug.LogError("My score status ---  " + mStatus);
            //Debug.LogError("My score" + score);
            ////PlayerPrefs.SetInt("points", Convert.ToInt32( score )+ PlayerPrefs.GetInt("points", 0));
            //return score;
        }
        else
        {
            //return score;
            
        }
    }

    public void OnAddScoreToLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_score_leaderboards, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }

    public void showLeaderBoardGUI()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            LogIn();
        }
    }
    public void showAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            LogIn();
        }
    }
    

    public void addWinEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_win, 1);
    }
    public void addLoseEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_lose, 1);
    }
    public void addGamePlayedEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_game_played, 1);
    }
    public void addDrawWinEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_draw_dominoes_win, 1);
    }
    public void addBlockWinEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_block_dominoes_win, 1);
    }
    public void addAllFiveWinEvent()
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Events.IncrementEvent(GPGSIds.event_all_fives_dominoes_win, 1);
    }

    public void OnCompletionAchievement(string value)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(value, 100.0f, success =>
            {
                Debug.Log("Achivement Open -->" + value);
            });
        }
    }

    

   

    // Update is called once per frame
    void Update()
    {

    }

    void loadImagesAndName()
    {
        nickName.text = getUserNameLeaderBoard();
        NickName2.text = getUserNameLeaderBoard();

        //userImage.sprite = getUserImageLeaderBoard();
        //userImage2.sprite = getUserImageLeaderBoard();

        StartCoroutine( getUserImageLeaderRoutine(userImage));
        StartCoroutine( getUserImageLeaderRoutine(userImage2));

    }
}
