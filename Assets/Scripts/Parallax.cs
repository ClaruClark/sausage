using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : Singleton<Parallax>
{
    [SerializeField] Transform cam;
    [SerializeField] Vector3 offset;
    private float[] startposX;
    private float[] lengthZ;
    [SerializeField] Transform[] layers;
    [SerializeField] float[] parallaxEfx;


    public void Restart()
    {
        lengthZ = new float[layers.Length];
        startposX = new float[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
          //  startposX[i] = layers[i].localPosition.x;
            startposX[i] = layers[i].position.z;
            lengthZ[i] = layers[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    private void Update()
    {
        transform.position = cam.position + offset;

        for(int i = 0; i < layers.Length; i++)
        {
            float tempX = (transform.position.z * (1 - parallaxEfx[i]));
            float distX = (transform.position.z * parallaxEfx[i]);

            layers[i].position = new Vector3(layers[i].position.x, layers[i].position.y, startposX[i] + distX);

            if (tempX > startposX[i] + lengthZ[i])
                startposX[i] += lengthZ[i];
            else if (tempX < startposX[i] - lengthZ[i])
                startposX[i] -= lengthZ[i];

        }
       
    }
}
