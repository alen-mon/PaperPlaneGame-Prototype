using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;
public class MyOwnPlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the thrust ramps up or down")]
    public float throttleIncrement = 0.1f;
    [Tooltip("Maximum engine thrust")]
    public float maxThrust = 200f;
    [Tooltip("responsiveness sensitivity")]
    public float responsiveness = 10f;

    public float lift = 135f;

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;


    private float responseModifier
    {
        get {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    Rigidbody rb;
    [SerializeField] TextMeshProUGUI hud;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle,0f,100f);

    }

    private void Update()
    {
        HandleInputs();
        UpdateHUD();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(-transform.forward * roll * responseModifier);

    }


    private void UpdateHUD()
    {
        hud.text = "Throttle" + throttle.ToString("F0")+"%\n";
        hud.text = "AirSpeed" + (rb.velocity.magnitude*3.6f).ToString("F0")+"km/h\n";
        hud.text = "Altitude" + transform.position.y.ToString("F0") + " m";
    }
}
