using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Stats stats;

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
            UpdateAllUI(); // calls update ui function
            Destroy(gameObject); // destroys object
        }
    }

    /// <summary>
    /// updates skill, ingame, lose menu UI
    /// </summary>
    private void UpdateAllUI()
    {
        stats.uiManager.skillMenu.UpdateSkillPointUI();
        stats.uiManager.inGameUI.UpdateScore();
        stats.uiManager.loseMenu.UpdateLoseMenuScore();
    }
}