using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMode : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMode;
    public void Play1VS1()
    {
        AudioManager.Instance.Play("ButtonSound");
        SceneManager.LoadScene("1VS1");
    }
    
    public void VSComputer()
    {
        AudioManager.Instance.Play("ButtonSound");
        SceneManager.LoadScene("VSComputer");
    }
    
    public void Custom()
    {
        SceneManager.LoadScene("Custom");
        AudioManager.Instance.Play("ButtonSound");
    }

    public void Back()
    {
        AudioManager.Instance.Play("ButtonSound");
        mainMenu.SetActive(true);
        playMode.SetActive(false);
    }
}
