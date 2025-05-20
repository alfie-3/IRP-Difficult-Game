using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformLineRenderer : MonoBehaviour
{
    [SerializeField] MovingPlatform movingPlatform;
    [SerializeField] LineRenderer lineRenderer;

    private void Start()
    {
        if (movingPlatform == null) return;
        if (lineRenderer == null) return;

        for (int i = 0; movingPlatform.Waypoints.Length > i; i++)
        {
            lineRenderer.SetPosition(i, movingPlatform.Waypoints[i].position);
        }
    }
}
