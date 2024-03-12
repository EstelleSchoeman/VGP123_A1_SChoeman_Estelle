using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyThrow : Enemy

{   
    public GameObject playerObject;
    public float Distance;
    

    [SerializeField] float projectileFireRate;
    float timeSinceLastFire = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();   
            
        if(projectileFireRate <=0)
        {
            projectileFireRate = 2;
        }
            
            }

    // Update is called once per frame
    void Update()
    {

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

    
        Distance = Vector2.Distance(gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);

        if(Distance >= 10) 
        {
            //anim.SetBool("Idle", true);
        }
        else if(Distance < 10) 
        {
            //anim.SetBool("Idle", false);

            if (curPlayingClips[0].clip.name != "Fire")
            {
                if (Time.time > timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                    Shoot shoot = GetComponent<Shoot>();
                    shoot.Fire();
                    Debug.Log("Throw");
                }
            }
        }
        
        if(GameManager.Instance.PlayerInstance.transform.position.x > transform.position.x) 
        {
            sr.flipX = true;
        }
        else if(GameManager.Instance.PlayerInstance.transform.position.x < transform.position.x)
        {
            sr.flipX = false;
        }
    }
}
