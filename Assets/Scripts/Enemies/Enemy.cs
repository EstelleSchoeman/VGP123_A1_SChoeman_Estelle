using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;

    protected int health;
    protected int maxHelath;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHelath <= 0)
            maxHelath = 10;

        health = maxHelath;
    }

    // PErhaps make this without int and damage - just call function
    public virtual void TakeDamage(int damage)
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
