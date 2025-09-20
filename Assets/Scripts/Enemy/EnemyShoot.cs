using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    [SerializeField] private Projectile projectilePrefab = null;

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10.0f, 0f);
            Debug.LogWarning("Initial shot velocity is not set. Using default value: " + initShotVelocity);
        }
        if (leftSpawn == null || rightSpawn == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points or projectile prefab not set. Please assign leftSpawn, rightSpawn, and projectilePrefab in the inspector.");
        }
    }

    public void Attack()
    {
        Projectile curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, rightSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
            curProjectile.SetFacing(false); // Face right
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, leftSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(-initShotVelocity);
            curProjectile.SetFacing(true); // Face left
        }
        audioSource?.PlayOneShot(shootSound);
    }
}