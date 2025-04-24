using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ShifterHandler : MonoBehaviour
{
    [SerializeField] GearstickAnchor[] GearstickAnchors;

    [SerializeField] int currentAnchorIndex;
    GearstickAnchor currentAnchor => GearstickAnchors[currentAnchorIndex];

    public Vector3 GetClosestLinePoint(Vector3 point)
    {
        float closestDistance = float.PositiveInfinity;
        Vector3 closestPoint = Vector3.zero;

        int selectedAnchorIndex = -1;
        float currentDistance;
        Vector3 currentPoint;

        for (int i = 0; i < currentAnchor.ConnectedAnchors.Length; i++)
        {
            GearstickAnchor anchor = GearstickAnchors[currentAnchor.ConnectedAnchors[i]];

            currentPoint = PointOnLine(anchor.Start.position, anchor.End.position, point);

            if (currentPoint == Vector3.zero)
            {
                continue;
            }

            currentDistance = Vector3.Distance(point, currentPoint);

            if (currentDistance < closestDistance)
            {
                selectedAnchorIndex = currentAnchor.ConnectedAnchors[i];
                closestDistance = currentDistance;
                closestPoint = currentPoint;
            }

            Debug.DrawLine(currentPoint, point, Color.red);
        }


        if (selectedAnchorIndex == -1) return Vector3.zero;

        Debug.DrawLine(closestPoint, point, Color.green);

        currentAnchorIndex = selectedAnchorIndex;
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
        for (int i = 0; i < GearstickAnchors.Length; i++)
        {
            if (GearstickAnchors[i].Start == null || GearstickAnchors[i].End == null) continue;

            if (currentAnchor.ConnectedAnchors.Contains(i))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(GearstickAnchors[i].Start.position, GearstickAnchors[i].End.position);
        }
    }
}

[System.Serializable]
public struct GearstickAnchor
{
    [field: SerializeField] public RectTransform Start { get; private set; }
    [field: SerializeField] public RectTransform End { get; private set; }
    [field: SerializeField] public int[] ConnectedAnchors { get; private set; }
}