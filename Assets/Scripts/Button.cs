using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    public AudioClip click;
    private bool canPlaySound = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<AnimationHandler>().ChangeAnimationState("Open");
            if (canPlaySound)
            {
                GetComponent<AudioSource>().PlayOneShot(click, .35f);
            }
            canPlaySound = false;
            
        }
    }

    public void OpenDoor()
    {
        door.GetComponent<AnimationHandler>().ChangeAnimationState("Open");
    }
}
