using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public List<Image> Speed = new List<Image>();
    public List<Image> Tiles = new List<Image>();
    public List<Image> BG = new List<Image>();
    public Sprite Select, Unselect;

    void Start()
    {
        SetSpeed();
        SetTiles();
        SetBG();
    }

    private void SetSpeed()
    {
        var temp = PlayerPrefs.HasKey("ANIM") ? PlayerPrefs.GetInt("ANIM") : 1;
        for (int i = 0; i < Speed.Count; i++)
        {
            Speed[i].sprite = i == temp ? Select : Unselect;
        }
    }

    private void SetTiles()
    {
        var temp = PlayerPrefs.GetInt("TILES");
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].sprite = i == temp ? Select : Unselect;
        }
    }

    private void SetBG()
    {
        var temp = PlayerPrefs.GetInt("BG");
        for (int i = 0; i < BG.Count; i++)
        {
            BG[i].sprite = i == temp ? Select : Unselect;
        }
        SceneManager.instance.PlayGameController.ChangeBg();
    }

    public void OnQuitGameClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.instance.PlayGameController.ShowQuitGame();
    }

    public void QuitGame()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        SceneManager.instance.PlayGameController.HidePlayGame();
        SceneManager.instance.ModeController.ShowMode();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnCloseSetting()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnAnimSpeedClick(GameObject obj)
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        PlayerPrefs.SetInt("ANIM", int.Parse(obj.name));
        SetSpeed();
        SceneManager.instance.PlayGameController.ChangeSpeed();
    }

    public void OnTypeOfTilesClick(GameObject obj)
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        PlayerPrefs.SetInt("TILES", int.Parse(obj.name));
        SetTiles();
        SceneManager.instance.PlayGameController.ChangeTiles();
    }

    public void OnBGClick(GameObject obj)
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        PlayerPrefs.SetInt("BG", int.Parse(obj.name));
        SetBG();
    }
}
