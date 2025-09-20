using UnityEngine;
using UnityEngine.Audio;
using static Enemy;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Skill2Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.0f;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }
}