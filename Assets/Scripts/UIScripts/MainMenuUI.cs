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
    #endregion

    #region private variables
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.Setup(this); // sets up the main menu reference
        creditMenu.Setup(this); // sets up the credit menu reference
                                
        mainMenu.ShowScreen(true); // display the main menu
        creditMenu.ShowScreen(false); // hides the credits menu
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
}

[System.Serializable]
public class MainMenu
{
    public GameObject mainMenuScreen; // a reference to the main menu UI object

    public Text title; // a reference to the title text
    public Button playGameButton; // a reference to the play game button
    public Button creditsButton; // a reference to the credits button
    public Button quitButton; // a reference to the quit button
    private MainMenuUI m_MainMenuUI;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // just get the next scene in the build index
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
    public GameObject creditsMenuScreen;  // a reference to the credits menu object
    public Text title;  // a reference to the title text
    public Text creditText;  // a reference to the credit text
    public Text creditDeveloper;  // a reference to the developer text
    public Button backButton;  // a reference to the back button
    private MainMenuUI m_MainMenuUI;

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