using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    
    private AudioSource audioSource;


    public delegate PlayerController PlayerSpawnDelegate(PlayerController playerInstance);
    public event PlayerSpawnDelegate OnPlayerControllerCreated;


    #region PlayerControllerInformation
    public PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController playerInstance => _playerInstance;
    private Vector3 currentCheckPoint;
    #endregion
    public event Action<int> OnLivesChanged;
    public event Action<int> OnHealthChanged;

    #region Stats
    private int _mana = 100;
    private int _health = 100;
    private int _score = 0;

    public int mana
    {
        get => _mana;
        set
        {
            if (value < 0)
            {
                Debug.Log("Not enough Mana");
                _mana = 0;
            }
            else if (value > 100)
                _mana = maxMana;
            else
                _mana = value;
        }
    } 
    public int maxMana = 100;
    public int score
    {
        get => _score;
        set
        {
            if (value < 0)
                _score = 0;
            else
                _score = value;
        }
    }

    public int health
    {
        get => _health;
        set
        {
            if (value < 0)
            {
                Debug.Log("Player Dead");
                _health = 0;
            }
            else if (value > maxHealth)
            {
                _health = maxHealth;
            }
            else
            {
                _health = value;
            }
            OnHealthChanged?.Invoke(_health);
        }
    }
    public int maxHealth = 100;
    #endregion

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null) // corrected: == instead of =
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Respawn()
    {
        if (_playerInstance != null)
        {
            Destroy(_playerInstance.gameObject);
        }
        _playerInstance = Instantiate(playerPrefab, currentCheckPoint, Quaternion.identity);
    }

    public void StartLevel(Vector3 startPosition)
    {
        currentCheckPoint = startPosition;
        _playerInstance = Instantiate(playerPrefab, currentCheckPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex == 0)
        //    {
        //        SceneManager.LoadScene(1);
        //    }
        //    else if (SceneManager.GetActiveScene().buildIndex == 2)
        //    {
        //        SceneManager.LoadScene(0);
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene(0);
        //    }
        //}

    }

    public bool TrySpendMana(int amount)
    {
        if (mana >= amount)
        {
            mana -= amount;
            return true;
        }
        return false;
    }
}

