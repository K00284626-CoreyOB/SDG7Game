using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeTileDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string tileValue;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalAnchoredPosition;
    private Canvas rootCanvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalAnchoredPosition = rectTransform.anchoredPosition;

        transform.SetParent(rootCanvas.transform, true);

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / rootCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        if (transform.parent == rootCanvas.transform)
        {
            ReturnToOriginalPosition();
        }
    }

    public void SnapToSlot(CodeDropSlot slot)
    {
        transform.SetParent(slot.transform, false);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent, false);
        rectTransform.anchoredPosition = originalAnchoredPosition;
    }
}