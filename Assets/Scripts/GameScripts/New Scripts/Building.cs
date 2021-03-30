using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Stats stats;
    public GameObject explosionPrefab;
    public GameObject buildingDebris;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Shell")
        {
            stats.playerScore += 2;
            BuildingExplosion(explosionPrefab, collision);
            stats.uiManager.loseMenu.UpdateLoseMenuScore();
            stats.uiManager.inGameUI.UpdateScore();
            Destroy(collision.gameObject); // destroy object on collision
            DebrisSpawn(buildingDebris);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// this spawns explosion prefab when create is destroyed
    /// </summary>
    /// <param name="Prefab"></param>
    public void BuildingExplosion(GameObject Prefab, Collision collision)
    {
        GameObject clone = Instantiate(explosionPrefab, collision.transform.position, explosionPrefab.transform.rotation);
        Destroy(clone, 2);
    }

    public void DebrisSpawn(GameObject Prefab)
    {
        GameObject clone = Instantiate(buildingDebris, transform.position, buildingDebris.transform.rotation);
    }
}
