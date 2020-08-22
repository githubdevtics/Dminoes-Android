using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TemplateLevelController : MonoBehaviour
{
    public List<Sprite> LevelNumber = new List<Sprite>();
    public List<Sprite> LevelNumberLock = new List<Sprite>();
    public List<Image> LevelName = new List<Image>();
    public List<Image> StarNum = new List<Image>();
    public Image BgLevel;

    private MapData CurrentData;

    public void InitData(MapData currentData)
    {
        CurrentData = currentData;

        string level = CurrentData.Level.ToString();
        foreach(var item in LevelName)
        {
            item.sprite = null;
        }

        for (int i = 0; i < 3; i++)
        {
            if (i < CurrentData.Star)
            {
                StarNum[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                StarNum[i].color = new Color(1, 1, 1, 0);
            }
        }

        if (CurrentData.IsLock)
        {
            BgLevel.color = new Color(1, 1, 1, 1);
            for (int i = 0; i < level.Length; i++)
            {
                LevelName[i].sprite = LevelNumberLock[int.Parse(level[i].ToString())];
            }
        }
        else
        {
            BgLevel.color = new Color(1, 1, 1, 0);
            for (int i = 0; i < level.Length; i++)
            {
                LevelName[i].sprite = LevelNumber[int.Parse(level[i].ToString())];
            }
        }
    }

    public void OnThisLevelClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        if (CurrentData != null && !CurrentData.IsLock)
        {
            SceneManager.instance.PlayGameController.TypeGame = 0;
            SceneManager.instance.LastLevel = CurrentData.Level;
            SceneManager.instance.CurrentMapData = CurrentData;
            SceneManager.instance.LevelController.HideLevel();
            SceneManager.instance.PlayGameController.ShowPlayGame();
        }
    }
}
