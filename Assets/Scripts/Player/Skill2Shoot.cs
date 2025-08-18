using UnityEngine;

public class Skill2Shoot : MonoBehaviour
{
    [SerializeField] private Skill2Projectile skill2ProjectilePrefab; // Prefab of the projectile
    [SerializeField] private float skillRange = 10f; // Maximum range to search for enemies
    public Transform groundY; // Y position for ground-level spawn (adjust as needed)

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        
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
            closestEnemy.transform.position.y // Enemy's Y position
           
        );

        Instantiate(skill2ProjectilePrefab, spawnPos, Quaternion.identity);
    }

    private GameObject FindClosestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = skillRange;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }
}