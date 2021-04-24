using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    #region public variables
    public Stats stats; // a reference to our stats script
    public GameObject explosionPrefab; // the explosion prefab
    public GameObject buildingDebris; // the building debris prefab

    public bool debuggingEnabled = false;
    #endregion

    #region private variables
    #endregion

    private void Awake()
    {
        stats = FindObjectOfType<Stats>(); // finds stats script and assigns it to this variable
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Shell")
        {
            stats.playerScore += 2; // add two to player score
            BuildingExplosion(collision); // explode building
            stats.CheckStatPoint();
            UpdateUI();
            Destroy(collision.gameObject); // destroy object on collision
            DebrisSpawn();
            Destroy(gameObject); // destroy building
        }
    }

    /// <summary>
    /// this spawns explosion prefab when create is destroyed
    /// </summary>
    /// <param name="Prefab"></param>
    public void BuildingExplosion(Collision collision)
    {
        GameObject clone = Instantiate(explosionPrefab, collision.transform.position, explosionPrefab.transform.rotation);
        Destroy(clone, 2);
    }

    /// <summary>
    /// spawns debris when building is destroyed
    /// </summary>
    /// <param name="Prefab"></param>
    public void DebrisSpawn()
    {
        Instantiate(buildingDebris, transform.position, buildingDebris.transform.rotation);

        if (debuggingEnabled)
        {
            Debug.Log("Debris Spawned");
        }
    }

    /// <summary>
    /// updates score in UI
    /// </summary>
    public void UpdateUI()
    {
        stats.uiManager.loseMenu.UpdateLoseMenuScore(); // update lose menu UI
        stats.uiManager.inGameUI.UpdateScore(); // update ingame UI

        if (debuggingEnabled)
        {
            Debug.Log("UI updated");
        }
    }
}
