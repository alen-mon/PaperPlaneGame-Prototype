using UnityEngine;

public class RotationalGyro : MonoBehaviour
{
    private Gyroscope gyro;
    private Quaternion initialOrientation;

    void Start()
    {
        // Check if device supports gyroscope
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.Log("Device does not support gyroscope");
            return;
        }

        // Get reference to device's gyroscope
        gyro = Input.gyro;

        // Enable gyroscope
        gyro.enabled = true;

        // Set the initial orientation of the device as the reference for rotations
        initialOrientation = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Inverse(gyro.attitude);
    }

    void Update()
    {
        // Check if gyroscope is enabled
        if (!gyro.enabled)
        {
            return;
        }

        // Get the current orientation of the device from the gyroscope
        Quaternion currentOrientation = gyro.attitude;

        // Apply the initial orientation to the current orientation to get the rotation
        Quaternion rotation = initialOrientation * currentOrientation;

        // Extract the euler angles from the rotation
        Vector3 eulerAngles = rotation.eulerAngles;

        // Apply the euler angles to the game object's transform
        transform.rotation = Quaternion.Euler(-eulerAngles.x, -eulerAngles.y, eulerAngles.z);
    }
}
