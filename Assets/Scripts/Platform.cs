using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] List<Cube> cubePool;
    [SerializeField] Transform[] posPool;

    public void Creation()
    {
        for(int i =0; i < posPool.Length; i++)
        {
            Cube nc = Instantiate(LevelGenerator.Instance.GetCube(), transform.parent).GetComponent<Cube>();
            nc.myID = i;
            nc.Flip();
            nc.transform.parent = transform;
            nc.transform.position = posPool[i].position;
            nc.parent = this;
            cubePool.Add(nc);
        }
    }

    public void FirstPlatform()
    {
        for(int i = 0; i < 7; i++)
        {
            cubePool[i].gameObject.SetActive(false);
        }
        cubePool[7].SetSling();
    }

 
    public void SetSausage(int n, Vector3 pos)
    {
        for (int i = 0; i < n; i++)
        {
            cubePool[i].gameObject.SetActive(false);
        }
        cubePool[n].SetSling(pos);
    }
   
}
