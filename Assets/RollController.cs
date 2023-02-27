using UnityEngine;

public class RollController : MonoBehaviour
{
    public float maxRollAngle = 45f; // Maximum roll angle in degrees
    public float rollSpeed = 10f; // Roll speed in degrees per second
    public GameObject plane; // The plane object to roll

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the rotation around the y axis (yaw) of the mobile device
        float mobileYaw = Input.gyro.attitude.eulerAngles.y;

        // Calculate the desired roll angle based on the mobile yaw
        float rollAngle = Mathf.Clamp(mobileYaw, -maxRollAngle, maxRollAngle);

        // Smoothly rotate the plane towards the desired roll angle
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -rollAngle);
        plane.transform.rotation = Quaternion.Slerp(plane.transform.rotation, targetRotation, rollSpeed * Time.deltaTime);

        // Check for collisions with the plane
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == plane)
            {
                // Push the game object away from the plane to prevent clipping
                Vector3 pushDirection = transform.position - plane.transform.position;
                rb.AddForce(pushDirection.normalized * 10f, ForceMode.Impulse);
            }
        }
    }
}
