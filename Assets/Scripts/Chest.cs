using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Chest : MonoBehaviour
{
   Animator anim;
   AudioSource audioSource;

    [SerializeField] AudioClip ChestSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
           if (collision.gameObject.CompareTag("Player"))
            {   
                Debug.Log("Chest collide");
                AnimatorClipInfo[] PlayerclipInfo= collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            
            if(PlayerclipInfo[0].clip.name == "JumpAttach")
                {
                audioSource.PlayOneShot(ChestSound);
                Destroy(gameObject, (ChestSound.length)/2);
                GameManager.Instance.score += 500;
                Debug.Log("Score : " + GameManager.Instance.score);
                }
            }
       
       // {
        //    Debug.Log("PowerUp");
        //    Destroy(collision.gameObject);
       // }
    }
}
