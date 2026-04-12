using UnityEngine;
using UnityEngine.EventSystems;

public class BulbDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string bulbType; // "old" or "eco"

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas rootCanvas;

    private Transform homeParent;
    private Vector2 homeAnchoredPosition;
    private Vector3 homeScale;

    public BulbSocket currentSocket;

    private bool startedFromSocket = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rootCanvas = GetComponentInParent<Canvas>();

        homeParent = transform.parent;
        homeAnchoredPosition = rectTransform.anchoredPosition;
        homeScale = rectTransform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startedFromSocket = currentSocket != null;

        if (currentSocket != null && currentSocket.currentBulb == this)
        {
            currentSocket.currentBulb = null;
            currentSocket = null;
        }

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
            if (bulbType == "old" && startedFromSocket)
            {
                ReturnToHome();
            }
            else if (bulbType == "eco")
            {
                ReturnToHome();
            }
        }

        startedFromSocket = false;
    }

    public void SnapToSocket(BulbSocket socket)
    {
        currentSocket = socket;
        transform.SetParent(socket.transform, false);
        transform.SetAsFirstSibling();
        rectTransform.anchoredPosition = new Vector2(0,-15);
        rectTransform.localScale = Vector3.one;
    }

    public void ReturnToHome()
    {
        currentSocket = null;
        transform.SetParent(homeParent, false);
        rectTransform.anchoredPosition = homeAnchoredPosition;
        rectTransform.localScale = homeScale;
    }
}