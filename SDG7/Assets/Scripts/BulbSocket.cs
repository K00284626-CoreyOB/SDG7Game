using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulbSocket : MonoBehaviour, IDropHandler
{
    public BulbDrag currentBulb;
    public StreetLampMinigameUI minigameUI;
    public TextMeshProUGUI feedbackText;

    public void OnDrop(PointerEventData eventData)
    {
        BulbDrag droppedBulb = eventData.pointerDrag.GetComponent<BulbDrag>();

        if (droppedBulb == null)
            return;

        if (droppedBulb.bulbType == "old")
        {
            currentBulb = droppedBulb;
            droppedBulb.SnapToSocket(this);

            if (feedbackText != null)
            {
                feedbackText.text = "That's the old bulb.";
            }

            return;
        }

        if (droppedBulb.bulbType == "eco")
        {
            if (currentBulb != null && currentBulb.bulbType == "old")
            {
                if (feedbackText != null)
                {
                    feedbackText.text = "Remove the old bulb first.";
                }

                droppedBulb.ReturnToHome();
                return;
            }

            currentBulb = droppedBulb;
            droppedBulb.SnapToSocket(this);

            if (feedbackText != null)
            {
                feedbackText.text = "Eco bulb installed!";
            }

            if (minigameUI != null)
            {
                minigameUI.CheckCompletion();
            }
        }
    }
}