using UnityEngine;

public class MainScene : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMode;
    public void PlayButton()
    {
        AudioManager.Instance.Play("ButtonSound");
        mainMenu.SetActive(false);
        playMode.SetActive(true);
    }
    
    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
        AudioManager.Instance.Play("ButtonSound");
    }
}
