using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Distance : Singleton<Distance>
{
    private float lastDistance;
    [SerializeField] TextMeshProUGUI distanceScore;
    [SerializeField] int scoreCurrent;
    [SerializeField] int lastCheck;
    [SerializeField] int platformChecker;
    void Start()
    {
        scoreCurrent = 0;
        lastCheck = 0;
        //load last score
        distanceScore.text = scoreCurrent.ToString();
        
    }
    public void ClearDistance()
    {
        scoreCurrent = 0;
        lastCheck = 0;
        lastDistance = 0;
        distanceScore.text = scoreCurrent.ToString();
        if(CameraController.Instance.sausage)
        lastDistance = CameraController.Instance.sausage.transform.position.z;
    }

   
    public void SetDist(float z)
    {
        lastDistance = z;
    }
    void Update()
    {
        if (!UIManager.Instance.isGame)
            return;
        if (!CameraController.Instance.sausage)
            return;

        float cur = CameraController.Instance.sausage.transform.position.z;
        scoreCurrent += Mathf.RoundToInt(Mathf.Abs(cur - lastDistance)*10);
       // Debug.Log(Mathf.Abs(cur - lastDistance) * 10);
        lastDistance = CameraController.Instance.sausage.transform.position.z;
        distanceScore.text = scoreCurrent.ToString();

        CheckDistance();
    }

    private void CheckDistance()
    {
        if (scoreCurrent - lastCheck > platformChecker)
        {
            LevelGenerator.Instance.AddPlatform();
            lastCheck = scoreCurrent;
        }
            
    }
}
