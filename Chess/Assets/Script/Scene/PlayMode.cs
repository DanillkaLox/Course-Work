using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMode : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMode;
    public void Play1VS1()
    {
        AudioManager.instance.Play("ButtonSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void VSComputer()
    {
        AudioManager.instance.Play("ButtonSound");
    }
    
    public void Custom()
    {
        AudioManager.instance.Play("ButtonSound");
    }

    public void Back()
    {
        AudioManager.instance.Play("ButtonSound");
        mainMenu.SetActive(true);
        playMode.SetActive(false);
    }
}
