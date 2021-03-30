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

        if (other.transform.tag == "Player")
        {
            stats.resources.fuel.AddFuel(5);
            stats.playerScore += 5;
            stats.CheckStatPoint();
            stats.uiManager.skillMenu.UpdateSkillPointUI();
            stats.uiManager.inGameUI.UpdateScore();
            stats.uiManager.loseMenu.UpdateLoseMenuScore();
            Destroy(gameObject,2);
        }
    }
}