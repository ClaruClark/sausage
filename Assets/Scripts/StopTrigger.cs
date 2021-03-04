using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    [SerializeField] Cube parent;
    private bool isTriggered;
    private Vector3 col;
  

    private void OnCollisionEnter(Collision collision)
    {
        if (isTriggered)
            return;

        if (collision.collider.CompareTag("sausage"))
        {
            col = collision.contacts[0].point;
            Sausage();

            isTriggered = true;
        }

    }
    private void Sausage()
    {
        CameraController.Instance.sausage.StopFly();
        StartCoroutine(SausMotion());
    }

    IEnumerator SausMotion()
    {
        yield return new WaitForSeconds(0.3f);
        parent.parent.SetSausage(parent.myID, col);
        isTriggered = false;
    }
}
