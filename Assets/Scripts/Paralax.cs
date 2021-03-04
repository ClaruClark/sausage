using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float lengthZ, startposZ;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;
    void Start()
    {
        startposZ = transform.position.z;
        
        lengthZ = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempZ = (cam.transform.position.z * (1 - parallaxEffect));
        float distZ = (cam.transform.position.z * parallaxEffect);

        transform.position = new Vector3(transform.position.x, transform.position.y, startposZ + distZ);
        
        if (tempZ > startposZ + lengthZ/2)
            startposZ += lengthZ;
        else if (tempZ < startposZ - lengthZ/2)
            startposZ -= lengthZ;
        
    }
}
