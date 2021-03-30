using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent pauseGame = new UnityEvent();
    public static UnityEvent resumeGame = new UnityEvent();
    public static UnityEvent skillPauseGame = new UnityEvent();
    public static UnityEvent skillResumeGame = new UnityEvent();

    public static UnityEvent returnToMenu = new UnityEvent();
    public static UnityEvent retryLevel = new UnityEvent();
    public static UnityEvent quitGame = new UnityEvent();

    public bool isPaused = false;

    private void OnEnable()
    {
        pauseGame.AddListener(PauseGame);
        resumeGame.AddListener(ResumeGame);
        skillPauseGame.AddListener(PauseGame);
        skillResumeGame.AddListener(ResumeGame);
        returnToMenu.AddListener(ReturnToMenu);
        retryLevel.AddListener(Retry);
        quitGame.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        pauseGame.RemoveListener(PauseGame);
        resumeGame.RemoveListener(ResumeGame);
        skillPauseGame.RemoveListener(PauseGame);
        skillResumeGame.RemoveListener(ResumeGame);
        returnToMenu.RemoveListener(ReturnToMenu);
        retryLevel.RemoveListener(Retry);
        quitGame.RemoveListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            resumeGame?.Invoke();
            isPaused = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseGame?.Invoke();
            isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isPaused)
        {
            skillResumeGame?.Invoke();
            isPaused = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            skillPauseGame?.Invoke();
            isPaused = true;
        }
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        // CancelInvoke("LoadNext");
        // Invoke("LoadNext", 3);
        // Time.timeScale = 1;
    }

    private void LoadNext()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}