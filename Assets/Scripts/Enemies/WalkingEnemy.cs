using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkingEnemy : Enemy
{
    Rigidbody2D rb;
    [SerializeField] float xVelocity;
    // Start is called before the first frame update
    public override void Start()
    {
      base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVelocity <= 0)
            xVelocity = 3;
    }

  
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if(curPlayingClips[0].clip.name == "Walking")
        {
            if (sr.flipX)
                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            else
                rb.velocity = new Vector2(-xVelocity, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Barrier"))
        {
            sr.flipX = !sr.flipX;
        }
    }
}

