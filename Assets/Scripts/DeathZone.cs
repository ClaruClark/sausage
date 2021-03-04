using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sausage"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Loser());
        }
    }

    IEnumerator Loser()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.Lose();
    }
}
