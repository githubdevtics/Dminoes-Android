using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every "drag and drop" item must contain this script
/// </summary>

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public DragAndDropItem draggedItem;                                      // Item that is dragged now
    static public GameObject icon;                                                  // Icon of dragged item
    static public DominoController sourceCell;                                      // From this cell dragged item is
    static public int IsSlide = -1;

    public delegate void DragEvent(DragAndDropItem item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event
    static public event DragEvent OnItemDragEndEvent;                               // Drag end event

    /// <summary>
    /// This item is dragged
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (SceneManager.instance.PlayGameController.isBlock)
            return;

        sourceCell = GetComponent<DominoController>();                       // Remember source cell
        if (sourceCell == null || !sourceCell.isMine)
            return;
        draggedItem = this;                                                         // Set as dragged item
        if (sourceCell.IsExtra || sourceCell.IsChecked)
            return;
        icon = SceneManager.instance.PlayGameController.Icon;
        icon.SetActive(true);
        Image image = icon.GetComponent<Image>();
        image.sprite = GetComponentInChildren<Image>().sprite;
        image.raycastTarget = false;                                                // Disable icon's raycast for correct drop handling
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions
        var childRect = transform.GetChild(0).GetComponentInChildren<RectTransform>().sizeDelta;
        iconRect.sizeDelta = new Vector2(childRect.x , childRect.y);
        iconRect.localEulerAngles = new Vector3(0, 0, 90);
        Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas
        if (canvas != null)
        {
            // Display on top of all GUI (in parent canvas)
            icon.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
            icon.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
            icon.transform.localScale = Vector3.one;
        }
        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
    }

    /// <summary>
    /// Every frame on this item drag
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        if (icon != null)
        {
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            icon.transform.position = v3;                          // Item's icon follows to cursor
        }
    }

    /// <summary>
    /// This item is dropped
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (icon != null)
        {
            icon.SetActive(false);                                                          // Destroy icon on item drop
        }
        MakeVisible(true);                                                          // Make item visible in cell

        //draggedItem.transform.position = icon.transform.position; // k added

        if (OnItemDragEndEvent != null)
        {
            OnItemDragEndEvent(this);                                               // Notify all cells about item drag end
        }
        draggedItem = null;
        icon = null;
        sourceCell = null;
    }

    /// <summary>
    /// Enable item's raycast
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeRaycast(bool condition)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            //image.raycastTarget = condition;
        }
    }

    /// <summary>
    /// Enable item's visibility
    /// </summary>
    /// <param name="condition"> true - enable, false - disable </param>
    public void MakeVisible(bool condition)
    {
        GetComponentInChildren<Image>().enabled = condition;
    }
}
