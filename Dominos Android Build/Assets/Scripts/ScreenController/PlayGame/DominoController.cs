using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DominoController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public PlayGameController MainController;
    public Image Chess;

    public int Up = 0;
    public int Down = 0;
    public bool IsVertical;
    public bool IsChecked;
    public int ConnectPoint = 0;
    public bool IsExtra;
    private Sprite temp;
    public int Index;
    public bool IsRemain;
    public bool isMine = false;

    private Button button;
    private DragAndDropItem dropItem;

    public RectTransform rectTransform;

    void Start()
    {
        button = GetComponent<Button>();
        dropItem = GetComponent<DragAndDropItem>();

        rectTransform = GetComponent<RectTransform>();
    }

	public void InitDomino(int index, int up, int down)
    {
        Index = index;
        temp = MainController.DominosSprites[index];
        transform.SetParent(MainController.DominoesHolder.transform);
        Chess.sprite = MainController.White;
        Up = up;
        Down = down;
        Chess.SetNativeSize();
        Chess.transform.eulerAngles = new Vector3(0, 0, 0f);
        IsChecked = false;
        IsExtra = false;
        IsRemain = true;
        Chess.raycastTarget = true;
        isMine = false;
    }

    public void ChangeBg()
    {
        temp = MainController.DominosSprites[Index];
    }

    public void ShowDominos()
    {
        Chess.sprite = temp;
        Chess.rectTransform.sizeDelta = new Vector2(160, 80);
        Chess.transform.eulerAngles = new Vector3(0, 0, 90f);
    }

    public void ChangeUI()
    {
        Chess.sprite = temp;
    }

    public Sprite GetUI()
    {
        return temp;
    }

    public void Rotate(float zIndex)
    {
        Chess.transform.eulerAngles = new Vector3(0, 0, zIndex);
    }

    public void OnThisDominoClick()
    {
        MainController.OnDominoClick(this, offSound);
        offSound = false;
    }

    public bool CheckDominoClick()
    {
        return button.interactable;
    }

    void OnEnable()
    {
        DragAndDropItem.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        DragAndDropItem.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
        // button.enabled = false;
    }

    void OnDisable()
    {
        DragAndDropItem.OnItemDragStartEvent -= OnAnyItemDragStart;
        DragAndDropItem.OnItemDragEndEvent -= OnAnyItemDragEnd;
    }

    bool offSound = false;
    private void OnAnyItemDragStart(DragAndDropItem item)
    {
        if (!isMine || IsExtra || IsChecked)
            return;

        if (SceneManager.instance.PlayGameController.isBlock
        || SceneManager.instance.PlayGameController.FirstPlayer != 0)
            return;

        DragAndDropItem myItem = item.GetComponent<DragAndDropItem>(); // Get item from current cell
        if (myItem != null && item.name == DragAndDropItem.sourceCell.name)
        {
            if (!DragAndDropItem.sourceCell.isMine || DragAndDropItem.sourceCell.IsExtra || DragAndDropItem.sourceCell.IsChecked)
                return;

            IsDragable = true;
            myItem.MakeRaycast(false);
            if (!DragAndDropItem.sourceCell.CheckDominoClick())
            {
                DragAndDropItem.icon.SetActive(false);
            }
            else
            {
                DragAndDropItem.sourceCell.Chess.color = new Color(1, 1, 1, 0);
                if (MainController.CurrentTurn == 0)
                {
                    DragAndDropItem.sourceCell.IsDragable = false;
                    DragAndDropItem.icon.SetActive(false);
                }
                offSound = true;
                DragAndDropItem.sourceCell.OnThisDominoClick();
                DragAndDropItem.IsSlide = SceneManager.instance.PlayGameController.GetHintStatus();
                if (DragAndDropItem.IsSlide >= 0)
                {
                    MainController.StartChecking = true;
                    MainController.FindAllHint();
                }
            }
            SceneManager.instance.PlayGameController.ClickingObj.transform.position = new Vector3(10000, 10000, 0);
        }
    }

    private void OnAnyItemDragEnd(DragAndDropItem item)
    {
        if (MainController.isBlock || !isMine || IsExtra)
            return;

        DragAndDropItem myItem = dropItem; // Get item from current cell
        if (myItem != null && DragAndDropItem.sourceCell != null 
            && item.name == DragAndDropItem.sourceCell.name)
        {
            if (!DragAndDropItem.sourceCell.isMine || DragAndDropItem.sourceCell.IsExtra || DragAndDropItem.sourceCell.IsChecked)
            {
                return;
            }
            //else
            //{
                
            //}
            myItem.MakeRaycast(true);                                       // Enable item's raycast
            if (DragAndDropItem.sourceCell != null && !DragAndDropItem.sourceCell.IsChecked)
            {
                DragAndDropItem.sourceCell.Chess.color = new Color(1, 1, 1, 1);
                SceneManager.instance.PlayGameController.ClickingObj.transform.position = DragAndDropItem.sourceCell.transform.position;
            }
            
        }
    }

    public bool IsDragable;
    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDragable)
            return;

        if (!isMine || IsExtra || IsChecked)
            return;

        //DragAndDropItem.IsSlide = SceneManager.instance.PlayGameController.GetHintStatus();
        //if (DragAndDropItem.icon && DragAndDropItem.icon.activeSelf && DragAndDropItem.IsSlide >= 0)
        //{
        //    if (DragAndDropItem.icon.transform.position.y > DragAndDropItem.sourceCell.transform.position.y)
        //    {
        //        DragAndDropItem.sourceCell.IsDragable = false;
        //        DragAndDropItem.icon.SetActive(false);
        //        MainController.StartChecking = false;
        //        MainController.OnThisHintClick(MainController.Hint[DragAndDropItem.IsSlide].gameObject);
        //    }
        //}
    }

    public void MakeRaycast(bool condition)
    {
        Chess.raycastTarget = condition;
    }

    private Vector2 clickPoint = new Vector2();
    public void OnPointerUp(PointerEventData data)
    {
        if (!isMine || IsExtra || IsChecked)
            return;
        if (clickPoint.x <= data.position.x + 5f && clickPoint.x >= data.position.x - 5f
            && clickPoint.y <= data.position.y + 5f && clickPoint.y >= data.position.y - 5f)
        {
            offSound = true;
            MainController.OnDominoClick(this);
            offSound = false;
        }
        else
        {
            if (!IsDragable)
                return;

            DragAndDropItem.IsSlide = SceneManager.instance.PlayGameController.GetHintStatus();
            if (DragAndDropItem.icon && DragAndDropItem.icon.activeSelf && DragAndDropItem.IsSlide >= 0)
            {
                if (DragAndDropItem.icon.transform.position.y > DragAndDropItem.sourceCell.transform.position.y)
                {
                    DragAndDropItem.sourceCell.IsDragable = false;
                    DragAndDropItem.icon.SetActive(false);
                    MainController.OnThisHintClick(MainController.Hint[DragAndDropItem.IsSlide].gameObject);


                    DragAndDropItem.draggedItem.transform.position = DragAndDropItem.icon.transform.position; //khalil
                }
            }
            MainController.StartChecking = false;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        clickPoint = data.position;
    }

    public void OnDropAfterPause()
    {
        if (DragAndDropItem.icon != null)
        {
            if (DragAndDropItem.icon.activeSelf == true)                    // If icon inactive do not need to drop item in cell
            {
                DragAndDropItem item = DragAndDropItem.draggedItem;
                DominoController sourceCell = DragAndDropItem.sourceCell;
                if (item != null)
                {
                    DragAndDropItem.icon.SetActive(false);
                    MainController.CurrentDomino.Chess.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}
