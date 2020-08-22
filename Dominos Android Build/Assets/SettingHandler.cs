using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingHandler : MonoBehaviour
{

    public RectTransform settingpanel;
    public Image soundImage, vibrationImage;
    public Sprite soundOn, soundoff;
    public Image[] tilesImages;
    public Sprite[] unSelectedTiles;
    public Sprite[] selectedTiles;
    public Image[] themesImages;
    public Sprite[] unSelectedThemes;
    public Sprite[] selectedThemes;
    public Image[] difficulityImages;
    public Sprite[] unSelectedDifficulity;
    public Sprite[] selectedDifficulity;

    public Image[] tempDifficulity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSetting()
    {
        updateSoundUI();
        updateVibrationUI();
        updateThemesUI();
        updateDifficulityUI();
        updateTempDifficulityUI();
        updateTilesUI();
        settingpanel.gameObject.SetActive(true);
    }
    public void updateSoundUI()
    {
        if (PlayerPrefs.GetInt("SOUND", 1) == 1)
        {
            soundImage.sprite = soundOn;
        }
        else
        {
            soundImage.sprite = soundoff;
        }
    }
    public void changeGameSound()
    {
        PlayerPrefs.SetInt("SOUND", -1 * PlayerPrefs.GetInt("SOUND", 1));
        SoundManager.instance.SetSound(PlayerPrefs.GetInt("SOUND", 1) ==1 ? 100f : 0f);
        updateSoundUI();
    }
    public void updateVibrationUI()
    {
        if (PlayerPrefs.GetInt("vibration", 1) == 1)
        {
            vibrationImage.sprite = soundOn;
        }
        else
        {
            vibrationImage.sprite = soundoff;
        }
    }
    public void changeGameVibration()
    {
        PlayerPrefs.SetInt("vibration", -1 * PlayerPrefs.GetInt("vibration", 1));
        updateVibrationUI();
    }

    public void updateTilesUI()
    {
        for (int i = 0; i < tilesImages.Length; i++)
        {
            tilesImages[i].sprite = unSelectedTiles[i];
        }
        int value = PlayerPrefs.GetInt("TILES", 0);
        tilesImages[value].sprite = selectedTiles[value];
    }
    public void changeTile(int value)
    {
        PlayerPrefs.SetInt("TILES", value);
        updateTilesUI();
    }
    public void updateThemesUI()
    {
        
        for (int i = 0; i < themesImages.Length; i++)
        {
            themesImages[i].sprite = unSelectedThemes[i];
        }
        int value = PlayerPrefs.GetInt("BG", 0);
        themesImages[value].sprite = selectedThemes[value];
    }
    public void changeTheme(int value)
    {
        PlayerPrefs.SetInt("BG", value);
        updateThemesUI();
    }

    public void updateDifficulityUI()
    {

        for (int i = 0; i < difficulityImages.Length; i++)
        {
            difficulityImages[i].sprite = unSelectedDifficulity[i];
        }
        int value = PlayerPrefs.GetInt("ANIM", 0);
        difficulityImages[value].sprite = selectedDifficulity[value];
    }
    public void changeDifficulity(int value)
    {
        PlayerPrefs.SetInt("ANIM", value);
        if (value == 0)
        {
            PlayerPrefs.SetInt("target-score", 50);
        }
        else if (value == 1)
        {
            PlayerPrefs.SetInt("target-score", 100);
        }
        else
        {
            PlayerPrefs.SetInt("target-score", 150);
        }
        updateDifficulityUI();
    }

    public void updateTempDifficulityUI()
    {

        for (int i = 0; i < difficulityImages.Length; i++)
        {
            tempDifficulity[i].sprite = unSelectedDifficulity[i];
        }
        int value = PlayerPrefs.GetInt("tempDeficulity", 0);
        tempDifficulity[value].sprite = selectedDifficulity[value];
    }
    public void changeTempDifficulity(int value)
    {
        PlayerPrefs.SetInt("tempDeficulity", value);
        updateTempDifficulityUI();
    }
    public void openLink(string value)
    {
        Application.OpenURL(value);
    }
    public void closePanel(GameObject go)
    {
        go.SetActive(false);
    }
}
