using UnityEngine;

public class PaperPlaneController : MonoBehaviour
{
    public float thrustSpeed = 10f;
    public float maxSpeed = 50f;
    public float torqueSpeed = 5f;
    public float sensitivity = 0.1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float roll = transform.eulerAngles.z;
        float pitch = transform.eulerAngles.x;
        float yaw = transform.eulerAngles.y;

        // Calculate sensitivity-adjusted input values
        float sensitivityRoll = Input.GetAxis("Roll") * sensitivity;
        float sensitivityPitch = Input.GetAxis("Pitch") * sensitivity;
        float sensitivityYaw = Input.GetAxis("Yaw") * sensitivity;

        // Add torque and force based on input values
        rb.AddRelativeTorque(sensitivityPitch * torqueSpeed, 0f, -sensitivityRoll * torqueSpeed);
        rb.AddRelativeForce(Vector3.forward * thrustSpeed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Apply yaw rotation
        transform.rotation = Quaternion.Euler(pitch, yaw + sensitivityYaw, roll);
    }
}
