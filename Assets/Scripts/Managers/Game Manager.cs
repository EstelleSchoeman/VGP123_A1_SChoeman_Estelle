using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[DefaultExecutionOrder(-1)]
//[RequireComponent(typeof(Animator))]

public class GameManager : MonoBehaviour
{
    //protected Animator anim;

    public UnityEvent<int> OnLifeValueChanged;
    public UnityEvent<int> OnScoreValueChanged;

    public static GameManager Instance => instance;
    static GameManager instance;


    [SerializeField] PlayerMovment playerPrefab;


    public PlayerMovment PlayerInstance => playerInstance;
    PlayerMovment playerInstance = null;
    Transform currentCheckpoint;


    [SerializeField] int maxLives = 5;

    public AudioSource BGM;
    public AudioClip newTrackTitle;
    public AudioClip newTrackLevel;
    public AudioClip newTrackWin;
    public AudioClip newTrackGameOver;

    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (lives > value)
            {
                Respawn();
            }

            _lives = value;

            if (lives > maxLives)
                _lives = maxLives;

            if (lives < 1)
                GameOver();

            if (OnLifeValueChanged != null)
                OnLifeValueChanged.Invoke(_lives);

        }
    }

    private int _score = 0;

    public int score
    {

        get => _score;
        set
        {
            _score = value;

            if (OnScoreValueChanged != null)
                OnScoreValueChanged.Invoke(_score);
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        // AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (instance)
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
            int buildIndex;
            if (SceneManager.GetActiveScene().name == "Level")
                buildIndex = 0;
            // Escape = Game Over scene
            else if (SceneManager.GetActiveScene().name == "Escape")
                buildIndex = 0;
            else if (SceneManager.GetActiveScene().name == "Win")
                buildIndex = 0;
            else
            {
                buildIndex = 1;
                ResetGame();
            }

            SceneManager.LoadScene(buildIndex);

        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCheckPoint(GameObject.FindGameObjectWithTag("Test").transform);
        }
    }

    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        if (buildIndex == 0)
        {
            ChangeBGM(newTrackTitle);
        }
        else if (buildIndex == 1)
        {
            ChangeBGM(newTrackLevel);
        }
        else if (buildIndex == 2)
        {
            ChangeBGM(newTrackGameOver);
        }
        else
            ChangeBGM(newTrackWin);
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
        //gotHurt = false;
        Debug.Log("gotHurt = false;");

    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
        ResetGame();

    }

    void ResetGame()
    {
        _lives = maxLives;
        _score = 0;


    }


    public void Win()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Win - @ col");
        ResetGame();

    }

    public void ChangeBGM(AudioClip music)
    {
        if (BGM.clip.name == music.name)
            return;

        BGM.Stop();
        BGM.clip = music;
        BGM.Play(); 
    }

}



    //public void hurt()
    //{
    //anim = GetComponent<Animator>();
    //AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
    //curPlayingClips[0].clip.name = "Die";
    //anim.SetBool("Dead", true); 

    // }
