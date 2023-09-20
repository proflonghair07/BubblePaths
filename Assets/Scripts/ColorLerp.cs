using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float lerpSpeed = 1.0f;

    public float initLerpSpeed = 1.0f;
    public float dangerLerpSpeed = 3.0f;

    private SpriteRenderer spriteRenderer;
    private float lerpTime = 0.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Calculate lerp progress using a sine wave
        float lerpProgress = Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time * lerpSpeed, 1f));

        // Lerp the color of the SpriteRenderer
        spriteRenderer.color = Color.Lerp(startColor, endColor, lerpProgress);
    }
}
