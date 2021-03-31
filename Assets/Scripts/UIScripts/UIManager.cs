using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InGameUI inGameUI;
    public PauseMenu pauseMenu;
    public SkillMenu skillMenu;
    public LoseMenu loseMenu;
    public Stats stats;
    public GameManager gameManager;

    private void OnEnable()
    {
        GameManager.pauseGame.AddListener(pauseMenu.PauseGame);
        GameManager.skillPauseGame.AddListener(skillMenu.PauseGame);
        GameManager.skillResumeGame.AddListener(skillMenu.Resume);
        GameManager.resumeGame.AddListener(pauseMenu.Resume);
    }

    private void OnDisable()
    {
        GameManager.pauseGame.RemoveListener(pauseMenu.PauseGame);
        GameManager.skillPauseGame.RemoveListener(skillMenu.PauseGame);
        GameManager.skillResumeGame.RemoveListener(skillMenu.Resume);
        GameManager.resumeGame.RemoveListener(pauseMenu.Resume);
    }

    // Start is called before the first frame update
    private void Start()
    {
        inGameUI.SetUp(this);
        pauseMenu.Setup(this);
        skillMenu.SetUp(this);
        loseMenu.SetUp(this);
    }
}

[System.Serializable]
public class InGameUI
{
    public GameObject inGameUI;
    public GameObject upgradeUI;
    public Text ammoText;
    public Text ammoCount;
    public Text scoreText;
    public Text scoreCount;
    public Text upgradeText;
    public Text upgradeInstructText;
    public Slider fuelGauge; // reference to our fuel gauge
    private UIManager uiManager;

    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowInGameUI(true);
        ShowUpgradeTextUI(false);

        ammoText.text = GameText.AmmoCount_Text;
        scoreText.text = GameText.Score_Text;
    }

    public void UpdateScore()
    {
        scoreCount.text = " " + uiManager.stats.playerScore;
    }

    public void UpdateAmmoUI(int amount, int maxAmount)
    {
        ammoCount.text = " " + amount + " / " + maxAmount;
    }

    public void UpdateFuelUI(float amount)
    {
        fuelGauge.value = amount;
    }

    public void ShowInGameUI(bool enable)
    {
        inGameUI.SetActive(enable);
    }

    public void ShowUpgradeTextUI(bool enable)
    {
        upgradeText.text = GameText.Upgrade_Text;
        upgradeInstructText.text = GameText.Upgrade_Instruct;
        upgradeUI.SetActive(enable);
    }
}

[System.Serializable]
public class LoseMenu
{
    public GameObject loseMenu;
    public Text gameOver;
    public Text outOfFuel;
    public Text scoreText;
    public Text scoreCount;
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;
    private UIManager uiManager;

    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowLoseMenu(false);

        gameOver.text = GameText.Lose_Title;
        outOfFuel.text = GameText.Lose_OutOfFuel;
        scoreText.text = GameText.Lose_ScoreText;

        retryButton.GetComponentInChildren<Text>().text = GameText.Lose_Retry;
        mainMenuButton.GetComponentInChildren<Text>().text = GameText.Lose_MainMenu;
        quitButton.GetComponentInChildren<Text>().text = GameText.Lose_Quit;

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.retryLevel?.Invoke());

        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(() => GameManager.returnToMenu?.Invoke());


        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => GameManager.quitGame?.Invoke());
    }

    public void UpdateLoseMenuScore()
    {
        scoreCount.text = " " + uiManager.stats.playerScore;
    }

    public void ShowLoseMenu(bool enable)
    {
        loseMenu.SetActive(enable);
    }
}

[System.Serializable]
public class SkillMenu
{
    public GameObject skillMenu;
    #region UI Text
    public Text title;
    public Text assignText;
    public Text remainingText;
    public Text pointCount;
    public Text fuelLevelText;
    public Text fuelLevel;
    public Text ammoLevelText;
    public Text ammoLevel;
    public Text turretLevelText;
    public Text turretLevel;
    #endregion
    public Button maxFuel;
    public Button maxAmmo;
    public Button turretSpeed;
    private UIManager uiManager;

    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowSkillUI(false);

        title.text = GameText.Skill_Title;
        assignText.text = GameText.Skill_AssignText;
        remainingText.text = GameText.Skill_RemainingText;

        fuelLevelText.text = GameText.Skill_FuelLevel;
        maxFuel.GetComponentInChildren<Text>().text = GameText.Skill_MaxFuel;
        ammoLevelText.text = GameText.Skill_AmmoLevel;
        maxAmmo.GetComponentInChildren<Text>().text = GameText.Skill_MaxAmmo;
        turretLevelText.text = GameText.Skill_TurretSpeedLevel;
        turretSpeed.GetComponentInChildren<Text>().text = GameText.Skill_TurretSpeed;

        maxFuel.onClick.AddListener(() => Stats.upgradeFuel?.Invoke());

        maxAmmo.onClick.AddListener(() => Stats.upgradeAmmo?.Invoke());

        turretSpeed.onClick.AddListener(() => Stats.upgradeTurret?.Invoke());
    }

    public void UpdateSkillPointUI()
    {
        pointCount.text = " " + uiManager.stats.statPoint;
        UpdateFuelLevelUI();
        UpdateAmmoLevelUI();
        UpdateTurretLevelUI();
    }

    public void UpdateFuelLevelUI()
    {
        fuelLevel.text = " " + uiManager.stats.upgradeFuelLevel;
    }

    public void UpdateAmmoLevelUI()
    {
        ammoLevel.text = " " + uiManager.stats.upgradeAmmoLevel;
    }

    public void UpdateTurretLevelUI()
    {
        turretLevel.text = " " + uiManager.stats.upgradeSpeedLevel;
    }

    public void Resume()
    {
        ShowSkillUI(false);
    }

    public void PauseGame()
    {
        ShowSkillUI(true);
    }

    public void ShowSkillUI(bool Enable)
    {
        skillMenu.SetActive(Enable);
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
    }

    public void PauseGame()
    {
        ShowPauseUI(true);
    }

    public void ShowPauseUI(bool Enable)
    {
        pauseMenu.SetActive(Enable);
    }
}