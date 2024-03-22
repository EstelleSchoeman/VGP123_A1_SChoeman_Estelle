using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip DiamondSound;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

    }
    public enum PickupType
    {
        Diamond,
        
    }

    [SerializeField] PickupType currentCollectible;
    //[SerializeField] float timeToDestroy = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(DiamondSound);
            Destroy(gameObject, DiamondSound.length);
            Debug.Log("Diamond hit");
            GameManager.Instance.score += 1000;
            Debug.Log("Score : " + GameManager.Instance.score);
        }
    }



    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
