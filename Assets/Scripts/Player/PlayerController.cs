using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D col;
    [SerializeField] private GameObject blueAuraPrefab; // Assign in Inspector
    public Transform playerTransform;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        playerTransform = GetComponent<Transform>();
        // Ensure GameManager health is set at start if needed
        GameManager.Instance.health = GameManager.Instance.maxHealth;
    }

    void Update()
    {
        float hValue = Input.GetAxis("Horizontal");
        float vValue = Input.GetAxis("Vertical");
        anim.SetFloat("hValue", Mathf.Abs(hValue));
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);
        float moveSpeed = 7f; 
        rb.linearVelocityX = hValue * moveSpeed;
        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);

        if (!currentState.IsName("Attack") && (Input.GetButtonDown("Fire1")))
        {
            anim.SetTrigger("Attack");
        }
        if (currentState.IsName("Attack"))
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (!currentState.IsName("Skill2") && (Input.GetButtonDown("Fire2")))
        {
            anim.SetTrigger("Skill2");
        }
        if (currentState.IsName("Skill2"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (!currentState.IsName("Skill1") && (Input.GetButtonDown("Fire3")))
        {
            anim.SetTrigger("Skill1");
        }
        if (currentState.IsName("Skill1"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (GameManager.Instance.health <= 0)
        {
            Die();
        }
    }

    void SpriteFlip(float hValue)
    {
        if (hValue != 0) sr.flipX = (hValue < 0);
    }

    // Damage method now only updates GameManager health
    public void TakeDamage(int amount)
    {
        GameManager.Instance.health -= amount;
        GameManager.Instance.health = Mathf.Clamp(GameManager.Instance.health, 0, GameManager.Instance.maxHealth);

        Debug.Log($"Player took {amount} damage. Current health: {GameManager.Instance.health}");

        
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
        SceneManager.LoadScene(2);
        // Add death logic here (animation, respawn, etc.)
    }
}