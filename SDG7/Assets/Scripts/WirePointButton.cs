using UnityEngine;
using UnityEngine.UI;

public class WirePointButton : MonoBehaviour
{
    public int wireID;
    public bool isLeftSide;
    public bool isConnected = false;

    public WireMinigameUI wireUI;
    public Image image;
    public Color normalColor = Color.white;
    public Color connectedColor = Color.green;

    public void ClickPoint()
    {
        if (wireUI != null)
        {
            wireUI.SelectPoint(this);
        }
    }

    public void SetConnected(bool value)
    {
        isConnected = value;

        if (image != null)
        {
            image.color = value ? connectedColor : normalColor;
        }
    }
}