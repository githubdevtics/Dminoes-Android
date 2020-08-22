using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingSoundController : MonoBehaviour
{
    public Sprite On, Off, TileSet1, TileSet2;
    public List<Sprite> BgPlayGames;
    public Image BgPlay, TileSet, DefaultHight;
    private int CurrentPlayBg = 0;
    private bool IsSet1 = false;
    private bool isAutoZoom = false;
    private bool isDefaultHight = false;
    public Image SliderMusic, SliderSound;
    public GameObject Music, Sound;

    void OnEnable()
    {
        InitUI();
    }

    private void InitUI()
    {
        try
        {
            CurrentPlayBg = PlayerPrefs.GetInt("BgPlayGames");
            BgPlay.sprite = BgPlayGames[CurrentPlayBg];
        }
        catch { };

        if (PlayerPrefs.GetInt("TileSet") == 0)
        {
            TileSet.sprite = TileSet1;
            IsSet1 = true;
        }
        else
        {
            TileSet.sprite = TileSet2;
            IsSet1 = false;
        }

        if(!PlayerPrefs.HasKey("DefaultHight"))
        {
            PlayerPrefs.SetInt("DefaultHight", 1);
        }

        if (PlayerPrefs.GetInt("DefaultHight") == 0)
        {
            isDefaultHight = false;
            DefaultHight.sprite = Off;
        }
        else
        {
            isDefaultHight = true;
            DefaultHight.sprite = On;
        }

        SliderMusic.fillAmount = PlayerPrefs.GetFloat(SoundManager.instance.MUSIC_VOLUME);
        SliderSound.fillAmount = PlayerPrefs.GetFloat(SoundManager.instance.SOUND_VOLUME);
        SetPosSlider(Music, SliderMusic.fillAmount);
        SetPosSlider(Sound, SliderSound.fillAmount);

    }

    public void OnSwitchBg(bool isLeft)
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);

        if (isLeft)
        {
            CurrentPlayBg--;
            if (CurrentPlayBg < 0)
                CurrentPlayBg = 2;
        }
        else
        {
            CurrentPlayBg++;
            if (CurrentPlayBg > 2)
                CurrentPlayBg = 0;
        }
        PlayerPrefs.SetInt("BgPlayGames", CurrentPlayBg);
        if(SceneManager.instance.PlayGameController.GetComponent<CanvasGroup>().alpha == 1)
        {
            // SceneManager.instance.PlayGameController.BgPlay.sprite = SceneManager.instance.PlayGameController.BgPlayGame[CurrentPlayBg];
        }
        BgPlay.sprite = BgPlayGames[CurrentPlayBg];
    }

    public void OnTileSetClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);

        if (IsSet1)
        {
            TileSet.sprite = TileSet2;
            PlayerPrefs.SetInt("TileSet", 1);
        }
        else
        {
            TileSet.sprite = TileSet1;
            PlayerPrefs.SetInt("TileSet", 0);
        }
        IsSet1 = !IsSet1;
    }

    public void OnDefaultHightClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);

        if (isDefaultHight)
        {
            DefaultHight.sprite = Off;
            PlayerPrefs.SetInt("DefaultHight", 0);
        }
        else
        {
            DefaultHight.sprite = On;
            PlayerPrefs.SetInt("DefaultHight", 1);
        }
        isDefaultHight = !isDefaultHight;
    }

    public void OnCloseClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        this.gameObject.SetActive(false);
        if(SceneManager.instance.PlayGameController.GetComponent<CanvasGroup>().alpha == 1)
        {
            SceneManager.instance.HideBanner();
        }
    }

    float endValue = 1.741791f;
    float startValue = -0.6800003f;

    public void OnDrag(GameObject slide)
    {
        float currentPos = 0f;
#if UNITY_EDITOR
        {
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            currentPos = v3.x;
        }
#else
    {
            if (Input.touchCount > 0)
            {
                var v3 = Input.touches[0].position;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                currentPos = v3.x;
            }
    }
#endif
        if (currentPos > startValue && currentPos < endValue)
        {
            slide.transform.position = new Vector2(currentPos, slide.transform.position.y);
        }
        else if (currentPos < startValue)
        {
            slide.transform.position = new Vector2(startValue, slide.transform.position.y);
        }
        else
        {
            slide.transform.position = new Vector2(endValue, slide.transform.position.y);
        }
        if (slide.name == "MusicControl")
        {
            SliderMusic.fillAmount = GetVolume(slide);
        }
        else
        {
            SliderSound.fillAmount = GetVolume(slide);
        }
    }

    public void OnEndDrag(GameObject slide)
    {
        if (slide.name == "MusicControl")
        {
            SoundManager.instance.SetMusic(GetVolume(slide));
        }
        else
        {
            SoundManager.instance.SetSound(GetVolume(slide));
        }
    }

    float GetVolume(GameObject slide)
    {
        float maxPercent = endValue - startValue;
        float currentPercent = slide.transform.position.x - startValue;

        return currentPercent / maxPercent;
    }

    void SetPosSlider(GameObject slide, float value)
    {
        float maxPercent = endValue - startValue;
        slide.transform.position = new Vector2(startValue + value * maxPercent, slide.transform.position.y);

    }
}
