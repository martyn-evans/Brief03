using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public MainMenu mainMenu; // a reference to a new instance of the main menu data class
    public CreditsMenu creditMenu; // a reference to a new instance of the credits menu data class
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // sets up the screen reference
        mainMenu.Setup(this);
        creditMenu.Setup(this);
        // display the main menu and then hide the credits menu
        creditMenu.ShowScreen(false);
        mainMenu.ShowScreen(true);
    }

    public void ShowMainMenu(bool ShowScreen)
    {
        mainMenu.ShowScreen(ShowScreen);
    }

    public void ShowCredits(bool ShowScreen)
    {
        creditMenu.ShowScreen(ShowScreen);
    }
}

[System.Serializable]
public class MainMenu
{
    public GameObject mainMenuScreen;

    public Text title;
    public Button playGameButton;
    public Button creditsButton;
    public Button quitButton;
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
        // quitButton.onClick.AddListener(mainMenuUI.gameManager.QuitGame);
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
        // just get the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// opens the credits menu
    /// </summary>
    private void CreditsMenu()
    {
        //hides the main menu
        ShowScreen(false);
        // displays credits
        m_MainMenuUI.ShowCredits(true);
    }
}


[System.Serializable]
public class CreditsMenu
{
    public GameObject creditsMenuScreen;
    public Text title;
    public Text creditText;
    public Text creditDeveloper;
    public Button backButton;
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

    private void Back()
    {
        // hide the credits
        ShowScreen(false);
        // displays the main menu
        m_MainMenuUI.ShowMainMenu(true);
    }
}