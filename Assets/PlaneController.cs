using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float maxRollAngle = 30.0f; // the maximum roll angle in degrees
    public float rollSmoothing = 1.0f; // the smoothing factor for the roll angle
    public float moveSpeed = 10.0f; // the speed of movement
    public float maxMovementAngle = 30.0f; // the maximum angle for movement in degrees
    public float movementSmoothing = 1.0f; // the smoothing factor for movement

    private float rollAngle = 0.0f; // the current roll angle
    private float movementAngle = 0.0f; // the current movement angle

    void Update()
    {
        // read the accelerometer data to get the roll and movement angles
        float rollInput = Input.acceleration.y * 90.0f;
        float movementInput = Input.acceleration.x * 90.0f;
        float rollside = Input.acceleration.z;

        // clamp the input values to the maximum angles
        float clampedRollInput = Mathf.Clamp(rollInput * -1.0f, -maxRollAngle, maxRollAngle);
        float clampedMovementInput = Mathf.Clamp(movementInput*-1.0f, -maxMovementAngle, maxMovementAngle);

        // smoothly interpolate the current roll angle towards the target roll angle
        rollAngle = Mathf.Lerp(rollAngle, clampedRollInput, rollSmoothing * Time.deltaTime);

        // smoothly interpolate the current movement angle towards the target movement angle
        movementAngle = Mathf.Lerp(movementAngle, clampedMovementInput, movementSmoothing * Time.deltaTime);

        // apply the roll and movement angles to the game object's rotation
        transform.rotation = Quaternion.Euler(-rollAngle, 0.0f, -movementAngle);

        // move the game object forward based on its current rotation
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
