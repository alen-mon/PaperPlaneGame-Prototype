using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllera : MonoBehaviour
{
    public Transform target; // reference to the plane object
    public float smoothTime = 0.3f; // time taken to smoothly follow the plane

    private Vector3 velocity = Vector3.zero; // velocity of the camera

    void LateUpdate()
    {
        // Set the camera's position to the plane's position with an offset
        Vector3 targetPosition = target.position + new Vector3(0, 0, -10);

        // Use SmoothDamp function to smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
