using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public variables
    public InGameUI inGameUI; // reference to the ingame class
    public PauseMenu pauseMenu; // reference to the pause menu class
    public SkillMenu skillMenu; // reference to the skill menu class
    public LoseWinMenu loseMenu; // reference to the lose menu class
    public EndScreen endScreen; // reference to the end screen class
    public Stats stats; // reference to the stats script
    public GameManager gameManager; // reference to the game manager
    public SceneLoading sceneLoadingOperation; // a reference to the scene loading script
    #endregion

    #region private variables
    private Coroutine waitToPauseRoutine;
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
        endScreen.Setup(this);
        sceneLoadingOperation.levelLoadingScreen.SetupInGame(this);
        sceneLoadingOperation.levelLoadingScreen.ShowScreen(false);
    }
    #endregion

    #region Coroutine used for LoseMenu TankZilla Lose Condition
    /// <summary>
    /// starts the coroutine to pause the game
    /// </summary>
    /// <param name="enable"></param>
    public void PauseGame(bool enable)
    {
        if(waitToPauseRoutine != null)
        {
            StopCoroutine(waitToPauseRoutine);
        }
        waitToPauseRoutine = StartCoroutine(WaitToPauseLose(enable));
        Debug.Log("Coroutine is called");
    }

    /// <summary>
    /// a coroutine to wait seconds to pause the timescale
    /// </summary>
    /// <param name="enable"></param>
    /// <returns></returns>
    private IEnumerator WaitToPauseLose(bool enable)
    {
        yield return new WaitForSeconds(1);

        if (enable == true)
        {
            Debug.Log("Time scale is 0");
            loseMenu.TimeScale(0);
        }
        yield return null;
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
        ShowWinLoseMenu(false,1);

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
    /// shows the win/lose menu, if 1 displays fuel fail text, if 2 displays health fail text. if 3 displays win text
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="number"></param>
    public void ShowWinLoseMenu(bool enable, int number)
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
            uiManager.PauseGame(enable);
        }
        else if(number == 3)
        {
            gameOver.text = GameText.Win_Title;
            reasonForLoss.text = GameText.Win_PointsReached;
            SetUpNextLevelButton();
            uiManager.PauseGame(enable);
        }
    }

    /// <summary>
    /// this is used to stop/play the world
    /// </summary>
    /// <param name="value"></param>
    public void TimeScale(int value)
    {
        Time.timeScale = value;
    }

    /// <summary>
    /// this sets up the retry button when the player loses
    /// </summary>
    public void SetUpRetryButton()
    {
        retryButton.GetComponentInChildren<Text>().text = GameText.Lose_Retry;

        retryButton.onClick.RemoveAllListeners();
        TimeScale(1);
        retryButton.onClick.AddListener(() => GameManager.retryLevel?.Invoke());
    }

    /// <summary>
    /// this sets up the next level button when the player wins
    /// </summary>
    public void SetUpNextLevelButton()
    {
        retryButton.GetComponentInChildren<Text>().text = GameText.Win_LoadNext;

        retryButton.onClick.RemoveAllListeners();
        TimeScale(1);
        retryButton.onClick.AddListener(LoadNext);
    }

    /// <summary>
    /// loads the next level, or loads end screen menu
    /// </summary>
    public void LoadNext()
    {
        uiManager.sceneLoadingOperation.levelLoadingScreen.ShowScreen(true);
        TimeScale(1);
        uiManager.sceneLoadingOperation.SetUp(SceneManager.GetActiveScene().buildIndex + 1); // get current build index and plus 1 to it

    }

    /// <summary>
    /// is called when the player reaches 140 points
    /// </summary>
    public void CheckToWinLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) // if scene has index that is not 2
        {
            if (uiManager.stats.playerScore >= 200) // if score is 200 or above show win menu
            {
                ShowWinLoseMenu(true, 3); // show win menu with winning description
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) // if scene has index of 2
        {
            if (uiManager.stats.playerScore >= 200) // if score is 200 or above show win menu
            {
                uiManager.endScreen.ShowEndScreenUI(true); // shows end screen
            }
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

    /// <summary>
    /// sets up the pause menu and disables it
    /// </summary>
    /// <param name="current"></param>
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

    /// <summary>
    /// a function to disable the pause menu used in an event
    /// </summary>
    public void Resume()
    {
        ShowPauseUI(false);
    }

    /// <summary>
    /// a function to enable the pause menu used in an event
    /// </summary>
    public void PauseGame()
    {
        ShowPauseUI(true);
    }

    /// <summary>
    /// enables/disables the pause menu
    /// </summary>
    /// <param name="Enable"></param>
    public void ShowPauseUI(bool Enable)
    {
        pauseMenu.SetActive(Enable);
    }
}
#endregion

#region EndScreen
[System.Serializable]
public class EndScreen
{
    #region public variables
    public GameObject endScreen;
    public Text title;
    public Text description;
    public Button replay;
    public Button returnToMenu;
    public Button quit;
    #endregion

    #region private variables
    private UIManager uiManager;
    #endregion

    /// <summary>
    /// sets up the end screen menu
    /// </summary>
    /// <param name="current"></param>
    public void Setup(UIManager current)
    {
        uiManager = current;
        ShowEndScreenUI(false);

        title.text = GameText.End_Title;
        description.text = GameText.End_Description;

        replay.GetComponentInChildren<Text>().text = GameText.End_Replay;
        returnToMenu.GetComponentInChildren<Text>().text = GameText.End_ReturnToMenu;
        quit.GetComponentInChildren<Text>().text = GameText.End_Quit;

        replay.onClick.RemoveAllListeners();
        replay.onClick.AddListener(() => GameManager.retryLevel?.Invoke());

        returnToMenu.onClick.RemoveAllListeners();
        returnToMenu.onClick.AddListener(() => GameManager.returnToMenu?.Invoke());

        quit.onClick.RemoveAllListeners();
        quit.onClick.AddListener(() => GameManager.quitGame?.Invoke());
    }

    /// <summary>
    /// enables/disables the end screen
    /// </summary>
    /// <param name="enable"></param>
    public void ShowEndScreenUI(bool enable)
    {
        endScreen.SetActive(enable);
    }
}
#endregion