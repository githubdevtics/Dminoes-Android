using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CheatData : MonoBehaviour {

    [MenuItem("CheatData/Clear UserData")]
    private static void ClearData()
    {
        PlayerPrefs.SetString("USER_DATA", "");
        PlayerPrefs.DeleteKey("TIP_DATA");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(SceneManager.RATE_DATA, 0);
        PlayerPrefs.SetInt(SceneManager.LEVEL_RATING, 0);
    }

    [MenuItem("CheatData/Cheat Win")]
    private static void CheatWin()
    {
        //if (SceneManager.instance.PlayGameController.GetComponent<CanvasGroup>().alpha == 1)
        //    SceneManager.instance.PlayGameController.WinGame();
    }
}
