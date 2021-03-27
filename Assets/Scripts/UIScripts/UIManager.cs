using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InGameUI inGameUI;
    public PauseMenu pauseMenu;
    public GameManager gameManager;

    private void OnEnable()
    {
        GameManager.pauseGame.AddListener(pauseMenu.PauseGame);
        GameManager.resumeGame.AddListener(pauseMenu.Resume);
    }

    private void OnDisable()
    {
        GameManager.pauseGame.RemoveListener(pauseMenu.PauseGame);
        GameManager.resumeGame.RemoveListener(pauseMenu.Resume);
    }

    // Start is called before the first frame update
    private void Start()
    {
        inGameUI.SetUp(this);
        pauseMenu.Setup(this);
    }
}

[System.Serializable]
public class InGameUI
{
    public GameObject inGameUI;
    public Text ammoText;
    public Text ammoCount;
    public Slider fuelGauge; // reference to our fuel gauge
    private UIManager uiManager;

    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowInGameUI(true);
    }

    public void UpdateAmmo(int amount)
    {
        ammoCount.text = " " + amount;
        Debug.Log("Bullets supposed to be in UI " + amount);
    }

    public void UpdateFuel(float amount)
    {
        fuelGauge.value = amount;
    }

    public void ShowInGameUI(bool enable)
    {
        inGameUI.SetActive(enable);
    }
}

[System.Serializable] 
public class PauseMenu
{
    public GameObject pauseMenu;
    public Text title;
    public Button resume;
    public Button returnToMenu;
    public Button quit;
    private UIManager uiManager;

    public void Setup(UIManager current)
    {
        uiManager = current;
        ShowPauseUI(false);

        title.text = GameText.Paused_Title;

        resume.GetComponentInChildren<Text>().text = GameText.Paused_Resume;
        returnToMenu.GetComponentInChildren<Text>().text = GameText.Paused_ReturnToMenu;
        quit.GetComponentInChildren<Text>().text = GameText.Paused_Quit;

        resume.onClick.RemoveAllListeners();
        resume.onClick.AddListener(() => GameManager.resumeGame?.Invoke());

        returnToMenu.onClick.RemoveAllListeners();
        returnToMenu.onClick.AddListener(() => GameManager.returnToMenu?.Invoke());

        quit.onClick.RemoveAllListeners();
        quit.onClick.AddListener(() => GameManager.quitGame?.Invoke());
    }

    public void Resume()
    {
        ShowPauseUI(false);
        // uiManager.inGameUI.ShowInGameUI(true);
    }

    public void PauseGame()
    {
        ShowPauseUI(true);
        // uiManager.inGameUI.ShowInGameUI(false);
    }

    public void ShowPauseUI(bool Enable)
    {
        pauseMenu.SetActive(Enable);
    }
}