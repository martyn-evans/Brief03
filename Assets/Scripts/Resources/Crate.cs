using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    #region public variables
    public LayerMask tankLayer;
    public GameObject smallExplosionPrefab;
    public GameObject fuelItemPrefab;
    public GameObject ammoItemPrefab;

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
            if(debuggingEnabled)
            {
                Debug.Log("Object has same tag");
            }
            return;
        }

        //if (collision.transform.root.GetComponent<Tank>() || collision.gameObject && gameObject.tag == "Shell")

        if(collision.transform.tag == "Player" || collision.transform.tag == "Shell") // if object has tag of player or shell it proceeds
        {
            if (debuggingEnabled)
            {
                Debug.Log("Object has tag " + collision.transform.tag);
            }

            int coinFlip = Random.Range(0, 2);
            if(coinFlip == 0)
            {
                FuelDrop(); // calls fuel drop function

                if (debuggingEnabled)
                {
                    Debug.Log("Fuel dropped");
                }
            }
            else if(coinFlip == 1)
            {
                AmmoDrop(); // calls ammo drop function

                if (debuggingEnabled)
                {
                    Debug.Log("Ammo Dropped");
                }
            }

            if(collision.transform.tag == "Shell") // if the colliding object has tag shell
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
