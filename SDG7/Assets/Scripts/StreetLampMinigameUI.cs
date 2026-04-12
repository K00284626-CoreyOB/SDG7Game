using TMPro;
using UnityEngine;

public class StreetLampMinigameUI : MonoBehaviour
{
    public BulbSocket bulbSocket;
    public BulbDrag oldBulb;
    public BulbDrag ecoBulb;
    public TextMeshProUGUI feedbackText;

    private StreetLampMinigameManager manager;

    public void Begin(StreetLampMinigameManager newManager)
    {
        manager = newManager;
        ResetMinigame();

        if (feedbackText != null)
        {
            feedbackText.text = "Drag out the old bulb, then install the eco bulb.";
        }
    }

    public void ResetMinigame()
    {
        if (oldBulb != null)
        {
            oldBulb.ReturnToHome();
        }

        if (ecoBulb != null)
        {
            ecoBulb.ReturnToHome();
        }

        if (bulbSocket != null)
        {
            bulbSocket.currentBulb = null;

            if (oldBulb != null)
            {
                bulbSocket.currentBulb = oldBulb;
                oldBulb.SnapToSocket(bulbSocket);
            }
        }
    }

    public void CheckCompletion()
    {
        if (bulbSocket != null &&
            bulbSocket.currentBulb != null &&
            bulbSocket.currentBulb.bulbType == "eco")
        {
            if (manager != null)
            {
                manager.CompleteMinigame();
            }
        }
    }
}