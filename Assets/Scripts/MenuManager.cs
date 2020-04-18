using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player.ToggleFreezeMovement(true);
    }
    
    public void ClickedPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ClickedSoundsToggle()
    {

    }

    public void ClickedQuit()
    {
        Application.Quit();
    }
}
