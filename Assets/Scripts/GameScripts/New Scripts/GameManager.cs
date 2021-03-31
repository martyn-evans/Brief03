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
        // adds functions to my events
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
        // removes functions from my events
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
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused) // checks if escape key has been pressed and is paused
        {
            resumeGame?.Invoke(); // resumes game
            isPaused = false; // is not paused
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) // checks if escape key has been pressed and is not paused
        {
            pauseGame?.Invoke(); // pauses game, displays pause menu
            isPaused = true; // is paused

            if (Input.GetKeyDown(KeyCode.Tab) && isPaused) // checks if escape key has been pressed and is paused
            {
                return; // do nothing
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isPaused) // checks if tab key has been pressed and is paused
        {
            skillResumeGame?.Invoke(); // resumes game
            isPaused = false; // is not paused
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !isPaused) // checks if tab key has been pressed and is not paused
        {
            skillPauseGame?.Invoke(); // pauses game, displays skill menu
            isPaused = true; // is paused

            if (Input.GetKeyDown(KeyCode.Escape) && isPaused) // checks if escape key has been pressed and is paused
            {
                return; // do nothing
            }
        }
    }

    /// <summary>
    /// loads up the active scene again
    /// </summary>
    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// pauses game
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// resumes game
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// loads up the main menu scene
    /// </summary>
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

    /// <summary>
    /// quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}