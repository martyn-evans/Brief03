using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Stats stat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tag) // if object has same tag, it will return
        {
            return;
        }

        if (other.transform.tag == "Player")
        {
            stat.resources.fuel.AddFuel(5);
            stat.points.AddPointsCheckpoint();
            stat.CheckStatPoint();
            stat.uiManager.skillMenu.UpdateSkillPointUI();
            Destroy(gameObject,2);
        }
    }
}
