using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Projectile : MonoBehaviour
    
{
    [SerializeField, Range(0, 20)] private float lifetime = 1.0f;
    private Animator anim;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() => Destroy(gameObject, lifetime);
    
    
    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("Impact");
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 0.3f);
    }
}