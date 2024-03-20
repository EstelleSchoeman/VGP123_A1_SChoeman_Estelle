using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyThrow : Enemy
{ 
    Rigidbody2D rb;
    [SerializeField] float xVelocity;
  
    
    public float Distance;
    private bool inRange = false;

    [SerializeField] public int XwalkPositionA;
    [SerializeField] public int XwalkPositionB;
    [SerializeField] float projectileFireRate;
    float timeSinceLastFire = 0;

    // Start is called before the first frame update
    public override void Start()
    {      
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVelocity <= 0)
            xVelocity = 3;



        if (projectileFireRate <=0)
        {
            projectileFireRate = 4;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("InRange", inRange);

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        Distance = Vector2.Distance(gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);

        
        

            
            if (Distance >= 10)
            {
            //Debug.Log(inRange ? "Is in range" : "Is out of range");
            //Debug.Log("A : " + XwalkPositionA);
            //Debug.Log("B : " + XwalkPositionB);
            //Debug.Log("X : " + transform.position.x);
            inRange = false;
                if (transform.position.x < XwalkPositionA)
                {
                //Debug.Log("Pat A");
                    sr.flipX = true;
                    if (xVelocity <= 0)
                    { xVelocity = 3; }
                }

                else if (transform.position.x > XwalkPositionB)
                {
               //Debug.Log("Pat B");
                    sr.flipX = false;
                    if (xVelocity <= 0)
                    { xVelocity = 3; }
                }

            }

        if (curPlayingClips[0].clip.name == "Walk")
        {

            if (sr.flipX)
                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            else
                rb.velocity = new Vector2(-xVelocity, rb.velocity.y);

        }


        else if (Distance < 10)
        {
            inRange = true;


            if (curPlayingClips[0].clip.name != "Fire")
            {
                rb.velocity = Vector2.zero;
                if (Time.time > timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                    Shoot shoot = GetComponent<Shoot>();
                    shoot.Fire();
                    Debug.Log("Throw");

                }
            }

            if (curPlayingClips[0].clip.name != "Fire" && inRange == true)
            {

                rb.velocity = Vector2.zero;
            }



            if (GameManager.Instance.PlayerInstance.transform.position.x > transform.position.x)
            {
                sr.flipX = true;
            }
            else if (GameManager.Instance.PlayerInstance.transform.position.x < transform.position.x)
            {
                sr.flipX = false;
            }
        }
        
    }

    //private void throwEnemyWalk()
    //{ 
    //    if (transform.position.x == XwalkPositionA)
     //   {
     //   }
    //
    //}
}
