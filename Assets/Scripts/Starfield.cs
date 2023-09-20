using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    public int maxStars = 100;
    public float starSize = 0.1f;
    public float starSizeRange = 0.5f;
    public float fieldWidth = 20f;
    public float fieldHeight = 25f;
    public bool colorize = false;
    public float moveSpeed = .5f;

    public float xOffset;
    public float yOffset;

    public Transform  bgCamera;

    private ParticleSystem particles;
    private ParticleSystem.Particle[] stars;

    private void Awake()
    {
        bgCamera = bgCamera.transform;
        stars = new ParticleSystem.Particle[maxStars];
        particles = GetComponent<ParticleSystem>();

        xOffset = fieldWidth * .5f;
        yOffset = fieldHeight * .5f;

        for (int i = 0; i < maxStars; i++)
        {
            float randSize = Random.Range(starSizeRange, starSizeRange + 1f);
            float scaledColor = (true == colorize) ? randSize - starSizeRange : 1f;

            stars[i].position = GetRandomInRectangle(fieldWidth, fieldHeight) + transform.position;
            stars[i].startSize = starSize * randSize;
            stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
        }
        particles.SetParticles(stars, stars.Length);
    }

    Vector3 GetRandomInRectangle(float width,float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }

    //movement
    private void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);

        for(int i = 0; i < maxStars; i++)
        {
            Vector3 pos = stars[i].position + transform.position;
            if(pos.y < (bgCamera.position.y - yOffset))
            {
                pos.y += fieldHeight;
            }
            stars[i].position = pos - transform.position;
        }
        particles.SetParticles(stars, stars.Length);
    }
}
