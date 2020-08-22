using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    private const int LevelPerPage = 20;
    public GameObject PosScroll;
    public GameObject TemplatePos;
    public List<Image> TemplatePosList = new List<Image>();
    public Sprite CurrentPos, NormalPos;

    private CanvasGroup Level;
    public GameObject TemplateLevel;
    public List<RectTransform> Pages = new List<RectTransform>();
    public List<CanvasGroup> CanvasPage = new List<CanvasGroup>();
    public ScrollRect ScrollRect;
    public RectTransform ScrollView;

    private int currentPage = 0;
    private int currentPoolPage = 1;
    private int maxPage = 0;
    public List<List<TemplateLevelController>> PoolListController = new List<List<TemplateLevelController>>();
    public Text StarText;
    public bool IsShowScreen;

    void Awake()
    {
        Level = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        int temp = 0;
        if (SceneManager.instance.MaxLevel % LevelPerPage != 0)
            temp = 1;
        maxPage = SceneManager.instance.MaxLevel / LevelPerPage + temp;
        InstanceObjectPool();
        InitLevel();
        InitScrollPos();
    }

    private void InitScrollPos()
    {
        for(int i = 0; i < maxPage;i++)
        {
            GameObject template = Instantiate(TemplatePos) as GameObject;
            template.transform.SetParent(PosScroll.transform);
            template.transform.localScale = Vector3.one;
            template.SetActive(true);
            TemplatePosList.Add(template.GetComponentInChildren<Image>());
        }
        TemplatePosList[currentPage - 1].sprite = CurrentPos;
    }

    private void InitLevel()
    {
        int lastLevel = SceneManager.instance.LastLevel + 1 > 300 ? 300 : SceneManager.instance.LastLevel;

        int temp = 1;
        if (lastLevel % LevelPerPage == 0)
            temp = 0;
        if (currentPage != 0 && currentPage - 1 < TemplatePosList.Count)
        {
            TemplatePosList[currentPage - 1].sprite = NormalPos;
        }
        currentPage = lastLevel / LevelPerPage + temp;

        if (currentPage != 0 && currentPage - 1 < TemplatePosList.Count)
        {
            TemplatePosList[currentPage - 1].sprite = CurrentPos;
        }

        SetDataPerPage(1, currentPage);

        if (currentPage + 1 <= maxPage)
        {
            SetDataPerPage(2, currentPage + 1);
        }
        else
        {
            CanvasPage[2].alpha = 0;
        }

        if (currentPage == 1)
        {
            CanvasPage[0].alpha = 0;
        }
        else
        {
            SetDataPerPage(0, currentPage - 1);
        }

        Pages[0].localPosition = new Vector2(-720f,0);
        Pages[1].localPosition = Vector2.zero;
        Pages[2].localPosition = new Vector2(720f, 0);

    }

    public void SetDataPerPage(int poolPage, int curPage)
    {
        CanvasPage[poolPage].alpha = 1;

        int tempIndex = SceneManager.instance.MaxLevel / LevelPerPage + 1;
        int maxInit = 0;
        if (curPage == tempIndex)
            maxInit = SceneManager.instance.MaxLevel % LevelPerPage;
        else maxInit = LevelPerPage;
        for (int i = 0; i < LevelPerPage; i++)
        {
            if (i < maxInit)
            {
                PoolListController[poolPage][i].transform.localScale = Vector3.one;
                PoolListController[poolPage][i].InitData(SceneManager.instance.AllMapData[(curPage - 1) * LevelPerPage + i]);
            }
            else
            {
                PoolListController[poolPage][i].transform.localScale = Vector3.zero;
            }
        }
    }

    private void InstanceObjectPool()
    {
        for (int i = 0; i < 3; i++)
        {
            List<TemplateLevelController> tempList = new List<TemplateLevelController>();
            for (int j = 0; j < LevelPerPage; j++)
            {
                var obj = Instantiate(TemplateLevel) as GameObject;
                obj.transform.SetParent(Pages[i].transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.name = "i";
                obj.SetActive(true);
                var controller = obj.GetComponent<TemplateLevelController>();
                tempList.Add(controller);
            }
            PoolListController.Add(tempList);
        }
    }

    public void ShowLevel()
    {
        this.transform.localScale = new Vector3(SceneManager.instance.Ratio, 1f, 1f);
        Level.alpha = 1;
        Level.blocksRaycasts = true;
        this.gameObject.transform.localPosition = Vector2.zero;
        ResetUi();
        IsShowScreen = true;
    }

    void ResetUi()
    {
        InitLevel();
        StarText.text = string.Format("{0} / {1}", SceneManager.instance.TotalStar().ToString()
            , SceneManager.instance.MaxLevel * 3);
    }
    public void HideLevel()
    {
        Level.alpha = 0;
        Level.blocksRaycasts = false;
        this.gameObject.transform.localPosition = new Vector2(10000, 10000);
        IsShowScreen = false;
    }

    public void OnBackClick()
    {
        HideLevel();
        SceneManager.instance.ModeController.ShowMode();
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

    public void OnEndDrag()
    {
        if(ScrollView.localPosition.x < -50f && currentPage < maxPage)
        {
            ScrollRect.enabled = false;
            LeanTween.moveLocalX(ScrollView.gameObject, -720f, 0.2f)
                .setOnComplete(OnMoveLeftComplete);
        }
        else if(ScrollView.localPosition.x > 50f && currentPage > 1)
        {
            ScrollRect.enabled = false;
            LeanTween.moveLocalX(ScrollView.gameObject, 720f, 0.2f)
                                .setOnComplete(OnMoveRightComplete);
        }
    }

    private void OnMoveRightComplete()
    {
        ScrollView.localPosition = new Vector2(0,0);
        ScrollRect.enabled = true;
        MoveScroll(false);
    }

    private void OnMoveLeftComplete()
    {
        ScrollView.localPosition = new Vector2(0, 0);
        ScrollRect.enabled = true;
        MoveScroll(true);
    }

    private void MoveScroll(bool isRight)
    {
        if (isRight)
        {
            TemplatePosList[currentPage - 1].sprite = NormalPos;
            currentPage++;
            TemplatePosList[currentPage - 1].sprite = CurrentPos;

            for (int i = 0; i < Pages.Count;i++)
            {
                Pages[i].localPosition = new Vector2(Pages[i].localPosition.x - 720f, 0);
                if (Pages[i].localPosition.x < -721f)
                {
                    Pages[i].localPosition = new Vector2(720f, 0);
                    if (currentPage + 1 <= maxPage)
                    {
                        SetDataPerPage(i, currentPage + 1);
                    }
                    else
                    {
                        CanvasPage[i].alpha = 0;
                    }
                }
            }

            currentPoolPage++;
            if(currentPoolPage > 2)
            {
                currentPoolPage = 0;
            }
        }
        else
        {
            TemplatePosList[currentPage - 1].sprite = NormalPos;
            currentPage--;
            TemplatePosList[currentPage - 1].sprite = CurrentPos;

            for (int i = 0; i < Pages.Count; i++)
            {
                Pages[i].localPosition = new Vector2(Pages[i].localPosition.x + 720f, 0);
                if (Pages[i].localPosition.x > 721f)
                {
                    Pages[i].localPosition = new Vector2(-720f, 0);
                    if (currentPage - 1 >= 1)
                    {
                        SetDataPerPage(i, currentPage - 1);
                    }
                    else
                    {
                        CanvasPage[i].alpha = 0;
                    }
                }
            }

            currentPoolPage--;
            if (currentPoolPage < 0)
            {
                currentPoolPage = 2;
            }
        }
    }
}
