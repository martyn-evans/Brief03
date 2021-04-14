using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public KeyCode levelLoadButton = KeyCode.Space; // the button that needs to be pressed for loading to follow through
    public Text continueText;

    private AsyncOperation levelLoading; // a reference used to hold my async loading
    private Coroutine levelLoadingRoutine; // used to display level loading progress

    /// <summary>
    ///  Sets up the async loading
    /// </summary>
    public void SetUp(int scene)
    {
        levelLoading = SceneManager.LoadSceneAsync(scene); // this holds a reference to my current async operation so I can access it later
        levelLoading.allowSceneActivation = false; // this stops the scene from automatically switching
        TextSetUp();

        if (levelLoadingRoutine != null) // if routine is not null
        {
            StopCoroutine(levelLoadingRoutine); // stop current routine
        }
        levelLoadingRoutine = StartCoroutine(LoadLevelAsync()); // start new routine
    }

    /// <summary>
    /// sets up the load level text and disables the text object
    /// </summary>
    public void TextSetUp()
    {
        continueText.text = GameText.Continue_Text;
        continueText.enabled = false;
    }

    /// <summary>
    /// loads level to 0.89f, user has to press load level button to fully load scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadLevelAsync()
    {
        while(levelLoading.progress < 0.89f)
        {
            yield return null;
        }

        while (!levelLoading.allowSceneActivation)
        {
            continueText.enabled = true;
            if (Input.GetKeyDown(levelLoadButton))
            {
                levelLoading.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return null;
    }
}
