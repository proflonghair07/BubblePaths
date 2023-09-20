using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCheck : MonoBehaviour
{
    public GameObject hasCollectableCheckpoint;
    public GameObject noCollectableCheckpoint;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(player.GetComponent<PlayerController>().hasCollectable == true)
        {
            hasCollectableCheckpoint.GetComponent<BoxCollider2D>().enabled = true;
            noCollectableCheckpoint.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (player.GetComponent<PlayerController>().hasCollectable == false)
        {
            hasCollectableCheckpoint.GetComponent<BoxCollider2D>().enabled = false;
            noCollectableCheckpoint.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
