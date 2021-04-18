using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    #region public variables
    public MainMenu mainMenu; // a reference to a new instance of the main menu data class
    public CreditsMenu creditMenu; // a reference to a new instance of the credits menu data class
    public SceneLoading sceneLoadingOperation;
    public LevelLoadingScreen loadingScreen;
    #endregion

    #region private variables
    #endregion

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        mainMenu.Setup(this); // sets up the main menu reference
        creditMenu.Setup(this); // sets up the credit menu reference
        loadingScreen.Setup(this);
                                
        mainMenu.ShowScreen(true); // display the main menu
        creditMenu.ShowScreen(false); // hides the credits menu
        loadingScreen.ShowScreen(false);
    }

    /// <summary>
    /// function that can show/hide main menu
    /// </summary>
    /// <param name="ShowScreen"></param>
    public void ShowMainMenu(bool ShowScreen)
    {
        mainMenu.ShowScreen(ShowScreen);
    }

    /// <summary>
    /// function that can show/hide the credits
    /// </summary>
    /// <param name="ShowScreen"></param>
    public void ShowCredits(bool ShowScreen)
    {
        creditMenu.ShowScreen(ShowScreen);
    }

    /// <summary>
    /// function that can show/hide the loading screen
    /// </summary>
    /// <param name="ShowScreen"></param>
    public void ShowLoadingScreen(bool ShowScreen)
    {
        loadingScreen.ShowScreen(ShowScreen);
    }
}

[System.Serializable]
public class MainMenu
{
    #region public variables 
    public GameObject mainMenuScreen; // a reference to the main menu UI object
    public Text title; // a reference to the title text
    public Button playGameButton; // a reference to the play game button
    public Button creditsButton; // a reference to the credits button
    public Button quitButton; // a reference to the quit button
    #endregion

    #region private variables
    private MainMenuUI m_MainMenuUI;
    #endregion

    /// <summary>
    /// Sets up the fields for this screen
    /// </summary>
    public void Setup(MainMenuUI mainMenuUI)
    {
        m_MainMenuUI = mainMenuUI;
        // sets up the text for my buttons
        title.text = GameText.MainMenu_Title;
        playGameButton.GetComponentInChildren<Text>().text = GameText.MainMenu_PlayGame;
        creditsButton.GetComponentInChildren<Text>().text = GameText.MainMenu_Credits;
        quitButton.GetComponentInChildren<Text>().text = GameText.MainMenu_Quit;

        // set up the functions fo each of my buttons
        // remove all the functions on the button already, and add my own
        playGameButton.onClick.RemoveAllListeners();
        playGameButton.onClick.AddListener(PlayGame);

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(CreditsMenu);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame);
    }

    /// <summary>
    /// displays this screen on and off
    /// </summary>
    /// <param name="displayScreen"></param>
    public void ShowScreen(bool displayScreen)
    {
        mainMenuScreen.SetActive(displayScreen);
    }

    /// <summary>
    /// loads the main scene
    /// </summary>
    private void PlayGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // just get the next scene in the build index
        m_MainMenuUI.ShowLoadingScreen(true);
        m_MainMenuUI.sceneLoadingOperation.SetUp(1);
        m_MainMenuUI.ShowMainMenu(false);
        m_MainMenuUI.ShowCredits(false);
    }

    /// <summary>
    /// opens the credits menu
    /// </summary>
    private void CreditsMenu()
    {
        ShowScreen(false); // hides the main menu
        m_MainMenuUI.ShowCredits(true); // displays credits
    }

    /// <summary>
    /// Main Menu quit game function
    /// </summary>
    private void QuitGame()
    {
        GameManager.quitGame?.Invoke();
    }
}

[System.Serializable]
public class CreditsMenu
{
    #region public variables
    public GameObject creditsMenuScreen;  // a reference to the credits menu object
    public Text title;  // a reference to the title text
    public Text creditText;  // a reference to the credit text
    public Text creditDeveloper;  // a reference to the developer text
    public Button backButton;  // a reference to the back button
    #endregion

    #region private variables
    private MainMenuUI m_MainMenuUI;
    #endregion

    /// <summary>
    /// Sets up the fields for this screen
    /// </summary>
    public void Setup(MainMenuUI mainMenuUI)
    {
        m_MainMenuUI = mainMenuUI;
        // sets up the text for my buttons
        title.text = GameText.Credits_Title;
        creditText.text = GameText.Credits_MadeBy;
        creditDeveloper.text = GameText.Credits_Developer;
        backButton.GetComponentInChildren<Text>().text = GameText.Credits_Back;

        // set up the functions for each of my buttons
        // remove all the functions on the button already, and add my own
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(Back);
    }

    /// <summary>
    /// displays this screen on and off
    /// </summary>
    /// <param name="displayScreen"></param>
    public void ShowScreen(bool displayScreen)
    {
        creditsMenuScreen.SetActive(displayScreen);
    }

    /// <summary>
    /// the credits back button function
    /// </summary>
    private void Back()
    {
        ShowScreen(false); // hide the credits
        m_MainMenuUI.ShowMainMenu(true); // displays the main menu
    }
}

[System.Serializable]
public class LevelLoadingScreen
{
    #region public variables
    public GameObject loadingScreen;
    public Text titleText;
    public Text tipText;
    public Text tip;
    public List<string> tipStringList = new List<string>();
    #endregion

    #region private variables
    private MainMenuUI m_MainMenuUI;
    #endregion

    /// <summary>
    /// sets up the loading screen
    /// </summary>
    /// <param name="mainMenuUI"></param>
    public void Setup(MainMenuUI mainMenuUI)
    {
        m_MainMenuUI = mainMenuUI;

        titleText.text = GameText.Loading_Title;
        tipText.text = GameText.TipWord_Text;
        SetUpStringList();
        tip.text = PickRandomTip();
    }

    /// <summary>
    /// enables/disables the loading screen
    /// </summary>
    /// <param name="displayScreen"></param>
    public void ShowScreen(bool displayScreen)
    {
        loadingScreen.SetActive(displayScreen);
    }

    /// <summary>
    /// adds game text to the list
    /// </summary>
    public void SetUpStringList()
    {
        tipStringList.Add(GameText.TipOne_Text);
        tipStringList.Add(GameText.TipTwo_Text);
        tipStringList.Add(GameText.TipThree_Text);
        tipStringList.Add(GameText.TipFour_Text);
        tipStringList.Add(GameText.TipFive_Text);
        tipStringList.Add(GameText.TipSix_Text);
        tipStringList.Add(GameText.TipSeven_Text);
        tipStringList.Add(GameText.TipEight_Text);
        tipStringList.Add(GameText.TipNine_Text);
        tipStringList.Add(GameText.TipTen_Text);
    }

    /// <summary>
    /// picks a random tip text from the list
    /// </summary>
    /// <returns></returns>
    public string PickRandomTip()
    {
        int randomTip = Random.Range(0, tipStringList.Count);
        return tipStringList[randomTip];
    }
}