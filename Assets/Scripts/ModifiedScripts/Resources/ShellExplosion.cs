using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    #region public variables
    public GameObject explosionPrefab; // the explosion we want to spawn in
    #endregion

    #region private varaibles
    #endregion


    // is called when the trigger hits an object
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == transform)
        {
            return;
        }
        else
        {
            GameObject clone = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            Destroy(clone, 2f);
        }
    }
}
