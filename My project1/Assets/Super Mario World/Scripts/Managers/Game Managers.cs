using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    //Private Lives Variable
    private int _lives = 10;

    //public variable for getting and setting lives
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            //all lives lost (zero counts as a life due to the check)
            if (value < 0)
            {
                GameOver();
                return;
            }

            //lost a life
            if (value < _lives)
            {
                Respawn();
            }

            //cannot roll over max lives
            if (value > maxLives)
            {
                value = maxLives;
            }

            _lives = value;

            Debug.Log($"Lives value on {gameObject.name} has changed to {lives}");
        }
    }

    //max lives that are possible
    [SerializeField] private int maxLives = 10;
    [SerializeField] private PlayerController playerPrefab;

    [HideInInspector] public PlayerController PlayerInstance => playerInstance;
    private PlayerController playerInstance;
    private Transform currentCheckpoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this instance persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    public void SpawnPlayer(Transform spawnLocation)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("PlayerPrefab is not assigned.");
            return;
        }

        playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;

        if (playerInstance == null)
        {
            Debug.LogError("PlayerInstance could not be instantiated.");
        }
        else
        {
            Debug.Log("PlayerInstance spawned.");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "Level")
                SceneManager.LoadScene("Title");
            else
                SceneManager.LoadScene("Level");
        }

    }

    void GameOver()
    {
        Debug.Log("Game Over, change it to move to a specific scene called Game Over");
        SceneManager.LoadScene("GameOver");
    }


    void Respawn()
    {
        playerInstance.transform.position = currentCheckpoint.position;
    }



    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }
}