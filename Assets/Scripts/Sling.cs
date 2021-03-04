using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    [SerializeField] Sausage sPrefab;
    [SerializeField] Transform startPos;
    [SerializeField] GameObject startDot;
    private Sausage sausage;
    private void Start()
    {
        if(CameraController.Instance.sausage)
        Destroy(CameraController.Instance.sausage.gameObject);
        if(LevelGenerator.Instance.currentSling)
        Destroy(LevelGenerator.Instance.currentSling.gameObject);

        sausage = Instantiate(sPrefab.gameObject, startPos).GetComponent<Sausage>();
        InputManager.Instance.dotsParent = startDot;
        InputManager.Instance.UpdateUs();
        
    }
}
