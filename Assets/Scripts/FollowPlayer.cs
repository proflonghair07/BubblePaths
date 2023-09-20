using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform.
    public Vector3 offset; // Offset from the player's position.

    void Update()
    {
        // Ensure the empty GameObject follows the player's position without inheriting its rotation.
        transform.position = playerTransform.position + offset;
    }
}
