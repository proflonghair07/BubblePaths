using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Transform[] waypoints;  // An array of waypoints representing the path.
    public float moveSpeed = 5f;   // Speed at which the GameObject moves.
    private int currentWaypoint = 0;

    public GameObject explosion;

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            // Move towards the current waypoint.
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

            // Check if the GameObject has reached the current waypoint.
            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                currentWaypoint++;
            }
        }
        else
        {
            // Loop back to the start of the path.
            currentWaypoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            GetComponent<AnimationHandler>().ChangeAnimationState("Respawn");
        }
    }
}