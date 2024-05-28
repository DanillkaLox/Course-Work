using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pauseGame;
    
    public GameObject pauseGameMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.Play("ButtonSound");
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        AudioManager.Instance.Play("ButtonSound");
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        AudioManager.Instance.Play("ButtonSound");
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void Restart()
    {
        AudioManager.Instance.Play("ButtonSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
