using UnityEngine;

public class BlueAura : MonoBehaviour
{
    private Animator anim;
    private float lifeTime = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Start() => Destroy(gameObject, lifeTime);




}
