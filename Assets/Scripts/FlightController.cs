// FlightController.cs
// CENG 454 – HW2: Sky-High Prototype
// Author: Emir Kaan Ersöz | Student ID: 220444093
using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Flight Settings")]
    [SerializeField] private float pitchSpeed = 90f;
    [SerializeField] private float yawSpeed = 90f;
    [SerializeField] private float rollSpeed = 90f;
    [SerializeField] private float thrustSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component for physics calculations
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Calling all control methods every frame
        HandleThrust();
        HandlePitch();
        HandleYaw();
        HandleRoll();
    }

    private void HandleThrust()
    {
        // TODO (Task 3-D): Forward thrust using Spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            // Moving the plane forward along its local X-axis (right)
            // Using transform.Translate as required by the assignment baseline
            transform.Translate(Vector3.right * thrustSpeed * Time.deltaTime);
        }
    }

    private void HandlePitch()
    {
        // TODO (Task 3-A): Pitch control (Vertical axis - W/S or Up/Down arrows)
        float pitchInput = Input.GetAxis("Vertical");

        // GROUND SAFETY CHECK: Prevents the nose from clipping through the terrain
        // If the plane's Y position is low (ground level ~4.4) 
        // and the player tries to rotate the nose DOWN (pitchInput > 0)
        if (transform.position.y < 4.6f && pitchInput > 0)
        {
            // Block the rotation to stop the nose from entering the ground
            return;
        }

        // Standard rotation around the local Z-axis for Pitch
        transform.Rotate(Vector3.forward * pitchInput * pitchSpeed * Time.deltaTime);
    }

    private void HandleYaw()
    {
        // TODO (Task 3-B): Yaw control (Horizontal axis - A/D or Left/Right arrows)
        float yawInput = Input.GetAxis("Horizontal");
        
        // Rotating around the local Y-axis (up) for steering left/right
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);
    }

    private void HandleRoll()
    {
        // TODO (Task 3-C): Roll control (Q and E keys)
        float rollInput = 0;

        if (Input.GetKey(KeyCode.Q)) rollInput = 1;
        if (Input.GetKey(KeyCode.E)) rollInput = -1;

        // Rotating around the local X-axis (right) for rolling the wings
        transform.Rotate(Vector3.right * rollInput * rollSpeed * Time.deltaTime);
    }
}