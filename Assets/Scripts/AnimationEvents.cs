using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioClip clipToPlay;
    public float clipVolume = 1f;
    public GameObject objectToEnable;

    public void PlayClip()
    {
        GetComponent<AudioSource>().PlayOneShot(clipToPlay, clipVolume);
    }

    public void EnableGameObject()
    {
        objectToEnable.SetActive(true);
    }

}
