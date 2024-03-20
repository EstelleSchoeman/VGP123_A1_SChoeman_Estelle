using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class webSpawner : MonoBehaviour
{
    public Spider spiderParent;
    public int spawnPointWebY;
    public GameObject webPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        spawnPointWebY = (spiderParent.y_hangPositionA - spiderParent.y_hangPositionB) /2 ;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnWeb()
    {
        //Instantiate(webPrefab, Vector2(gameObject.transform.parent.transform.position.x, spawnPointWebY,);

    }
}
