using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SolarIfStatemenUI : MonoBehaviour
{
    public CodeDropSlot slotIf;
    public CodeDropSlot slotCondition;
    public CodeDropSlot slotAction;

    public TextMeshProUGUI feedbackText;
    public Button exitButton;
    public Button learnButton;

    public void CheckAnswer()
    {
        bool correct =
            slotIf.GetCurrentValue() == "if" &&
            slotCondition.GetCurrentValue() == "panelIsDirty" &&
            slotAction.GetCurrentValue() == "CallEngineer();";

        if (correct)
        {
            feedbackText.text = "Correct! The system calls the engineer if the panel gets dirty.";
            exitButton.gameObject.SetActive(true);
            learnButton.gameObject.SetActive(true);
        }
        else
        {
            feedbackText.text = "Not quite. Build: if (panelIsDirty) { CallEngineer(); }";
        }
    }
}