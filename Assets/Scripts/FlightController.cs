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
    [SerializeField] private float stabilizeSpeed = 2f; // Speed of returning to level flight

    private Rigidbody rb;

    void Start()
    {
        // Getting the Rigidbody component for physics-based interactions
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Executing all flight control methods in every frame
        HandleThrust();
        HandlePitch();
        HandleYaw();
        HandleRoll();
        
        // Optional: Keeps the plane from drifting into weird angles over time
        ApplyStabilization();
    }

    private void HandleThrust()
    {
        // Task 3-D: Moving forward using the Spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            // The model moves along its local X-axis (Right) as per original setup
            transform.Translate(Vector3.right * thrustSpeed * Time.deltaTime);
        }
    }

    private void HandlePitch()
    {
        // Task 3-A: Pitch control using Vertical axis (W/S)
        float pitchInput = Input.GetAxis("Vertical");

        // GROUND SAFETY CHECK: Prevents the nose from hitting the ground at low altitudes
        if (transform.position.y < 4.6f && pitchInput > 0)
        {
            return; // Block downward rotation near the ground
        }

        // Rotating around the local Z-axis for Pitching up/down
        transform.Rotate(Vector3.forward * pitchInput * pitchSpeed * Time.deltaTime);
    }

    private void HandleYaw()
    {
        // Task 3-B: Yaw control (Steering) using Horizontal axis (A/D)
        float yawInput = Input.GetAxis("Horizontal");
        
        // Rotating around the local Y-axis for left/right turns
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);
    }

    private void HandleRoll()
    {
        // Task 3-C: Roll control using Q and E keys
        float rollInput = 0;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1;
        if (Input.GetKey(KeyCode.E)) rollInput = -1;

        // Rotating around the local X-axis for wing banking
        transform.Rotate(Vector3.right * rollInput * rollSpeed * Time.deltaTime);
    }

    private void ApplyStabilization()
    {
        // Only stabilize if the player is NOT pressing any rotation keys
        if (!Input.anyKey)
        {
            // Gently pulling the aircraft's rotation back to a horizontal state
            Quaternion targetRotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * stabilizeSpeed);
        }
    }
}