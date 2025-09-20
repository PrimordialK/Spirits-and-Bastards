using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXpos;
    [SerializeField] private float maxXpos;
    [SerializeField] private Transform target;
    [SerializeField] private float yOffset = 0f; // Add this line

    void Start()
    {
        if (!target)
        {
            Debug.LogError("Target not set for CameraFollow script! Please assign a target in the inspector.");
        }
    }

    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x, minXpos, maxXpos);
        pos.y = target.position.y + yOffset; // Apply y offset here
        transform.position = pos;
    }
}
