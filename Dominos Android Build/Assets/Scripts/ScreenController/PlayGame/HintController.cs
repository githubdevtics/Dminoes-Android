using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HintController : MonoBehaviour, IDropHandler
{

    public int Index;
    public Image Hint;

    public void OnDrop(PointerEventData data)
    {

        if (gameObject.activeSelf && DragAndDropItem.icon != null)
        {
            if (DragAndDropItem.icon.activeSelf == true)                    // If icon inactive do not need to drop item in cell
            {
                DragAndDropItem item = DragAndDropItem.draggedItem;
                DominoController sourceCell = DragAndDropItem.sourceCell;
                if (item != null)
                {
                    SceneManager.instance.PlayGameController.OnThisHintClick(gameObject);
                }
            }
        }
    }
}
