using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   Animator anim;
  

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
           if (collision.gameObject.CompareTag("Player"))
            {   
                Debug.Log("Chest col!!d");
                AnimatorClipInfo[] PlayerclipInfo= collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            
            if(PlayerclipInfo[0].clip.name == "JumpAttach")
                {
                Destroy(gameObject);
                }
            }
       
       // {
        //    Debug.Log("PowerUp");
        //    Destroy(collision.gameObject);
       // }
    }
}
