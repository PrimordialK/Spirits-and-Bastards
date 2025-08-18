using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Skill2Projectile : MonoBehaviour

{
    [SerializeField] private float lifetime = 1.0f;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start() => Destroy(gameObject, lifetime);

   



}