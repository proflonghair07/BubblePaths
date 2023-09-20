using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    public GameObject player;
    public GameObject gm;
    public AudioClip latchOpen;
    // Start is called before the first frame update
    public void EnablePlayer()
    {
        player.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(latchOpen, 1f);
    }

    public void EnableGM()
    {
        gm.GetComponent<AudioSource>().enabled = true;
    }
}
