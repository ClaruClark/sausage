using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject slingPrefab;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider[] colls;
    private bool isTriggered;
    public int myID { get; set; }
    public Platform parent { get; set; }
    private bool DontDestroy;
   public void Flip()
    {
        int rand = Random.Range(0, 8);
        switch (rand)
        {
            case 0:
                transform.rotation = new Quaternion(0,0,0,0);
                break;
            case 1:
                transform.rotation = new Quaternion(0, -75, 0, 0);
                break;
            case 2:
                transform.rotation = new Quaternion(0, 90, 0, 0);
                break;
            case 3:
                transform.rotation = new Quaternion(0, 145, 0, 0);
                break;
            case 4:
                transform.rotation = new Quaternion(0, 180, 0, 0);
                break;
            case 5:
                transform.rotation = new Quaternion(0, -200, 0, 0);
                break;
            case 6:
                transform.rotation = new Quaternion(0, 280, 0, 0);
                break;
            case 7:
                transform.rotation = new Quaternion(0, -15, 0, 0);
                break;
            case 8:
                transform.rotation = new Quaternion(0, 10, 0, 0);
                break;
            default:
                transform.rotation = new Quaternion(0, 180, 0, 0);
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       // DontDestroy = true;
     //   GameManager.Instance.ShowMe();
        if (isTriggered)
            return;

        if (collision.collider.CompareTag("sausage"))
        {
            ChangeColor();
            
            isTriggered = true;
        }

    }
    public void ChangeColor()
    {
        mesh.sharedMaterial.color = LevelGenerator.Instance.ChangeColor();
    }
    public void SetSling()
    {
        
        if (LevelGenerator.Instance.currentSling)
            Destroy(LevelGenerator.Instance.currentSling.gameObject);
        GameObject sling = Instantiate(slingPrefab, transform);
        sling.transform.position = spawnPoint.position;
        mesh.enabled = true;

        foreach (BoxCollider c in colls)
            c.enabled = true;

        DontDestroy = false;
    }

    public void SetSling(Vector3 pos)
    {

        if (LevelGenerator.Instance.currentSling)
            Destroy(LevelGenerator.Instance.currentSling.gameObject);
        GameObject sling = Instantiate(slingPrefab, transform);
        sling.transform.position = pos;

        mesh.enabled = true;
        foreach (BoxCollider c in colls)
            c.enabled = true;

        DontDestroy = false;
    }

    public void CleanSling()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sausage"))
        {
            DontDestroy = true;
           // StartCoroutine(DD());
        }

    }*/
  /*  private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("sausage"))
        {
            DontDestroy = false;
        }

    }*/

  /*  IEnumerator DD()
    {
        yield return new WaitForSeconds(1);
        DontDestroy = false;
    }*/

    private void OnTriggerExit(Collider other)
    {
       // if (DontDestroy)
        //   return;

       // Debug.LogError("hrere");
        if (other.CompareTag("sausage"))
        {
            /* mesh.enabled = false;
             foreach (BoxCollider c in colls)
                 c.enabled = false;*/
            GameManager.Instance.HideMe(this.gameObject);
        }
           
    }
}
