using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region public variables
    public Stats stats;
    public bool debuggingEnabled = false;
    #endregion

    #region private variables
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tag) // if object has same tag, it will return
        {
            return;
        }

        if (other.transform.tag == "Player") // if object has tag of Player
        {
            stats.resources.fuel.AddFuel(5); // add value to fuel
            stats.playerScore += 5; // add value to score
            stats.CheckStatPoint(); // checks stat points through function
            UpdateUI(); // calls update ui function
            Destroy(gameObject); // destroys object
            if (debuggingEnabled)
            {
                Debug.Log("Checkpoint Triggered");
            }
        }
    }

    /// <summary>
    /// updates skill, ingame, lose menu UI
    /// </summary>
    private void UpdateUI()
    {
        stats.uiManager.skillMenu.UpdateSkillPointUI(); // updates skill menu score UI
        stats.uiManager.inGameUI.UpdateScore(); // updates in games score UI
        stats.uiManager.loseMenu.UpdateLoseMenuScore(); // updates lose menu score UI
        if (debuggingEnabled)
        {
            Debug.Log("UI updated");
        }
    }
}