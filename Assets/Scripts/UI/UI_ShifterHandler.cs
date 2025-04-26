using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ShifterHandler : MonoBehaviour
{
    public UI_GearshiftMovementConnector startConnector;
    public UI_GearshiftMovementConnector endConnector;

    [SerializeField] CarInterface carInterface;

    [SerializeField] float splitDistance = 100f;

    public Vector3 GetClosestLinePoint(Vector3 point)
    {
        float closestDistance = float.PositiveInfinity;
        Vector3 closestPoint = Vector3.zero;

        float currentDistance;
        Vector3 currentPoint;

        UI_GearshiftMovementConnector connectorSplit;

        if (Vector3.Distance(point, startConnector.position) < splitDistance)
        {
            connectorSplit = startConnector;
        }
        else if (Vector3.Distance(point, endConnector.position) < splitDistance)
        {
            connectorSplit = endConnector;
        }
        else
        {
            return PointOnLine(startConnector.position, endConnector.position, point);
        }

        for (int i = 0; i < connectorSplit.connectedPoints.Length; i++)
        {
            currentPoint = PointOnLine(connectorSplit.position, connectorSplit.connectedPoints[i].position, point);

            if (currentPoint == Vector3.zero)
            {
                continue;
            }

            currentDistance = Vector3.Distance(point, currentPoint);

            if (currentDistance < closestDistance)
            {
                startConnector = connectorSplit;
                endConnector = connectorSplit.connectedPoints[i];

                closestDistance = currentDistance;
                closestPoint = currentPoint;
            }

        }


        Debug.DrawLine(closestPoint, point, Color.green);

        //carInterface.ChangeGear(GearshiftLine[currentLineIndex].Gear);
        return closestPoint;
    }

    public void ChangeGear(int gear)
    {
        carInterface.ChangeGear(gear);
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
}

[System.Serializable]
public struct GearshiftLine
{
    [field: SerializeField] public UI_GearshiftMovementConnector Start { get; private set; }
    [field: SerializeField] public UI_GearshiftMovementConnector End { get; private set; }
    [field: Space]
    [field: SerializeField] public int Gear {  get; private set; }
}