using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [Space]
    public Transform[] Waypoints;
    [SerializeField] float speed = 2;
    [SerializeField] float pauseDelay = 1;
    int currentTargetWaypoint;
    bool reverse;

    float pauseTimer;

    private void Start()
    {
        pauseTimer = pauseDelay;
    }

    private void Update()
    {
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        if (Vector3.Distance(Waypoints[currentTargetWaypoint].position, transform.position) < 0.05)
        {
            if (currentTargetWaypoint >= Waypoints.Length - 1)
                reverse = true;
            else if (currentTargetWaypoint <= 0)
                reverse = false;

            if (reverse)
                currentTargetWaypoint--;
            else
                currentTargetWaypoint++;


            pauseTimer = pauseDelay;
        }

        rb.MovePosition(Vector3.MoveTowards(transform.position, Waypoints[currentTargetWaypoint].position, Time.deltaTime * speed));
    }
}