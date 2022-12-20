using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel, gameplayPanel;

    public void TryAgain()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameplayPanel.SetActive(true);
    }
}
