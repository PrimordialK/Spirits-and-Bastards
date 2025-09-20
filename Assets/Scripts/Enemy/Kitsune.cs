using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
public class Kitsune : Enemy
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float rangedAttackMinRange = 3f;   // Minimum distance for ranged attack
    [SerializeField] private float rangedAttackMaxRange = 7f;   // Maximum distance for ranged attack
    private float nextAttackTime = 0f;
    private float nextRangedAttackTime = 0f;
    [SerializeField] private float rangedAttackCooldown = 3f;

    public AudioClip attackSound;
    public AudioClip rangeAttackSound;
    private AudioSource audioSource;

    private Transform playerTransform;

    protected override void Start()
    {

        if (attackSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (rangeAttackSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        base.Start();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // Find the player by tag (make sure your player is tagged "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Melee attack
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            sr.flipX = playerTransform.position.x < transform.position.x;
            if (Time.time >= nextAttackTime)
            {
                anim.SetTrigger("Attack1");
                nextAttackTime = Time.time + attackCooldown;
                audioSource?.PlayOneShot(attackSound);
            }
        }
        else if (distanceToPlayer > rangedAttackMinRange && distanceToPlayer <= rangedAttackMaxRange)
        {
            // Ranged attack
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            sr.flipX = playerTransform.position.x < transform.position.x;
            if (Time.time >= nextRangedAttackTime)
            {
                anim.SetTrigger("Attack2");
                nextRangedAttackTime = Time.time + rangedAttackCooldown;
                audioSource?.PlayOneShot(rangeAttackSound);
            }
        }
        else if (distanceToPlayer <= chaseRange)
        {
            // Walk toward the player
            sr.flipX = playerTransform.position.x < transform.position.x;
            anim.SetTrigger("Walk");
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Idle
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    // Optional: Visualize ranges in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangedAttackMinRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangedAttackMaxRange);
    }
}

