using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float turnSpeed = 5f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        // TODO (Task 3-E): cache the aircraft transform
        target = newTarget;
    }

    void Update()
    {
        // TODO (Task 3-F): rotate toward the target and move forward
        if (target == null) return;

        // Calculate the direction to the target and create a rotation that looks in that direction
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Softly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        // Continuously move forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}