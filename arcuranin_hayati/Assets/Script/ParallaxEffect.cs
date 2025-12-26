using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxMultiplier; // 0.1 = uzak, 0.5 = yakýn

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(
            deltaMovement.x * parallaxMultiplier,
            deltaMovement.y * parallaxMultiplier,
            0
        );

        lastCameraPosition = cameraTransform.position;
    }
}
