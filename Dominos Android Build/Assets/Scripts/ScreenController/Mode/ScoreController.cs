using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public List<Text> Puzzle = new List<Text>();
    public List<Text> Draw = new List<Text>();
    public List<Text> AllFive = new List<Text>();
    public List<Text> Block = new List<Text>();

    public void InitBoard()
    {
        var puzzle = PlayerPrefs.GetString("PUZZLE_SCORE");
        if (puzzle == "")
        {
            foreach (var item in Puzzle)
            {
                item.text = "0";
            }
        }
        else
        {
            var parts = puzzle.Split('-');
            Puzzle[0].text = parts[0];
            Puzzle[1].text = parts[1];
            Puzzle[2].text = parts[2];
            Puzzle[3].text = parts[3];

        }

        var draw = PlayerPrefs.GetString("DRAW_SCORE");
        if (draw == "")
        {
            foreach(var item in Draw)
            {
                item.text = "0";
            }
        }
        else
        {
            var parts = draw.Split('-');
            Draw[0].text = parts[0];
            Draw[1].text = parts[1];
            Draw[2].text = parts[2];
            Draw[3].text = parts[3];
			Draw[4].text = parts[4];
        }

        var alLFive = PlayerPrefs.GetString("ALLFIVE_SCORE");
        if (alLFive == "")
        {
            foreach (var item in AllFive)
            {
                item.text = "0";
            }
        }
        else
        {
            var parts = alLFive.Split('-');
            AllFive[0].text = parts[0];
            AllFive[1].text = parts[1];
            AllFive[2].text = parts[2];
            AllFive[3].text = parts[3];
			AllFive[4].text = parts[4];
        }

        var block = PlayerPrefs.GetString("BLOCK_SCORE");
        if (block == "")
        {
            foreach (var item in Block)
            {
                item.text = "0";
            }
        }
        else
        {
            var parts = block.Split('-');
            Block[0].text = parts[0];
            Block[1].text = parts[1];
            Block[2].text = parts[2];
            Block[3].text = parts[3];
			Block[4].text = parts[4];
        }
    }

    public void ClickBack()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        this.gameObject.SetActive(false);
    }

    public void OnCleanClick()
    {
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        PlayerPrefs.SetString("DRAW_SCORE", "");
        PlayerPrefs.SetString("ALLFIVE_SCORE", "");
        PlayerPrefs.SetString("BLOCK_SCORE", "");
        InitBoard();
    }
}
