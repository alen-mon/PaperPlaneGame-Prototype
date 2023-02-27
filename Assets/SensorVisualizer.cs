using UnityEngine;
using UnityEngine.UI;

public class SensorVisualizer : MonoBehaviour
{
    public float speed = 10.0f;
    public Text accelerometerText;
    public Text gyroscopeText;

    private Vector3 initialAccel;
    private Quaternion initialRotation;

    void Start()

    {
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.Log("Gyroscope is not supported on this device");
        }
        else
        {
            Input.gyro.enabled = true;
        }

        initialAccel = Input.acceleration;
        initialRotation = Input.gyro.attitude;
    }

    void Update()
    {
        // Accelerometer data
        Vector3 accel = Input.acceleration - initialAccel;
        transform.position += accel * speed;

        // Gyroscope data
        Quaternion rotFix = new Quaternion(0, 0, 1, 0);
        Quaternion gyro = Input.gyro.attitude * rotFix;
        gyro = new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w);
        Quaternion rot = Quaternion.Lerp(transform.rotation, gyro, Time.deltaTime * speed);

        // Update transform
        transform.rotation = rot;

        // Update text
        if (accelerometerText != null)
        {
            accelerometerText.text = $"Accelerometer:\nX: {accel.x:F2}\nY: {accel.y:F2}\nZ: {accel.z:F2}";
        }

        if (gyroscopeText != null)
        {
            Vector3 euler = gyro.eulerAngles;
            gyroscopeText.text = $"Gyroscope:\nX: {euler.x:F2}\nY: {euler.y:F2}\nZ: {euler.z:F2}";
        }
    }
}
