using UnityEngine;
using System.Collections;

public class AircraftThreatHandler : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private Transform respawnPoint; // Starting position

    [Header("UI Elements")]
    [SerializeField] private GameObject failureText; // Red message
    [SerializeField] private GameObject successText; // Green message
    [SerializeField] private GameObject statusText;  // The "Safe Zone" message

    [Header("Audio")]
    [SerializeField] private AudioSource hitAudioSource;

    private Rigidbody rb;
    private bool isMissionOver = false; 

    void Start()
    {
        // Get Rigidbody for physics control
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMissionOver) return;

        //Hit by a missile
        if (other.CompareTag("Missile"))
        {
            StartCoroutine(HandleHitSequence());
        }

        //Reached the landing area
        if (other.CompareTag("LandingArea")) 
        {
            StartCoroutine(HandleSuccessSequence());
        }
    }

    private IEnumerator HandleSuccessSequence()
    {
        isMissionOver = true; // Lock the triggers
        
        // UI Management
        if (statusText != null) statusText.SetActive(false); // Hide "Safe Zone"
        if (successText != null) successText.SetActive(true); // Show "Mission Completed"
        
        // Stop the aircraft
        if (rb != null) { rb.linearVelocity = Vector3.zero; rb.angularVelocity = Vector3.zero; }

        yield return new WaitForSeconds(4.0f); // Display time

        // Restart Loop
        ResetAircraft();
        if (successText != null) successText.SetActive(false);
        if (statusText != null) statusText.SetActive(true);
        isMissionOver = false;
    }

    private IEnumerator HandleHitSequence()
    {
        if (hitAudioSource != null) hitAudioSource.Play();
        if (failureText != null) failureText.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);

        ResetAircraft();
        if (failureText != null) failureText.SetActive(false);
        isMissionOver = false;
    }

    // Helper method 
    private void ResetAircraft()
    {
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (rb != null) 
        { 
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero; 
        }
    }
}