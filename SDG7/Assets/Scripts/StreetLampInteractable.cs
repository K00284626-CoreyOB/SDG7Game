using TMPro;
using UnityEngine;

public class StreetLampInteractable : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite brokenLampSprite;
    public Sprite fixedLampSprite;

    public StreetLampMinigameManager minigameManager;
    public UIPanelSwitcher panelSwitcher;

    public bool isFixed = false;

    public TextMeshProUGUI textToRemove;

    void Start()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isFixed ? fixedLampSprite : brokenLampSprite;
        }
    }

    private void OnMouseDown()
    {
        if (isFixed)
            return;

        if (minigameManager != null)
        {
            minigameManager.OpenMinigame(this);
            textToRemove.gameObject.SetActive(false);
        }
    }

    public void SetFixed()
    {
        isFixed = true;

        if (this.gameObject.CompareTag("firstLamp"))
        {
            panelSwitcher.OpenEquiptmentText();
        }

        if (this.gameObject.CompareTag("house"))
        {
            panelSwitcher.FixEquiptment();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = fixedLampSprite;
        }
    }
}