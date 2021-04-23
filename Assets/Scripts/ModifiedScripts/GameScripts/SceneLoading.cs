using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    #region public variables
    public KeyCode levelLoadButton = KeyCode.Space; // the button that needs to be pressed for loading to follow through
    public Text continueText; // a reference to the conetinue text object on the loading screen
    public Slider progressBar; // a reference to the loading bar slider on the loading screen
    public LevelLoadingScreen levelLoadingScreen;
    #endregion

    #region private variables
    private AsyncOperation levelLoading; // a reference used to hold my async loading
    private Coroutine levelLoadingRoutine; // used to display level loading progress

    private bool loadingDone;
    #endregion

    /// <summary>
    ///  Sets up the async loading
    /// </summary>
    public void SetUp(int scene)
    {
        loadingDone = false;
        levelLoading = SceneManager.LoadSceneAsync(scene); // this holds a reference to my current async operation so I can access it later
        levelLoading.allowSceneActivation = false; // this stops the scene from automatically switching

        if (levelLoadingRoutine != null) // if routine is not null
        {
            StopCoroutine(levelLoadingRoutine); // stop current routine
        }
        levelLoadingRoutine = StartCoroutine(LoadLevelAsync()); // start new routine
    }

    /// <summary>
    /// loads level to 0.89f, user has to press load level button to fully load scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadLevelAsync()
    {
        while (!loadingDone) // if loading is not done
        {
            float progress = Mathf.Clamp01(levelLoading.progress / 0.9f); // normalises the level loading progress
            progressBar.value = progress; // updates progress bar value

            if(progress >= 0.89f) // if progresss equals 0.89 or higher
            {
                loadingDone = true; // loading is true
            }

            yield return null;
        }

        yield return new WaitForSeconds(2); // waits for 2 seconds

        while(loadingDone) // if loading is done
        {
            continueText.text = GameText.Continue_Text; // updates continue text

            if (Input.GetKeyDown(levelLoadButton))
            {
                levelLoading.allowSceneActivation = true; // activates scene activation
            }
            yield return null;
        }
        yield return null;
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
    public Text goalText;
    public Text goal;
    public List<string> tipStringList = new List<string>();
    #endregion

    #region private variables
    private MainMenuUI m_MainMenuUI;
    private UIManager m_UIManager;
    #endregion

    /// <summary>
    /// sets up the loading screen
    /// </summary>
    /// <param name="mainMenuUI"></param>
    public void SetUpMainMenuLoading(MainMenuUI mainMenuUI)
    {
        m_MainMenuUI = mainMenuUI;

        goalText.text = GameText.Goal_Text;
        goal.text = GameText.Goal_Description;
        titleText.text = GameText.Loading_Title;
        tipText.text = GameText.TipWord_Text;

        SetUpStringList();
        tip.text = PickRandomTip();
    }

    /// <summary>
    /// sets up the loading screen fo
    /// </summary>
    /// <param name="uiManager"></param>
    public void SetUpInGameLoading(UIManager uiManager)
    {
        m_UIManager = uiManager;

        goalText.text = GameText.Goal_Text;
        goal.text = GameText.Goal_Description;
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
        tipStringList.Add(GameText.TipEleven_Text);
        tipStringList.Add(GameText.TipTwelve_Text);
        tipStringList.Add(GameText.TipThirteen_Text);
        tipStringList.Add(GameText.TipFourteen_Text);
        tipStringList.Add(GameText.TipFifteen_Text);
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
