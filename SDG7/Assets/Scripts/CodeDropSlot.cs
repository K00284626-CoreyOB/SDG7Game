using UnityEngine;
using UnityEngine.EventSystems;

public class CodeDropSlot : MonoBehaviour, IDropHandler
{
    public string expectedValue;
    public CodeTileDrag currentTile;

    public void OnDrop(PointerEventData eventData)
    {
        CodeTileDrag droppedTile = eventData.pointerDrag.GetComponent<CodeTileDrag>();

        if (droppedTile == null)
            return;

        if (currentTile != null)
        {
            currentTile.ReturnToOriginalPosition();
        }

        currentTile = droppedTile;
        droppedTile.SnapToSlot(this);
    }

    public string GetCurrentValue()
    {
        if (currentTile == null)
            return "";

        return currentTile.tileValue;
    }

    public void ClearSlot()
    {
        currentTile = null;
    }
}