using UnityEngine;

public class MovementController : MonoBehaviour
{

    private Rigidbody rb;

    public float speed = 10f;
    public float pitchSpeed = 2f;
    public float rollSpeed = 2f;
    public float yawSpeed = 2f;

    private float pitch = 0f;
    private float roll = 0f;
    private float yaw = 0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Pitch");

        // Calculate pitch, roll and yaw based on mobile gyroscope input
        pitch = Mathf.Clamp(pitch + Input.acceleration.x * pitchSpeed, -90f, 90f);
        roll = Mathf.Clamp(roll - Input.acceleration.y * rollSpeed, -90f, 90f);
        yaw = Mathf.Clamp(yaw + Input.gyro.rotationRateUnbiased.z * yawSpeed, -180f, 180f);

        // Calculate the direction the plane should be facing based on pitch, roll and yaw
        Vector3 direction = Quaternion.Euler(pitch, yaw, roll) * Vector3.forward;

        // Apply forces to the rigidbody to move the plane
        rb.AddForce(direction * speed * moveVertical);
        rb.AddTorque(Vector3.right * roll * rollSpeed);
        rb.AddTorque(Vector3.up * yaw * yawSpeed);
        rb.AddTorque(Vector3.forward * pitch * pitchSpeed);
    }
}
