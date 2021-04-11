using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickUp : MonoBehaviour
{
    #region public variables
    public float fuelValue = 5f; // amount of fuel player is given when picked up

    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variables
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            Stats.playerPickUp?.Invoke();
            collision.transform.root.GetComponent<Tank>().tankMovement.resources.fuel.AddFuel(fuelValue);
            Destroy(gameObject);

            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Fuel has been picked up");
            }
            #endregion
        }
    }
}