using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sausage : MonoBehaviour
{
    [SerializeField] Rigidbody[] rbs;
    [SerializeField] Rigidbody mainRB;
    void Start()
    {
        CameraController.Instance.SetTarget(this);
        mainRB.isKinematic = true;
        foreach (Rigidbody r in rbs)
            r.isKinematic = true;
    }

    public void Fly(Vector3 dist)
    {
        mainRB.isKinematic = false;
        foreach (Rigidbody r in rbs)
            r.isKinematic = false;

        Vector3 d = new Vector3(0, dist.y, dist.x);
        mainRB.useGravity = true;
        //mainRB.AddForce(d * (dist.y*0.8f), ForceMode.Impulse);
        mainRB.AddForce(dist, ForceMode.Impulse);
        foreach (Rigidbody r in rbs)
            r.useGravity = true;
        transform.parent = LevelGenerator.Instance.transform;
    }

    public void StopFly()
    {
        mainRB.isKinematic = true;
        foreach (Rigidbody r in rbs)
            r.isKinematic = true;
    }
    
}
