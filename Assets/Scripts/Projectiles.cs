using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource audioSource;

    public float lifeTime;

    public Vector2 initialVelocity;

    // Audio clips
    [SerializeField] AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (lifeTime <= 0)
        {
            lifeTime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = initialVelocity;
        Destroy(gameObject, lifeTime);

        sr = GetComponent<SpriteRenderer>();

        if (initialVelocity.x > 0)
        {
            sr.flipX = true;
        }
        else 
        { 
            sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "ThrowingEnemy" && collision.gameObject.tag != "Chest"  )
        {
            lifeTime = 0.0f;
            Destroy(gameObject, lifeTime);

        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.lives--;
            Debug.Log("Lives:" + GameManager.Instance.lives);
            audioSource.PlayOneShot(hurtSound);

        }
    }
}