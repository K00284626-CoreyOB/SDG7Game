using TMPro;
using UnityEngine;

public class SolarPanelInteractable : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite dirtySprite;
    public Sprite cleanSprite;

    public bool isClean = false;

    public GameObject minigamePanel;
    public GameObject ifStatementPanel;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI solarDoneText;
    public TextMeshProUGUI solarIfDoneText;
    public CleaningMinigameManager cleaningManager;
    public UIPanelSwitcher panelSwitcher;

    private void OnMouseDown()
    {
        if (!isClean)
        {
            minigamePanel.SetActive(true);
            startText.enabled = false;
            cleaningManager.OpenMinigame(this);
        }
        else if(isClean && this.gameObject.CompareTag("house"))
        {
            return;
        }
        else
        {
            ifStatementPanel.SetActive(true);
            solarDoneText.enabled = false;
        }
    }

    public void CloseIfGame()
    {
        ifStatementPanel.SetActive(false);
        solarIfDoneText.gameObject.SetActive(true);
    }

    public void SetClean()
    {
        isClean = true;
        spriteRenderer.sprite = cleanSprite;
        if (this.gameObject.CompareTag("house"))
        {
            panelSwitcher.FixEquiptment();
        }
    }

    public void SetDirty()
    {
        isClean = false;
        spriteRenderer.sprite = dirtySprite;
    }

    private void Start()
    {
        spriteRenderer.sprite = isClean ? cleanSprite : dirtySprite;
    }
}