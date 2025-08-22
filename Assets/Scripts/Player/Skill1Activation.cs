using UnityEngine;

public class Skill1Activation : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Transform shieldSpawn;
    [SerializeField] private GameObject shieldPrefab; // Assign your shield prefab in the Inspector

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // This method will be called by the animation event
    public void ActivateShield()
    {
        if (shieldPrefab != null && shieldSpawn != null)
        {
            Instantiate(shieldPrefab, shieldSpawn.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Shield prefab or spawn point not assigned.");
        }
    }
}
