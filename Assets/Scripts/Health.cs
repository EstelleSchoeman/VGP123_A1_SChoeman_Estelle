using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] int heartNum;
    private bool lostLife = false;

    SpriteRenderer sr;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(heartNum > GameManager.Instance.lives)
        {
            lostLife = true;
            anim.SetBool("LostLife", lostLife);
            
        }
    }
}
