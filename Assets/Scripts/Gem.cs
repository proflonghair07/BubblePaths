using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Transform playerFollowPoint;
    private bool isActivated = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isActivated)
        {
            // Use Rigidbody2D's MovePosition to move the Gem towards the playerFollowPoint
            rb.MovePosition(Vector2.MoveTowards(rb.position, playerFollowPoint.position, 8f * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isActivated = true;
        }
    }
}
