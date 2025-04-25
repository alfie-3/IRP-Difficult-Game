using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ShifterHandler : MonoBehaviour
{
    [SerializeField] GearshiftLine[] GearshiftLine;

    [SerializeField] int currentLineIndex;
    public GearshiftLine CurrentLine => GearshiftLine[currentLineIndex];

    [SerializeField] CarInterface carInterface;

    public Vector3 GetClosestLinePoint(Vector3 point)
    {
        float closestDistance = float.PositiveInfinity;
        Vector3 closestPoint = Vector3.zero;

        int selectedAnchorIndex = -1;
        float currentDistance;
        Vector3 currentPoint;

        UI_GearshiftMovementConnector closestMovementConnector;

        if (Vector3.Distance(point, CurrentLine.End.position) > Vector3.Distance(point, CurrentLine.Start.position))
        {
            closestMovementConnector = CurrentLine.Start;
        }
        else
        {
            closestMovementConnector = CurrentLine.End;
        }

        for (int i = 0; i < closestMovementConnector.connectedPoints.Length; i++)
        {
            currentPoint = PointOnLine(closestMovementConnector.position, closestMovementConnector.connectedPoints[i].position, point);

            if (currentPoint == Vector3.zero)
            {
                continue;
            }

            currentDistance = Vector3.Distance(point, currentPoint);

            if (currentDistance < closestDistance)
            {
                //selectedAnchorIndex = CurrentLine.ConnectedAnchors[i];
                closestDistance = currentDistance;
                closestPoint = currentPoint;
            }

            Debug.DrawLine(currentPoint, point, Color.red);
        }


        if (selectedAnchorIndex == -1) return Vector3.zero;

        Debug.DrawLine(closestPoint, point, Color.green);

        currentLineIndex = selectedAnchorIndex;
        carInterface.ChangeGear(GearshiftLine[currentLineIndex].Gear);
        return closestPoint;
    }

    public Vector3 PointOnLine(Vector3 start, Vector3 end, Vector3 pnt, float minDistance = 0.01f)
    {
        var line = (end - start);
        var len = line.magnitude;
        line.Normalize();

        var v = pnt - start;
        var d = Vector3.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);
        return start + line * d;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < GearshiftLine.Length; i++)
        {
            if (GearshiftLine[i].Start == null || GearshiftLine[i].End == null) continue;

            Gizmos.color = Color.green;

            Gizmos.DrawLine(GearshiftLine[i].Start.position, GearshiftLine[i].End.position);
        }
    }
}

[System.Serializable]
public struct GearshiftLine
{
    [field: SerializeField] public UI_GearshiftMovementConnector Start { get; private set; }
    [field: SerializeField] public UI_GearshiftMovementConnector End { get; private set; }
    [field: Space]
    [field: SerializeField] public int Gear {  get; private set; }
}