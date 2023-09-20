using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;        // The starting point empty GameObject
    public Transform endPoint;          // The ending point empty GameObject
    public float moveSpeed = 2f;        // The speed at which the platform moves

    private Vector3 currentTarget;      // The current target position
    private bool movingToStartPoint;    // Flag indicating whether the platform is moving towards the starting point

    private void Start()
    {
        // Set the initial target as the starting point
        currentTarget = startPoint.position;
        movingToStartPoint = false;
        transform.position = startPoint.position;
    }

    private void FixedUpdate()
    {
        // Move the platform towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        // Check if the platform has reached the current target
        if (transform.position == currentTarget)
        {
            // If the platform reaches the starting point, set the next target as the ending point
            if (movingToStartPoint)
            {
                currentTarget = endPoint.position;
            }
            // If the platform reaches the ending point, set the next target as the starting point
            else
            {
                currentTarget = startPoint.position;
            }

            // Toggle the moving direction flag
            movingToStartPoint = !movingToStartPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make the colliding object a child of the platform
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove the colliding object as a child of the platform
        collision.collider.transform.SetParent(null);
    }
}
