using UnityEngine;

public class ParallaxTilemap : MonoBehaviour
{
    public float parallaxSpeedX = 0.5f; // Speed of parallax movement in the X-axis
    public float parallaxSpeedY = 0.5f; // Speed of parallax movement in the Y-axis

    private Vector3 previousCameraPosition;

    private void Start()
    {
        previousCameraPosition = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraMovement = Camera.main.transform.position - previousCameraPosition;
        Vector3 parallaxMovement = new Vector3(cameraMovement.x * parallaxSpeedX, cameraMovement.y * parallaxSpeedY, 0f);
        transform.position += parallaxMovement;

        previousCameraPosition = Camera.main.transform.position;
    }
}
