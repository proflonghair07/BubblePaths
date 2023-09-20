using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // You need this namespace to access TextMeshPro components.

public class OxygenCounter : MonoBehaviour
{
    public int startingSeconds = 10; // Set the initial countdown duration in seconds.
    private float timeRemaining;

    public TextMeshProUGUI countdownText;

    public float oxygenBoost = 10f;

    //water stuff
    public bool inWater = false;
    public GameObject splash;

    //warning signal stuff
    public float dangerTime;
    public float extremeDangerTime;

    //audio
    private AudioSource source;
    public AudioClip splashSound;
    public AudioClip bubblePop;

    private bool canSplash = true;

    public AudioSource loopSource;
    



    void Start()
    {
        timeRemaining = startingSeconds;
        UpdateCountdownText();

        source = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (inWater)
        {
            countdownText.enabled = true;
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
                timeRemaining = Mathf.Max(0f, timeRemaining);
                UpdateCountdownText();
            }
            
            else
            {
                countdownText.text = "!";
                this.GetComponent<PlayerController>().KillPlayer();
                this.GetComponent<ColorLerp>().enabled = false;
            }
        }
        else
        {
            countdownText.enabled = false;
        }

        if (timeRemaining < dangerTime)
        {
            loopSource.enabled = true;
            this.GetComponent<ColorLerp>().enabled = true;
        }
        if (timeRemaining < extremeDangerTime)
        {
            this.GetComponent<ColorLerp>().lerpSpeed = 8.0f;
        }
        if (timeRemaining > dangerTime)
        {
            loopSource.enabled = false;
            this.GetComponent<ColorLerp>().enabled = false;
            this.GetComponent<SpriteRenderer>().color = this.GetComponent<ColorLerp>().startColor;
        }

    }

    // Public method to add seconds to the countdown timer.
    public void AddSeconds(int secondsToAdd)
    {
        timeRemaining += secondsToAdd;
        UpdateCountdownText();
    }

    // Helper method to update the TextMeshPro Text element with the current time remaining.
    private void UpdateCountdownText()
    {
        int remainingSeconds = Mathf.FloorToInt(timeRemaining);
        countdownText.text = remainingSeconds.ToString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bubble")
        {
            timeRemaining = oxygenBoost;
            source.PlayOneShot(bubblePop, 1f);
        }

        if(collision.gameObject.tag == "Water")
        {
            inWater = true;
            if (canSplash)
            {
                Instantiate(splash, new Vector3(transform.position.x, transform.position.y + .4f), transform.rotation);
                source.PlayOneShot(splashSound, 1.5f);
                canSplash = false;
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            inWater = false;
            canSplash = true;
            if(timeRemaining < startingSeconds)
            {
                timeRemaining = startingSeconds;
            }
            this.GetComponent<ColorLerp>().enabled = false;
            this.GetComponent<PlayerController>().ReturnToStartColor();
            loopSource.enabled = false;
        }
    }
}