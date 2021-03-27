using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickUp : MonoBehaviour
{
    public float fuelValue = 5f; // amount of fuel player is given when picked up

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            GameManager.playerPickUp?.Invoke();
            collision.transform.root.GetComponent<Tank>().tankMovement.resources.fuel.AddFuel(fuelValue);
            Destroy(gameObject);
        }
    }
}