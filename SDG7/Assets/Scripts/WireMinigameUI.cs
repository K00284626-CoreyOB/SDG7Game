using UnityEngine;
using System.Collections.Generic;

public class WireMinigameUI : MonoBehaviour
{
    public List<WirePointButton> leftPoints;
    public List<WirePointButton> rightPoints;

    private WireMinigameManager manager;
    private WirePointButton selectedLeft;
    private int completedConnections = 0;

    public Transform rightPointsContainer;

    public void Begin(WireMinigameManager newManager)
    {
        manager = newManager;
        selectedLeft = null;
        completedConnections = 0;

        ResetPoints();
        ShuffleRightPoints();
        RepositionRightPoints();
    }

    void ResetPoints()
    {
        foreach (var p in leftPoints)
            p.SetConnected(false);

        foreach (var p in rightPoints)
            p.SetConnected(false);
    }

    void ShuffleRightPoints()
    {
        for (int i = 0; i < rightPoints.Count; i++)
        {
            int randomIndex = Random.Range(i, rightPoints.Count);
            var temp = rightPoints[i];
            rightPoints[i] = rightPoints[randomIndex];
            rightPoints[randomIndex] = temp;
        }
    }

    void RepositionRightPoints()
    {
        for (int i = 0; i < rightPoints.Count; i++)
        {
            rightPoints[i].transform.SetSiblingIndex(i);
        }
    }

    public void SelectPoint(WirePointButton point)
    {
        if (point.isConnected)
            return;

        if (point.isLeftSide)
        {
            selectedLeft = point;
            return;
        }

        if (selectedLeft == null)
            return;

        if (selectedLeft.wireID == point.wireID)
        {
            selectedLeft.SetConnected(true);
            point.SetConnected(true);
            completedConnections++;
            selectedLeft = null;

            if (completedConnections >= leftPoints.Count)
            {
                manager.CompleteMinigame();
            }
        }
        else
        {
            selectedLeft = null;
        }
    }
}