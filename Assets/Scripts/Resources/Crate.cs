using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public LayerMask tankLayer;
    public GameObject smallExplosionPrefab;
    public GameObject fuelItemPrefab;
    public GameObject ammoItemPrefab;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            
            int coinFlip = Random.Range(0, 2);
            if(coinFlip == 0)
            {
                CrateExplosion(smallExplosionPrefab);
                DropItem(fuelItemPrefab);
                Debug.Log("Fuel Dropped");
            }
            else if(coinFlip == 1)
            {
                CrateExplosion(smallExplosionPrefab);
                DropItem(ammoItemPrefab);
                Debug.Log("Ammo Dropped");
            }
        }
    }

    /// <summary>
    /// this drops either fuel or ammo from the crate
    /// </summary>
    /// <param name="Prefab"></param>
    public void DropItem(GameObject Prefab)
    {
        GameObject clone = Instantiate(Prefab, transform.position, Prefab.transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// this spawns explosion prefab when create is destroyed
    /// </summary>
    /// <param name="Prefab"></param>
    public void CrateExplosion(GameObject Prefab)
    {
        GameObject clone = Instantiate(smallExplosionPrefab, transform.position, smallExplosionPrefab.transform.rotation);
        Destroy(clone, 2);
    }
}
