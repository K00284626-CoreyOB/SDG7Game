using TMPro;
using UnityEngine;

public class UIPanelSwitcher : MonoBehaviour
{
    public GameObject ifStatementPanel;
    public GameObject solarInfoPanel;
    public GameObject StreetLampPanel;
    public GameObject bulbInfoPanel;
    public TextMeshProUGUI solarIfDoneText;
    public TextMeshProUGUI bulbDoneText;
    public GameObject gameDonePanel;
    public GameObject controlsPanel;

    public TextMeshProUGUI equiptmentToFixText;
    public int fixedEquiptment = 0;

    public void OpenSolarInfo()
    {
        if (ifStatementPanel != null)
            ifStatementPanel.SetActive(false);

        if (solarInfoPanel != null)
            solarInfoPanel.SetActive(true);
    }

    public void CloseSolarInfo()
    {
        if (solarInfoPanel != null)
            solarInfoPanel.SetActive(false);

        solarIfDoneText.gameObject.SetActive(true);
    }

    public void OpenBulbInfo()
    {
        if (StreetLampPanel != null)
            StreetLampPanel.SetActive(false);

        if (bulbInfoPanel != null)
            bulbInfoPanel.SetActive(true);
    }

    public void CloseBulbInfo()
    {
        if (bulbInfoPanel != null)
            bulbInfoPanel.SetActive(false);

        bulbDoneText.gameObject.SetActive(true);
    }

    public void OpenEquiptmentText()
    {
        equiptmentToFixText.gameObject.SetActive(true);
        SetEquiptmentText(0);
    }

    public void CloseEquiptmentText()
    {
        equiptmentToFixText.gameObject.SetActive(false);
    }

    public void FixEquiptment()
    {
        fixedEquiptment++;
        SetEquiptmentText(fixedEquiptment);

        if(fixedEquiptment == 4)
        {
            gameDonePanel.gameObject.SetActive(true);
            controlsPanel.gameObject.SetActive(false);
            CloseEquiptmentText();
        }
    }

    public void SetEquiptmentText(int numberFixed)
    {
        equiptmentToFixText.SetText(numberFixed + "/4");
    }
}