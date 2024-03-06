using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance => instance;
    static GameManager instance;

    [SerializeField] PlayerMovment playerPrefab;
    public PlayerMovment PlayerInstance => playerInstance;
    PlayerMovment playerInstance = null;
    Transform currentCheckpoint;


    [SerializeField] int maxLives = 5;
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (lives > value)
                Respawn();

            _lives = value;

            if (lives > maxLives)
                _lives = maxLives;

            if (lives < 1)
                GameOver();

        }
    }

    private int _score = 0;

    public int score
    {

        get => _score;
        set
        {
            _score = value;
        }

    }

    
    // Start is called before the first frame update
    void Start()
    {
    if(instance)
        {
            Destroy(gameObject);
            return;

        }

    instance = this;
    DontDestroyOnLoad(gameObject);
        _lives = maxLives;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int buildIndex = (SceneManager.GetActiveScene().name == "Level") ? 0:1;
            SceneManager.LoadScene(buildIndex);

            
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCheckPoint(GameObject.FindGameObjectWithTag("Test").transform);
        }
    }

    public void spawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckPoint(Transform updatedCheckpoint)
    {
        Debug.Log("checkpoint update");
        currentCheckpoint = updatedCheckpoint;
    }

    void Respawn()
    {
        Debug.Log("Respawn called");
        playerInstance.transform.position = currentCheckpoint.position; 
    }

    void GameOver()
    {
        SceneManager.LoadScene(2);

    }
}
