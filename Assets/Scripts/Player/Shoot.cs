using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    [SerializeField] private Projectile projectilePrefab = null;
    [SerializeField] private int manaCost = 10; // Mana cost for shooting

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        if (shootSound != null)
        {

            TryGetComponent(out audioSource);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                Debug.Log("AudioSource component was missing. Added one dynamically.");
            }
        }
            sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10.0f, 0f);
            Debug.LogWarning("Initial shot velocity is not set. Using default value: " + initShotVelocity);
        }
        if (leftSpawn == null || rightSpawn == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points or projectile prefab not set. Please assign leftSpawn, rightSpawn, and attackProjectilePrefab in the inspector.");
        }
    }

    public void Attack()
    {
        // Check mana before shooting
        if (!GameManager.Instance.TrySpendMana(manaCost))
        {
            Debug.Log("Not enough mana to shoot!");
            return;
        }

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






