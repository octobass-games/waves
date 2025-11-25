using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxFactor = 0.1f;

    private Transform cam;
    private Vector3 previousCamPos;
    public float activationDistance = 15;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(cam.position, transform.position);

        if (distance <= activationDistance)
        {
            Vector3 deltaMovement = cam.position - previousCamPos;

            transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);
        }

        previousCamPos = cam.position;
    }
}
