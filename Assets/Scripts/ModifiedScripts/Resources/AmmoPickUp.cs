using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    #region public variables
    public int ammoValue = 3; // amount of ammo player is given when picked up

    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variables
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            Stats.playerPickUp?.Invoke();
            collision.transform.root.GetComponent<Tank>().tankMovement.resources.ammo.AddAmmo(ammoValue);
            Destroy(gameObject);

            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Ammo has been picked up");
            }
            #endregion
        }
    }
}
