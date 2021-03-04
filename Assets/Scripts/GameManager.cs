using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject lastHidden;
   
    void Start()
    {
        UIManager.Instance.Menu();
        LevelGenerator.Instance.NewLevel();
    }

    public void HideMe(GameObject hide)
    {
        lastHidden = hide;
       // hide.SetActive(false);
    }

    public GameObject GetObject()
    {
        return lastHidden;
    }
   
    public void ShowMe()
    {
        if(lastHidden)
        lastHidden.SetActive(true);
    }
}
