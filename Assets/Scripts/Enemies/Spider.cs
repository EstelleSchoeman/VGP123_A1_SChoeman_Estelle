using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    AudioSource audioSource;

    [SerializeField] AudioClip playerKillThrowEnemySound;

    Rigidbody2D rb;
    // Y- Velocity of the spider
    [SerializeField] float yVelocity = 10;


    // Distance cariable between spider and player
    public float Distance;

    // spider and player in range bool
    private bool inRange = false;

    // Position A and B for haning 
    [SerializeField] public int y_hangPositionA;
    [SerializeField] public int y_hangPositionB;



    // Start is called before the first frame update
    public override void Start()
    {
        // call start from enemy class
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // set animation bool = scrip bool
        anim.SetBool("InRange", inRange);

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        // Math to get distane between enemy and player
        Distance = Vector2.Distance(gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);

        if (Distance < 5)
        {
            inRange = true;
            rb.velocity = new Vector2(0, -yVelocity);
            if (transform.position.y < y_hangPositionB)
            {
                rb.velocity = Vector2.zero;
            }
        }
        else if (Distance >= 5)
        {
            inRange = false;
            rb.velocity = new Vector2(0, yVelocity);

            if (transform.position.y > y_hangPositionA)
            {
                rb.velocity = Vector2.zero;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Animator playerAnimator = GameManager.Instance.PlayerInstance.GetComponent<Animator>();

        AnimatorClipInfo[] curPlayingClips = playerAnimator.GetCurrentAnimatorClipInfo(0);

        
          if (col.CompareTag("Player") && curPlayingClips[0].clip.name != "JumpAttach")
            {
                GameManager.Instance.lives--;
                //audioSource.PlayOneShot(LoseLifeSound);

                
            }

        if (curPlayingClips[0].clip.name == "JumpAttach")
        {
            audioSource.PlayOneShot(playerKillThrowEnemySound);
            Destroy(gameObject, (playerKillThrowEnemySound.length)/2);

        }

    }
}