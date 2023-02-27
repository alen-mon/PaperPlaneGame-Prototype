using UnityEngine;

public class PhoneOrientation : MonoBehaviour
{
    private Quaternion _initialRotation;

    private void Start()
    {
        Input.gyro.enabled = true;
        _initialRotation = Input.gyro.attitude;
    }

    private void Update()
    {
        // Calculate the rotation from the gyroscope data
        Quaternion rotation = _initialRotation * Input.gyro.attitude;

        // Display the orientation in the console
        Debug.Log("Orientation: " + rotation.eulerAngles.ToString("F2"));
    }
}
