using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using AudienceNetwork;
using System;

public class AdsManager : MonoBehaviour
{

    public LeaderBoardManager leaderBoardManager;

    private GoogleMobileAds.Api.InterstitialAd interstitial;
    public string GInterstitialadUnitId;

    private RewardedAd rewardedAd;
    public string GRVadUnitId;


    //Facebook
    private AudienceNetwork.InterstitialAd FBinterstitialAd;
    public string FBinterstitialID;
    private bool isLoaded;
#pragma warning disable 0414
    private bool didClose;
#pragma warning restore 0414

    private RewardedVideoAd FBrewardedVideoAd;
    public string FBRVID;
    private bool FBRVisLoaded;


    public float numberInter = 0;

    public RectTransform adNotLoadedPanel, receivedPanel;

    private void Awake()
    {
        AudienceNetworkAds.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        loadGoogleAds();
        LoadInterstitial();
        LoadFBRewardedVideo();
    }

    void loadGoogleAds()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        // Initialize an InterstitialAd.
        this.interstitial = new GoogleMobileAds.Api.InterstitialAd(GInterstitialadUnitId);
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);


        this.rewardedAd = new RewardedAd(GRVadUnitId);
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Create an empty ad request.
        AdRequest requestRV = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(requestRV);
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 5);
        leaderBoardManager.OnAddScoreToLeaderBorad(PlayerPrefs.GetInt("points", 0));
        loadRank();
        StartCoroutine(receivedReward());

        // Create an empty ad request.
        AdRequest requestRV = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(requestRV);
    }

    public void showIntersticial()
    {
        try
        {
            if (numberInter % 2 == 0)
            {

                if (isLoaded)
                {
                    FBinterstitialAd.Show();
                    isLoaded = false;

                }
                else if (this.interstitial.IsLoaded())
                {
                    this.interstitial.Show();
                }
            }

            numberInter += 1;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void UserChoseToWatchAd()
    {
        if (isLoaded)
        {
            FBrewardedVideoAd.Show();
            isLoaded = false;

        }
        else if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            StartCoroutine(noAdAvailabl());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadInterstitial()
    {


        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        FBinterstitialAd = new AudienceNetwork.InterstitialAd(FBinterstitialID);

        FBinterstitialAd.Register(gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        FBinterstitialAd.InterstitialAdDidLoad = delegate ()
        {
            Debug.Log("Interstitial ad loaded.");
            isLoaded = true;
            didClose = false;
            //string isAdValid = FBinterstitialAd.IsValid() ? "valid" : "invalid";
            //statusLabel.text = "Ad loaded and is " + isAdValid + ". Click show to present!";
        };
        FBinterstitialAd.InterstitialAdDidFailWithError = delegate (string error)
        {
            Debug.Log("Interstitial ad failed to load with error: " + error);
            //statusLabel.text = "Interstitial ad failed to load. Check console for details.";
        };
        FBinterstitialAd.InterstitialAdWillLogImpression = delegate ()
        {
            Debug.Log("Interstitial ad logged impression.");
        };
        FBinterstitialAd.InterstitialAdDidClick = delegate ()
        {
            Debug.Log("Interstitial ad clicked.");
        };
        FBinterstitialAd.InterstitialAdDidClose = delegate ()
        {
            Debug.Log("Interstitial ad did close.");
            didClose = true;
            if (FBinterstitialAd != null)
            {
                FBinterstitialAd.Dispose();
            }
        };

#if UNITY_ANDROID
        /*
         * Only relevant to Android.
         * This callback will only be triggered if the Interstitial activity has
         * been destroyed without being properly closed. This can happen if an
         * app with launchMode:singleTask (such as a Unity game) goes to
         * background and is then relaunched by tapping the icon.
         */
        FBinterstitialAd.interstitialAdActivityDestroyed = delegate () {
            if (!didClose)
            {
                Debug.Log("Interstitial activity destroyed without being closed first.");
                Debug.Log("Game should resume.");
            }
        };
#endif

        // Initiate the request to load the ad.
        FBinterstitialAd.LoadAd();
    }

    public void LoadFBRewardedVideo()
    {
        try
        {

            // Create the rewarded video unit with a placement ID (generate your own on the Facebook app settings).
            // Use different ID for each ad placement in your app.
            FBrewardedVideoAd = new RewardedVideoAd(FBRVID);

            // For S2S validation you can create the rewarded video ad with the reward data
            // Refer to documentation here:
            // https://developers.facebook.com/docs/audience-network/android/rewarded-video#server-side-reward-validation
            // https://developers.facebook.com/docs/audience-network/ios/rewarded-video#server-side-reward-validation
            //        RewardData rewardData = new RewardData
            //        {
            //            UserId = "USER_ID",
            //            Currency = "REWARD_ID"
            //        };
            //#pragma warning disable 0219
            //        RewardedVideoAd s2sRewardedVideoAd = new RewardedVideoAd("YOUR_PLACEMENT_ID", rewardData);
            //#pragma warning restore 0219

            FBrewardedVideoAd.Register(gameObject);

            // Set delegates to get notified on changes or when the user interacts with the ad.
            FBrewardedVideoAd.RewardedVideoAdDidLoad = delegate ()
            {
                Debug.Log("RewardedVideo ad loaded.");
                isLoaded = true;
                didClose = false;
                string isAdValid = FBrewardedVideoAd.IsValid() ? "valid" : "invalid";

            };
            FBrewardedVideoAd.RewardedVideoAdDidFailWithError = delegate (string error)
            {
                Debug.Log("RewardedVideo ad failed to load with error: " + error);

            };
            FBrewardedVideoAd.RewardedVideoAdWillLogImpression = delegate ()
            {
                Debug.Log("RewardedVideo ad logged impression.");
            };
            FBrewardedVideoAd.RewardedVideoAdDidClick = delegate ()
            {
                Debug.Log("RewardedVideo ad clicked.");
            };

            // For S2S validation you need to register the following two callback
            // Refer to documentation here:
            // https://developers.facebook.com/docs/audience-network/android/rewarded-video#server-side-reward-validation
            // https://developers.facebook.com/docs/audience-network/ios/rewarded-video#server-side-reward-validation
            FBrewardedVideoAd.RewardedVideoAdDidSucceed = delegate ()
            {
                Debug.Log("Rewarded video ad validated by server");


                

            };

            FBrewardedVideoAd.RewardedVideoAdDidFail = delegate ()
            {
                Debug.Log("Rewarded video ad not validated, or no response from server");
            };

            FBrewardedVideoAd.RewardedVideoAdDidClose = delegate ()
            {
                Debug.Log("Rewarded video ad did close.");
                didClose = true;
                if (FBrewardedVideoAd != null)
                {
                    FBrewardedVideoAd.Dispose();

                    PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points", 0) + 5);
                    leaderBoardManager.OnAddScoreToLeaderBorad(PlayerPrefs.GetInt("points", 0));
                    loadRank();
                    StartCoroutine(receivedReward());

                    FBrewardedVideoAd.LoadAd();

                    
                }
            };

#if UNITY_ANDROID
            /*
             * Only relevant to Android.
             * This callback will only be triggered if the Rewarded Video activity
             * has been destroyed without being properly closed. This can happen if
             * an app with launchMode:singleTask (such as a Unity game) goes to
             * background and is then relaunched by tapping the icon.
             */
            FBrewardedVideoAd.RewardedVideoAdActivityDestroyed = delegate ()
            {
                if (!didClose)
                {
                    Debug.Log("Rewarded video activity destroyed without being closed first.");
                    Debug.Log("Game should resume. User should not get a reward.");
                }
            };
#endif

            // Initiate the request to load the ad.
            FBrewardedVideoAd.LoadAd();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnDestroy()
    {
        // Dispose of interstitial ad when the scene is destroyed
        if (FBinterstitialAd != null)
        {
            FBinterstitialAd.Dispose();
        }
        Debug.Log("InterstitialAd was destroyed!");
        if (FBrewardedVideoAd != null)
        {
            FBrewardedVideoAd.Dispose();
        }
    }



    void loadRank()
    {
        ProfileHandler lc = GameObject.FindObjectOfType<ProfileHandler>();
        lc.updateLevelInfo();
    }


    IEnumerator noAdAvailabl()
    {
        adNotLoadedPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        adNotLoadedPanel.gameObject.SetActive(false);
    }
    IEnumerator receivedReward()
    {
        receivedPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        receivedPanel.gameObject.SetActive(false);
    }
}
