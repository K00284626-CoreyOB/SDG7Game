using UnityEngine;

public class WireMinigameManager : MonoBehaviour
{
    public WireMinigameUI wireUI;

    private WindTurbineInteractable currentTurbine;

    public PlayerController player;

    public void OpenMinigame(WindTurbineInteractable turbine)
    {
        currentTurbine = turbine;
        wireUI.gameObject.SetActive(true);
        wireUI.Begin(this);
    }

    public void CompleteMinigame()
    {
        if (currentTurbine != null)
        {
            currentTurbine.SetRepaired();
            player.ClosePanel();
            player.FixTurbine();
        }

        wireUI.gameObject.SetActive(false);
        currentTurbine = null;
    }


    public void CancelMinigame()
    {
        wireUI.gameObject.SetActive(false);
        currentTurbine = null;
    }
}