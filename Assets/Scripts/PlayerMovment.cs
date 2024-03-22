using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR.WSA;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerMovment : MonoBehaviour
{
    //Testmode toggle
    public bool TestMode = true;

    //Component
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource audioSource;

    // *E: Shows under player Movment script
    [SerializeField] float speed = 7.0f;
    [SerializeField] float jumpForce = 7.0f;

    [SerializeField] Transform GroundCheck;

    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] float groundCheckRadius = 0.0f;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isAirborne;

    // Audio clips
    [SerializeField] AudioClip pogoStickSound;
    [SerializeField] AudioClip playerJumpSound;
    [SerializeField] AudioClip playerClimbSound;
    [SerializeField] AudioClip LoseLifeSound;
    private float timeSinceLastPlayed;

    [SerializeField] AudioClip playerKillThrowEnemySound;

    //[SerializeField] AudioClip hurtSound;

    private float _verticle;
    public float climbspeed = 4;
    private bool isClimbing;

    private HashSet<GameObject> Vine = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode)
            {
                Debug.Log("Default Value of Speed has changed on " + gameObject.name);
            }
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            if (TestMode)
            {
                Debug.Log("Groundcheck radius value has been defaulted on " + gameObject.name);
            }
        }

        if (GroundCheck == null)
        {
            //GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            GameObject groundCheckObject = new GameObject();
            groundCheckObject.transform.SetParent(gameObject.transform);
            groundCheckObject.transform.localPosition = Vector3.zero;
            groundCheckObject.name = "GroundCheck";
            GroundCheck = groundCheckObject.transform;
            if (TestMode) Debug.Log("Groundcheck object was created on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (CanvasManager.pausedTime == true)
            return;

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        //controls the left and right movment 
        float xInput = Input.GetAxisRaw("Horizontal");

        //controls upwards movment 
        _verticle = Input.GetAxisRaw("Vertical");

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2 (rb.velocity.x, _verticle * climbspeed);
        }

        else { rb.gravityScale = 1; }


        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        if (xInput != 0)
        {
            sr.flipX = (xInput > 0);
        }

       //isGrounded = Physics2D.OverlapArea(GroundCheck.position, groundCheckRadius, isGroundLayer);
        isGrounded = Physics2D.OverlapArea( new Vector2((float)(GroundCheck.position.x + 1.3 * 0.5), (float)(GroundCheck.position.y + groundCheckRadius  * 0.5)), new Vector2((float)(GroundCheck.position.x - 1.3 * 0.5), (float)(GroundCheck.position.y + groundCheckRadius * -0.5)), isGroundLayer);

        //*Animation left and right:
        anim.SetFloat("Input", Mathf.Abs(xInput));
        anim.SetFloat("VerticleInput", Mathf.Abs(_verticle));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsClimbing", isClimbing);


        if (Input.GetButtonDown("Jump") && isGrounded && clipInfo[0].clip.name != "swing")
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Is jumping");
            anim.SetBool("IsJumping", true);

            if(clipInfo[0].clip.name != "JumpAttack" )
            {
                audioSource.PlayOneShot(playerJumpSound);
            }
            
        }

        if (isGrounded == false && anim.GetBool("IsJumping") == true)
        {
            isAirborne = true;
            
        }


        anim.SetBool("IsAirBorne", isAirborne);

        if (isAirborne == true && isGrounded == true)
        {
            isAirborne = false;
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetButtonDown("Fire1") && isGrounded == true)
        {
            //Debug.Log("Fire 1 Pressed");
            anim.SetBool("IsSwinging", true);

        }

        if (clipInfo[0].clip.name == "swing" && anim.GetBool("IsJumping") == false)
        {
            rb.velocity = Vector2.zero;
        }

        if (!Input.GetButtonDown("Fire1") && isGrounded == true)
        {
            Debug.Log("Fire 1  is not Pressed");
            anim.SetBool("IsSwinging", false);

        }

        if (anim.GetBool("IsJumping") == true && isAirborne == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("JumpAttack");
                audioSource.PlayOneShot(pogoStickSound);
            }

          
        }

        // Vine handeling

        

        if(Vine.Count > 0 && Mathf.Abs(_verticle) > 0f)
        {
            isClimbing = true;

            if (Time.time > timeSinceLastPlayed + (playerClimbSound.length)/3)
            {
                audioSource.PlayOneShot(playerClimbSound);
                timeSinceLastPlayed = Time.time;
                Debug.Log("Playing Vine sound");

            }
        }

        else if (Vine.Count <= 0)
            isClimbing = false;

    }



    //climbing:

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Vine"))
        {
            Vine.Add(col.gameObject);
            Debug.Log("Enter Vine Col");
            
        }

        Debug.Log(col.tag);

        if (col.CompareTag("FallCollider"))
        {
            GameManager.Instance.lives--;
            audioSource.PlayOneShot(LoseLifeSound);
        }

        if(col.CompareTag("ThrowEnemyHead"))
        {
            anim = GetComponent<Animator>();
            AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

            if (clipInfo[0].clip.name == "JumpAttach")
            {
                Destroy(col.gameObject);
                Destroy(col.gameObject.transform.parent.gameObject);
                Debug.Log("Enemy head");
                audioSource.PlayOneShot(playerKillThrowEnemySound);
            }
        }

        if (col.CompareTag("WinCol"))
        {
            GameManager.Instance.Win();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrowingEnemy") 
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            anim = GetComponent<Animator>();
            AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name != "JumpAttach")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GameManager.Instance.lives--;
                audioSource.PlayOneShot(LoseLifeSound);
            }

                // gotHurt = true;
                anim.SetTrigger("playerHurt");

                //anim = GetComponent<Animator>();
                //AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
                //curPlayingClips[0].clip.name = "Hurt";
                Debug.Log("On top!");
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Vine"))
        {
         Vine.Remove(col.gameObject);
            Debug.Log("Exit Vine Col");
        }
        Debug.Log(col.tag);
    }
}
