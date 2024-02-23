using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] float minXClamp;
    [SerializeField] float maxXClamp;
    [SerializeField] float minYClamp;
    [SerializeField] float maxYClamp;


    void FixedUpdate()
    {
       Vector3 cameraPos = transform.position;

        cameraPos.x = Mathf.Clamp(_target.transform.position.x, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(_target.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
