using UnityEngine;
using TMPro;

public class DangerZoneController : MonoBehaviour
{
    [Header("UI Configuration")]
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Missile System Integration")]
    [SerializeField] private MissileLauncher launcher; // Reference to the ground missile launcher
    [SerializeField] private float launchDelay = 5f;    // Requirement: 5 seconds countdown
    [SerializeField] private string dangerZoneTag = "DangerZone";

    private float timer = 0f;
    private bool isPlayerInZone = false;
    private bool missileFired = false;

    private void Start()
    {
        // Initial HUD state
        UpdateHUD("Safe Zone", Color.green);
    }

    private void Update()
    {
        // Task 3: Handle the 5-second delayed launch logic
        if (isPlayerInZone && !missileFired)
        {
            timer += Time.deltaTime;

            // Optional: Providing readability for the threat (Countdown)
            int countdown = Mathf.CeilToInt(launchDelay - timer);
            if (countdown > 0)
            {
                UpdateHUD("DANGER: Missile Launch in " + countdown, Color.yellow);
            }

            if (timer >= launchDelay)
            {
                TriggerLaunch();
            }
        }
    }

    private void TriggerLaunch()
    {
        missileFired = true;
        UpdateHUD("MISSILE INBOUND! EVADE!", Color.red);

        // Tell the launcher to fire and track this aircraft
        if (launcher != null)
        {
            launcher.Launch(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Task 2: Detecting entry into the Danger Zone
        if (other.CompareTag(dangerZoneTag))
        {
            isPlayerInZone = true;
            timer = 0f; // Reset timer upon entry
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Task 2 & 3: Resetting state upon leaving the zone
        if (other.CompareTag(dangerZoneTag))
        {
            isPlayerInZone = false;
            timer = 0f;
            missileFired = false;

            // Task 3: Automatically destroy the active missile when player escapes
            if (launcher != null)
            {
                launcher.DestroyActiveMissile();
            }

            UpdateHUD("Safe Zone", Color.green);
        }
    }

    // Task 3-J: This method is called by AircraftThreatHandler when a collision occurs
    public void HandleFailure()
    {
        UpdateHUD("MISSION FAILED: DESTROYED", Color.red);
        Debug.Log("Collision detected. Player failed the mission.");
        
        // Resetting internal flags
        missileFired = false;
        timer = 0f;
    }

    // Helper method to update the UI text and color
    private void UpdateHUD(string message, Color textColor)
    {
        if (statusText != null)
        {
            statusText.text = message;
            statusText.color = textColor;
        }
    }
}