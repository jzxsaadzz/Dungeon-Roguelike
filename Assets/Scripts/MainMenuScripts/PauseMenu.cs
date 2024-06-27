using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;


    
    public Button continueButton;
    public Button exitButton;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        continueButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(ExitGame);
        GameInput.Instance.OnPause += Pause; 
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause(object sender, EventArgs e)
    {
        if (!isPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        SceneManager.LoadScene("MainMenu");
    }
}
