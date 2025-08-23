using UnityEngine;

public class BlueAura : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float lifeTime = 30.0f;
    public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        if (playerTransform != null)

        {
            playerTransform.position = transform.position;
        }
        else
        {
            Debug.Log("Player postion not set"); 
        }
    }

}
