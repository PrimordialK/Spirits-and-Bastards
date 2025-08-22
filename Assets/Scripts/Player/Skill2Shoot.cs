using UnityEngine;

public class Skill2Shoot : MonoBehaviour
{
    [SerializeField] private Skill2Projectile skill2ProjectilePrefab; // Prefab of the projectile
    [SerializeField] private float skillRange = 10f; // Maximum range to search for enemies
    public float groundY; // Y position for ground-level spawn

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Update groundY to the player's current Y position every frame
        groundY = transform.position.y;
    }

    public void Skill2()
    {
        if (skill2ProjectilePrefab == null)
        {
            Debug.LogError("Skill2Projectile prefab not set. Please assign it in the inspector.");
            return;
        }

        GameObject closestEnemy = FindClosestEnemyInRange();
        if (closestEnemy == null)
        {
            Debug.Log("No enemy in range for Skill2.");
            return;
        }

        Vector2 spawnPos = new Vector2(
            closestEnemy.transform.position.x,
            groundY // Use the updated groundY value
        );

        Instantiate(skill2ProjectilePrefab, spawnPos, Quaternion.identity);
    }

    private GameObject FindClosestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = skillRange;

        // Determine facing direction: +1 for right, -1 for left
        int facingDir = (sr != null && sr.flipX) ? -1 : 1;

        foreach (GameObject enemy in enemies)
        {
            Vector2 toEnemy = enemy.transform.position - transform.position;
            float dist = toEnemy.magnitude;

            // Only consider enemies in the facing direction
            if (dist < minDist && Mathf.Sign(toEnemy.x) == facingDir)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }
}