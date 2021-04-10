using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    #region public variables
    public GameObject smallExplosionPrefab;
    #endregion

    #region private variables
    #endregion

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shell") // if the colliding object has tag shell
        {
            GameObject clone = Instantiate(smallExplosionPrefab, collision.gameObject.transform.position, smallExplosionPrefab.transform.rotation);
            Destroy(clone, 2);
            Destroy(collision.gameObject); // destroy object on collision
        }
    }
}