using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMode;
    public void PlayButton()
    {
        AudioManager.instance.Play("ButtonSound");
        mainMenu.SetActive(false);
        playMode.SetActive(true);
    }
    
    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
        AudioManager.instance.Play("ButtonSound");
    }
}
