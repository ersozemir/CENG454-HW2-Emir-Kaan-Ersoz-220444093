using UnityEngine;
using TMPro; // Required for Task 2 UI logic

public class DangerZoneController : MonoBehaviour
{
    [Header("UI Configuration")]
    public TextMeshProUGUI statusText; // Reference to the HUD text

    [Header("Zone Settings")]
    [SerializeField] private string dangerZoneTag = "DangerZone";

    private void Start()
    {
        // Task 2: Initialize the HUD state at the start
        if (statusText != null)
        {
            statusText.text = "Safe Zone";
            statusText.color = Color.green;
        }
    }

    // Task 2: Logic for entering the DangerZone trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dangerZoneTag))
        {
            UpdateHUD("Entered a Dangerous Zone!", Color.red);
        }
    }

    // Task 2: Logic for leaving the DangerZone trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(dangerZoneTag))
        {
            UpdateHUD("Safe Zone", Color.green);
        }
    }

    // Helper method to keep code clean
    private void UpdateHUD(string message, Color textColor)
    {
        if (statusText != null)
        {
            statusText.text = message;
            statusText.color = textColor;
        }
    }
}