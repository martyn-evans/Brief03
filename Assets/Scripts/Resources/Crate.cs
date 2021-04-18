using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    #region public variables
    public GameObject smallExplosionPrefab; // a refernece to the explosion prefab
    public GameObject fuelItemPrefab; // a reference to the fuel pickup prefab
    public GameObject ammoItemPrefab; // a reference to the ammo pickup prefab

    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variables
    #endregion

    /// <summary>
    /// if the crate collides with certain objects it executes this function
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tag) // if object has same tag, it will return
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Object has same tag");
            }
            #endregion
            return;
            
        }

        if (collision.transform.tag == "Player" || collision.transform.tag == "Shell") // if object has tag of player or shell it proceeds
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Object has tag " + collision.transform.tag);
            }
            #endregion

            int coinFlip = Random.Range(0, 2);
            if(coinFlip == 0)
            {
                FuelDrop(); // calls fuel drop function

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Fuel dropped");
                }
                #endregion
            }
            else if(coinFlip == 1)
            {
                AmmoDrop(); // calls ammo drop function

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Ammo Dropped");
                }
                #endregion
            }

            if (collision.transform.tag == "Shell") // if the colliding object has tag shell
            {
                Destroy(collision.gameObject); // destroy object on collision
            }
        }
    }

    /// <summary>
    /// this creates a small explosion and drops fuel
    /// </summary>
    public void FuelDrop()
    {
        CrateExplosion(smallExplosionPrefab);
        DropItem(fuelItemPrefab);
    }

    /// <summary>
    /// this creates a small explosion and drops ammo
    /// </summary>
    public void AmmoDrop()
    {
        CrateExplosion(smallExplosionPrefab);
        DropItem(ammoItemPrefab);
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

    /// <summary>
    /// this drops either fuel or ammo from the crate
    /// </summary>
    /// <param name="Prefab"></param>
    public void DropItem(GameObject Prefab)
    {
        GameObject clone = Instantiate(Prefab, transform.position, Prefab.transform.rotation);
        Destroy(gameObject);
    }
}
