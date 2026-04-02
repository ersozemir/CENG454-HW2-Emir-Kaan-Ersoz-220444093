using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private AudioSource launchAudioSource;

    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        // TODO (Task 3-A): instantiate the missile at launchPoint
        activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);

        // TODO (Task 3-B): give the missile its target
        MissileHoming homingScript = activeMissile.GetComponent<MissileHoming>();
        if (homingScript != null)
        {
            homingScript.SetTarget(target);
        }

        // TODO (Task 3-C): play launch audio and return the spawned missile
        if (launchAudioSource != null) launchAudioSource.Play();
        
        return activeMissile;
    }

    public void DestroyActiveMissile()
    {
        // TODO (Task 3-D): destroy the current missile safely if one exists
        if (activeMissile != null)
        {
            Destroy(activeMissile);
            activeMissile = null;
        }
    }
}
