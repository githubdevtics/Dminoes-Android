using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileHandler : MonoBehaviour
{

    public RectTransform profilePanel, namePanel, avatarPanel;
    public Text nickNameText,nickNameText2, winText, loseText, scoreText, rankingText, rankText1, rankText2;
    public Image rankFillImage, rankFillImage2;
    public InputField nickNameInputField;
    public Button[] avatarButtonsPosition;
    public Image selectImage;
    public Image ImageMainPage, imageProfilePanel;
    public Sprite[] allAvatarSprite;
    public levelCalculator levelCalculator;

    public RectTransform profileBG;
    public Button nickNameButton, profilePicButton;


    // Start is called before the first frame update
    void Start()
    {
        updateNickName();
        updateAvatarUI();
        updateLevelInfo();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openProfile()
    {
        if (!PlayerPrefs.GetString("autoloadLogin", "").Equals("load"))
        {
            winText.text = PlayerPrefs.GetInt("win-games", 0).ToString();
            loseText.text = PlayerPrefs.GetInt("lose-games", 0).ToString();
            scoreText.text = PlayerPrefs.GetInt("points", 0).ToString();
            rankingText.text = "" + levelCalculator.getLevel();
            if (PlayerPrefs.GetString("autoloadLogin", "").Equals("load"))
                changesAfterGoogleLogIN();

            profilePanel.gameObject.SetActive(true);
        }
    }
    public void openNamePanel()
    {
        nickNameInputField.text = PlayerPrefs.GetString("nick-name", "Nick Name");
        namePanel.gameObject.SetActive(true);
    }
    public void setNickName()
    {
        PlayerPrefs.SetString("nick-name", nickNameInputField.text.ToString());
        updateNickName();
        namePanel.gameObject.SetActive(false);
    }
    void updateNickName()
    {
        nickNameText.text = PlayerPrefs.GetString("nick-name", "Nick Name");
        nickNameText2.text = PlayerPrefs.GetString("nick-name", "Nick Name");
    }
    public void openAvatarPanel()
    {
        avatarPanel.gameObject.SetActive(true);
    }
    public void setAvatar(int value)
    {
        PlayerPrefs.SetInt("avatar-number", value);
        updateAvatarUI();
    }
    void updateAvatarUI()
    {
        int value = PlayerPrefs.GetInt("avatar-number", 0);
        selectImage.rectTransform.anchoredPosition = avatarButtonsPosition[value].image.rectTransform.anchoredPosition;
        ImageMainPage.sprite = allAvatarSprite[value];
        imageProfilePanel.sprite = allAvatarSprite[value];
    }

    public void updateLevelInfo()
    {
        levelCalculator lc = GameObject.FindObjectOfType<levelCalculator>();
        rankText1.text = "" + lc.getLevel();
        rankText2.text = "" + lc.getLevel();
        rankFillImage.fillAmount = lc.calculateLevel();
        rankFillImage2.fillAmount = lc.calculateLevel();
        
    }

    public void changesAfterGoogleLogIN()
    {
        nickNameButton.gameObject.SetActive(false);
        profilePicButton.gameObject.SetActive(false);
        profileBG.sizeDelta = new Vector3(500f, 660f);
    }
}
