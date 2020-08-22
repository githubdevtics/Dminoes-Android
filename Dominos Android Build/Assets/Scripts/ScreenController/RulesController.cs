using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{

    public bool IsShowScreen;
    private CanvasGroup Rules;
    public List<Sprite> rule = new List<Sprite>();
    public List<RectTransform> target = new List<RectTransform>();
    public Image RulesImage;
    public int CurrentIndex = 0;
    public RectTransform ActiveRules;
    private float defaultX = 0;

    void Awake()
    {
        Rules = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        defaultX = ActiveRules.localPosition.x;
    }

    public void ShowMode()
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        RulesImage.transform.localScale = new Vector3(1 / SceneManager.instance.Ratio, 1f, 1f);
        Rules.alpha = 1;
        Rules.blocksRaycasts = true;
        gameObject.transform.localPosition = Vector2.zero;
        IsShowScreen = true;
        CurrentIndex = 0;
        RulesImage.sprite = rule[CurrentIndex];
        ActiveRules.localPosition = target[CurrentIndex].localPosition;
    }

    public void HideMode()
    {
        IsShowScreen = false;
        Rules.alpha = 0;
        Rules.blocksRaycasts = false;
        gameObject.transform.localPosition = new Vector2(10000, 10000);
    }

    public void OnBackClick()
    {
        HideMode();
        SceneManager.instance.HomeController.ShowHome();
        SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
    }

    void Update()
    {
        if (IsShowScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackClick();
            }
        }
    }

    public void OnNextClick(bool isLeft)
    {
		SoundManager.instance.SoundOn(SoundManager.SoundIngame.Click);
        CurrentIndex = isLeft ? CurrentIndex-=1 : CurrentIndex+=1;
        if (CurrentIndex < 0)
        {
            CurrentIndex = rule.Count - 1;
        }
        else if (CurrentIndex >= rule.Count)
        {
            CurrentIndex = 0;
        }
        RulesImage.sprite = rule[CurrentIndex];
        ActiveRules.localPosition = target[CurrentIndex].localPosition;
    }
}
