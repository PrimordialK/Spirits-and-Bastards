using UnityEngine;

public class Skill1Activation : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Transform shieldSpawn;
    [SerializeField] private GameObject shieldPrefab; // Assign your shield prefab in the Inspector
    [SerializeField] private int manaCost = 30; // Mana cost for activating the shield
    [SerializeField] private int healAmount = 40; // Amount to heal

    public AudioClip shieldSound;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (shieldSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    // This method will be called by the animation event
    public void ActivateShield()
    {
        // Check mana before activating shield
        if (!GameManager.Instance.TrySpendMana(manaCost))
        {
            Debug.Log("Not enough mana to activate shield!");
            return;
        }

        // Heal the player by 40 via GameManager
        GameManager.Instance.health += healAmount;

        if (shieldPrefab != null && shieldSpawn != null)
        {
            Instantiate(shieldPrefab, shieldSpawn.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Shield prefab or spawn point not assigned.");
        }
        audioSource?.PlayOneShot(shieldSound);
    }
}
