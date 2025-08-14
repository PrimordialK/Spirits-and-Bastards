using UnityEngine;




[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D col;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

    }

    void Update()
    {
        float hValue = Input.GetAxis("Horizontal");
        float vValue = Input.GetAxis("Vertical");
        anim.SetFloat("hValue", Mathf.Abs(hValue));
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);
        float moveSpeed = 5f; 
        rb.linearVelocityX = hValue * moveSpeed;

        if (!currentState.IsName("Attack") && (Input.GetButtonDown("Fire1")))
        {
            anim.SetTrigger("Attack");
        }
        if (currentState.IsName("Attack"))
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (!currentState.IsName("Skill1") && (Input.GetButtonDown("Fire2")))
        {
            anim.SetTrigger("Skill1");
        }
        if (currentState.IsName("Skill1"))
        {
            rb.linearVelocity = Vector2.zero;
        }

    }

    void SpriteFlip(float hValue)
    {
        if (hValue != 0) sr.flipX = (hValue < 0);
    }
}