using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KarasuTengu : Enemy
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;

    private Transform playerTransform;

    protected override void Start()
    {
        base.Start();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // Find the player by tag (make sure your player is tagged "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;

        if (attackSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            // Walk toward the player
            sr.flipX = playerTransform.position.x < transform.position.x;
            anim.SetTrigger("Walk");
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
        }
        else if (distanceToPlayer <= attackRange)
        {
            // Stop and attack
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            sr.flipX = playerTransform.position.x < transform.position.x;
            if (Time.time >= nextAttackTime)
            {
                Attack();
                
            }
             
           
        }
        else
        {
            // Idle
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;
        audioSource?.PlayOneShot(attackSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            
                GameManager.Instance.health -= 10;
                Debug.Log("Player hit by KarasuTengu during attack! -10 health.");
            
        }
    }

    // Optional: Visualize ranges in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

