using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ammoValue = 3; // amount of ammo player is given when picked up

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            Stats.playerPickUp?.Invoke();
            collision.transform.root.GetComponent<Tank>().tankMovement.resources.ammo.AddAmmo(ammoValue);
            Destroy(gameObject);
        }
    }
}
