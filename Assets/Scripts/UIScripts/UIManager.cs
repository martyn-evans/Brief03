using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public variables
    public InGameUI inGameUI; // reference to the ingame class
    public PauseMenu pauseMenu; // reference to the pause menu class
    public SkillMenu skillMenu; // reference to the skill menu class
    public LoseWinMenu loseMenu; // reference to the lose menu class
    public Stats stats; // reference to the stats script
    public GameManager gameManager; // reference to the game manager
    public SceneLoading sceneLoadingOperation; // a reference to the scene loading script
    #endregion

    #region private variables
    #endregion

    #region Unity Functions
    private void Awake()
    {
        stats = FindObjectOfType<Stats>();
        gameManager = FindObjectOfType<GameManager>();
    }

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
        sceneLoadingOperation.levelLoadingScreen.SetupInGame(this);
        sceneLoadingOperation.levelLoadingScreen.ShowScreen(false);
    }
    #endregion
}

#region InGame UI
[System.Serializable]
public class InGameUI
{
    #region public variables
    public GameObject inGameUI;
    public GameObject upgradeUI;
    public Text ammoText;
    public Text ammoCount;
    public Text scoreText;
    public Text scoreCount;
    public Text upgradeText;
    public Text upgradeInstructText;
    public Slider fuelGauge; // reference to our fuel gauge
    #endregion

    #region private variables
    private UIManager uiManager;
    #endregion

    /// <summary>
    /// sets up the ingame UI
    /// </summary>
    /// <param name="current"></param>
    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowInGameUI(true);
        ShowUpgradeTextUI(false);

        ammoText.text = GameText.AmmoCount_Text;
        scoreText.text = GameText.Score_Text;
    }

    /// <summary>
    /// updates the scores in the ingame ui
    /// </summary>
    public void UpdateScore()
    {
        scoreCount.text = " " + uiManager.stats.playerScore;
    }

    /// <summary>
    /// updates the ammo in the ingame ui
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateAmmoUI(int amount, int maxAmount)
    {
        ammoCount.text = " " + amount + " / " + maxAmount;
    }

    /// <summary>
    /// updates the fuel in the ingame ui
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateFuelUI(float amount)
    {
        fuelGauge.value = amount;
    }

    /// <summary>
    /// enables/disables the ingame ui
    /// </summary>
    /// <param name="enable"></param>
    public void ShowInGameUI(bool enable)
    {
        inGameUI.SetActive(enable);
    }

    /// <summary>
    /// this enables/disables the upgrade available text
    /// </summary>
    /// <param name="enable"></param>
    public void ShowUpgradeTextUI(bool enable)
    {
        upgradeText.text = GameText.Upgrade_Text;
        upgradeInstructText.text = GameText.Upgrade_Instruct;
        upgradeUI.SetActive(enable);
    }
}
#endregion

#region Lose / Win Menu
[System.Serializable]
public class LoseWinMenu
{
    #region public variables
    public GameObject loseMenu;
    public Text gameOver;
    public Text reasonForLoss;
    public Text scoreText;
    public Text scoreCount;
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;
    #endregion

    #region private variables
    private UIManager uiManager;
    #endregion

    /// <summary>
    /// sets up the lose menu
    /// </summary>
    /// <param name="current"></param>
    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowLoseMenu(false,1);

        scoreText.text = GameText.Lose_ScoreText;


        mainMenuButton.GetComponentInChildren<Text>().text = GameText.Lose_MainMenu;
        quitButton.GetComponentInChildren<Text>().text = GameText.Lose_Quit;

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.retryLevel?.Invoke());

        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(() => GameManager.returnToMenu?.Invoke());


        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => GameManager.quitGame?.Invoke());
    }

    /// <summary>
    /// updates the score UI on the lose menu
    /// </summary>
    public void UpdateLoseMenuScore()
    {
        scoreCount.text = " " + uiManager.stats.playerScore;
    }

    /// <summary>
    /// shows the lose menu, if 1 displays fuel text, if 2 displays health text
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="number"></param>
    public void ShowLoseMenu(bool enable, int number)
    {
        loseMenu.SetActive(enable);

        if(number == 1)
        {
            gameOver.text = GameText.Lose_Title;
            reasonForLoss.text = GameText.Lose_OutOfFuel;
            SetUpRetryButton();
        }
        else if(number == 2)
        {
            gameOver.text = GameText.Lose_Title;
            reasonForLoss.text = GameText.Lose_OutOfHealth;
            SetUpRetryButton();
        }
        else if(number == 3)
        {
            gameOver.text = GameText.Win_Title;
            reasonForLoss.text = GameText.Win_PointsReached;
            SetUpNextLevelButton();
        }
    }

    /// <summary>
    /// this sets up the retry button when the player loses
    /// </summary>
    public void SetUpRetryButton()
    {
        retryButton.GetComponentInChildren<Text>().text = GameText.Lose_Retry;

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.retryLevel?.Invoke());
    }

    /// <summary>
    /// this sets up the next level button when the player wins
    /// </summary>
    public void SetUpNextLevelButton()
    {
        retryButton.GetComponentInChildren<Text>().text = GameText.Win_LoadNext;

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(LoadLevelTwo);
    }

    public void LoadLevelTwo()
    {
        uiManager.sceneLoadingOperation.levelLoadingScreen.ShowScreen(true);
        uiManager.sceneLoadingOperation.SetUp(2);
    }

    /// <summary>
    /// is called when the player reaches 140 points
    /// </summary>
    public void CheckToWinLevel()
    {
        if(uiManager.stats.playerScore>=140)
        {
            ShowLoseMenu(true, 3);
        }
    }
}
#endregion

#region Skill Menu
[System.Serializable]
public class SkillMenu
{
    #region public variables
    public GameObject skillMenu;
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
    public Text tankSpeedLevelText;
    public Text tankSpeedLevel;

    public Button maxFuelButton;
    public Button maxAmmoButton;
    public Button turretSpeedButton;
    public Button tankSpeedButton;
    #endregion

    #region private variables
    private UIManager uiManager;
    #endregion

    public void SetUp(UIManager current)
    {
        uiManager = current;
        ShowSkillUI(false);

        title.text = GameText.Skill_Title;
        assignText.text = GameText.Skill_AssignText;
        remainingText.text = GameText.Skill_RemainingText;

        fuelLevelText.text = GameText.Skill_FuelLevel;
        maxFuelButton.GetComponentInChildren<Text>().text = GameText.Skill_MaxFuel;
        ammoLevelText.text = GameText.Skill_AmmoLevel;
        maxAmmoButton.GetComponentInChildren<Text>().text = GameText.Skill_MaxAmmo;
        turretLevelText.text = GameText.Skill_TurretSpeedLevel;
        turretSpeedButton.GetComponentInChildren<Text>().text = GameText.Skill_TurretSpeed;
        tankSpeedLevelText.text = GameText.Skill_TankSpeedLevel;
        tankSpeedButton.GetComponentInChildren<Text>().text = GameText.Skill_TankSpeed;


        maxFuelButton.onClick.AddListener(() => Stats.upgradeFuel?.Invoke());

        maxAmmoButton.onClick.AddListener(() => Stats.upgradeAmmo?.Invoke());

        turretSpeedButton.onClick.AddListener(() => Stats.upgradeTurret?.Invoke());

        tankSpeedButton.onClick.AddListener(() => Stats.upgradeSpeed?.Invoke());
    }

    public void UpdateSkillPointUI()
    {
        pointCount.text = " " + uiManager.stats.statPoint;
        UpdateFuelLevelUI();
        UpdateAmmoLevelUI();
        UpdateTurretLevelUI();
        UpdateSpeedLevelUI();
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
        turretLevel.text = " " + uiManager.stats.upgradeTurretLevel;
    }
    
    public void UpdateSpeedLevelUI()
    {
        tankSpeedLevel.text = " " + uiManager.stats.upgradeSpeedLevel;
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
#endregion

#region Pause Menu
[System.Serializable] 
public class PauseMenu
{
    #region public variables
    public GameObject pauseMenu;
    public Text title;
    public Button resume;
    public Button returnToMenu;
    public Button quit;
    #endregion

    #region private variables
    private UIManager uiManager;
    #endregion

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
#endregion