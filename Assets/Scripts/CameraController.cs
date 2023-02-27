using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // the target gameobject to follow
    public float distance = 5.0f; // the distance from the target
    public float height = 2.0f; // the height offset from the target
    public float smoothSpeed = 0.1f; // the speed at which the camera moves

    private Vector3 offset; // the offset vector between the camera and the target

    void Start()
    {
        // calculate the offset vector between the camera and the target
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // calculate the desired position of the camera
        Vector3 targetPosition = target.position + offset + Vector3.up * height - target.forward * distance;

        // smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // make the camera look at the target
        transform.LookAt(target.position + Vector3.up * height);
    }
}
