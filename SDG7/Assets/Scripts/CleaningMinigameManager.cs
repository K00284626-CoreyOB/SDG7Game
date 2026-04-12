using TMPro;
using UnityEngine;

public class CleaningMinigameManager : MonoBehaviour
{
    public CleaningMinigameUI cleaningUI;

    private SolarPanelInteractable currentPanel;

    public TextMeshProUGUI solarDoneText;

    public void OpenMinigame(SolarPanelInteractable panel)
    {
        currentPanel = panel;
        cleaningUI.gameObject.SetActive(true);
        cleaningUI.Begin(this);
    }

    public void CompleteMinigame()
    {
        if (currentPanel != null)
        {
            currentPanel.SetClean();
        }

        cleaningUI.gameObject.SetActive(false);
        currentPanel = null;
        solarDoneText.gameObject.SetActive(true);
    }

    public void CancelMinigame()
    {
        cleaningUI.gameObject.SetActive(false);
        currentPanel = null;
    }
}