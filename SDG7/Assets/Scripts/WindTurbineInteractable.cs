using TMPro;
using UnityEngine;

public class WindTurbineInteractable : MonoBehaviour
{
    public WireMinigameManager wireManager;

    public Transform bladeTransform;
    public float spinSpeed = 180f;

    public bool isRepaired = false;

    public PlayerController player;

    public TextMeshProUGUI solarIfDoneText;

    public UIPanelSwitcher panelSwitcher;

    private void OnMouseDown()
    {
        if (!isRepaired && wireManager != null && !player.PanelStatus())
        {
            Debug.Log("Minigame started");
            wireManager.OpenMinigame(this);
            player.OpenPanel();
            solarIfDoneText.gameObject.SetActive(false);
        }
    }

    public void SetRepaired()
    {
        isRepaired = true;

        if (this.gameObject.CompareTag("house"))
        {
            panelSwitcher.FixEquiptment();
        }
    }

    private void Update()
    {
        if (isRepaired && bladeTransform != null)
        {
            bladeTransform.Rotate(0f, 0f, -spinSpeed * Time.deltaTime);
        }
    }
}