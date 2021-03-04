using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Canvas GameCanvas;
    [SerializeField] Canvas MenuCanvas;
    [SerializeField] Canvas PauseCanvas;
    [SerializeField] Canvas LoseCanvas;
    public bool isGame { get; set; }
    private void Close()
    {
        GameCanvas.enabled = false;
        MenuCanvas.enabled = false;
        PauseCanvas.enabled = false;
        LoseCanvas.enabled = false;
    }

   
    public void Game()
    {
        Close();
        GameCanvas.enabled = true;
        isGame = true;
        Time.timeScale = 1;
        
    }

    public void Pause()
    {
        Close();
        PauseCanvas.enabled = true;
        isGame = false;
        Time.timeScale = 0;
    }

    public void Menu()
    {
        Close();
        MenuCanvas.enabled = true;
        isGame = false;
        Time.timeScale = 1;
        Distance.Instance.ClearDistance();
        Parallax.Instance.Restart();
    }

    public void Lose()
    {
        Close();
        LoseCanvas.enabled = true;
        isGame = false;
        Time.timeScale = 1;
    }
}
