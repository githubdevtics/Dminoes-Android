using UnityEngine;
using System.Collections;

public class QuitGameController : MonoBehaviour
{
    public void OnNoClick()
    {
        SceneManager.instance.ShowBanner();
        this.gameObject.SetActive(false);
    }

    public void OnYesClick()
    {
        Application.Quit();
    }

    public void OnMoreGameClick()
    {
#if UNITY_ANDROID
        Application.OpenURL ("market://search?id="+Application.companyName+"");
#endif
    }
}
