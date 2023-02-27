using UnityEngine;

public class SmoothGyro : MonoBehaviour
{
    private Quaternion baseOrientation = Quaternion.identity;
    private Quaternion baseAttitude = Quaternion.identity;
    private Gyroscope gyro;

    // Variables for controlling the smoothness of the movement
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Initialize gyroscope
        gyro = Input.gyro;
        gyro.enabled = true;

        // Set base orientation and attitude to the device's initial orientation
        baseOrientation = Quaternion.Euler(90, 0, 0) * Quaternion.identity;
        baseAttitude = Input.gyro.attitude * Quaternion.Inverse(baseOrientation);
    }

    void Update()
    {
        // Get gyroscope data
        Quaternion gyroOrientation = gyro.attitude * baseAttitude;
        Quaternion relativeOrientation = baseOrientation * Quaternion.Inverse(gyroOrientation);

        // Convert gyroscope data to Euler angles and apply to game object's rotation
        Vector3 euler = relativeOrientation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, euler.y, 0);

        // Smoothly move game object forward in the direction of its current rotation
        Vector3 targetPosition = transform.position + transform.forward;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
