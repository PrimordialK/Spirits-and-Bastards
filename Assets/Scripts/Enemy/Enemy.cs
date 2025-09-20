using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using static Enemy;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public const int freezeDamage = 10;
    private Rigidbody2D rb;
    private bool isFrozen = false;
    protected Collider2D col;
    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] private int maxHealth = 20;
    private const int projectileDamage = 7;
    [SerializeField] public int knockBackForce = 5;
    private float knockBack = 5f;
    private Coroutine stopCoroutine;

    public AudioClip deathSound;
    private AudioSource audioSource;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (maxHealth <= 0)
        {
            Debug.Log("maxHealth must be greater than 0. Setting to 20.");
            maxHealth = 20;
        }
        health = maxHealth;

        // Setup AudioSource
        if (deathSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Default damageValue is projectileDamage (7)
    public virtual void TakeDamage(int damageValue, DamageType damagetype = DamageType.Projectile)
    { 
        health -= damageValue;

        if (health <= 0)
        {
            anim.SetTrigger("Dead");

            // Play death sound
            
            

            // Restore mana to max when this enemy is destroyed
            GameManager.Instance.mana = GameManager.Instance.maxMana;

            if (transform != null)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(transform.gameObject, 5.0f);
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 5.0f);
            }
        audioSource?.PlayOneShot(deathSound);
        if ( health < 0)
        {
            anim.SetTrigger("Hurt");
        }
        }


        
        
    }



    public enum DamageType
    {
        Projectile,
        Freeze
    }

    
}

