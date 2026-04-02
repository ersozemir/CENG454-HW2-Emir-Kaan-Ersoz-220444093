using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private DangerZoneController examManager;

    private Rigidbody rb;

    void Start()
    {
        // TODO (Task 3-G): cache GetComponent<Rigidbody>() into 'rb'
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO (Task 3-H): if the missile hits the aircraft, apply the chosen penalty
        if (other.CompareTag("Missile")) // Füzenin tag'ini "Missile" yapmayı unutma!
        {
            if (hitAudioSource != null) hitAudioSource.Play();

            // Penalty: Take the jet to the respawn point.
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
            
            // Make sure to reset the velocity of the jet to prevent it from flying away after respawn.
            if (rb != null) { rb.linearVelocity = Vector3.zero; rb.angularVelocity = Vector3.zero; }

            if (examManager != null) examManager.HandleFailure(); 
        }
    }
}