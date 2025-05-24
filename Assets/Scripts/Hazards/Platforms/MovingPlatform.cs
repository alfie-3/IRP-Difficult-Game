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
    [SerializeField] bool waitForPlayer;
    [SerializeField] float pauseDelay = 1;
    int currentTargetWaypoint;
    bool running;
    bool reverse;

    float pauseTimer;

    private void Start()
    {
        pauseTimer = pauseDelay;

        if (!waitForPlayer)
            running = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && waitForPlayer && !running)
        {
            currentTargetWaypoint = 1;
            running = true;
        }
    }

    private void FixedUpdate()
    {
        if (!running) return;

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
            {
                if (waitForPlayer)
                    running = false;

                reverse = false;
            }

            if (reverse)
                currentTargetWaypoint--;
            else
                currentTargetWaypoint++;


            pauseTimer = pauseDelay;
        }

        rb.MovePosition(Vector3.MoveTowards(transform.position, Waypoints[currentTargetWaypoint].position, Time.fixedDeltaTime * speed));
    }
}