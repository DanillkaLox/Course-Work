using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMode : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMode;
    public void Play1VS1()
    {
        AudioManager.Instance.Play("ButtonSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void VSComputer()
    {
        AudioManager.Instance.Play("ButtonSound");
    }
    
    public void Custom()
    {
        AudioManager.Instance.Play("ButtonSound");
    }

    public void Back()
    {
        AudioManager.Instance.Play("ButtonSound");
        mainMenu.SetActive(true);
        playMode.SetActive(false);
    }
}
