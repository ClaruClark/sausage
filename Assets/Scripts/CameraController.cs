using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    [SerializeField] Transform target;
    [SerializeField] float smoothMotion;
    [SerializeField] Vector3 offset;
    [SerializeField] bool Game;
    [SerializeField] Vector3 cur;
    private float lasPos;
    public Sausage sausage { get; set; }
    public void SetTarget(Sausage s)
    {
        if (target)
            target = null;

        target = s.transform;
        sausage = s;
        transform.position = target.position + offset;
        Distance.Instance.SetDist(sausage.transform.position.z);
    }
    private void Update()
    {
        if (!target)
            return;

        Game = UIManager.Instance.isGame;
        if (UIManager.Instance.isGame)
        FollowMode();
        else
        {
            lasPos = target.position.z;
        }
    }

    private void FollowMode()
    {
        if (lasPos == target.position.z)
            return;

        cur = target.position;
        lasPos = target.position.z;
        transform.position = target.position + offset;       
    }
}
