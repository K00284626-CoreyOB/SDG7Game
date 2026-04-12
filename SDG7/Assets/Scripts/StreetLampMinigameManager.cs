using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StreetLampMinigameManager : MonoBehaviour
{
    public StreetLampMinigameUI minigameUI;
    public TextMeshProUGUI lampDoneText;

    private StreetLampInteractable currentLamp;

    public void OpenMinigame(StreetLampInteractable lamp)
    {
        currentLamp = lamp;

        if (minigameUI != null)
        {
            minigameUI.gameObject.SetActive(true);
            minigameUI.Begin(this);
        }
    }

    public void CompleteMinigame()
    {
        if (currentLamp != null)
        {
            currentLamp.SetFixed();
        }

        if (lampDoneText != null)
        {
            if(currentLamp.gameObject.CompareTag("house"))
            {
                return;
            }
            else
            {
                lampDoneText.gameObject.SetActive(true);
            }
        }

        currentLamp = null;
    }

    public void CancelMinigame()
    {
        if (minigameUI != null)
        {
            minigameUI.gameObject.SetActive(false);
        }

        currentLamp = null;
    }
}