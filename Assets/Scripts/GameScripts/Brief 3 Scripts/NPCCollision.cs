using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    #region public variables


    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variables
    #endregion

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tag) // if object has same tag, it will return
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Object has same tag");
            }
            #endregion
            return;
        }

        //if (collision.transform.root.GetComponent<Tank>() || collision.gameObject && gameObject.tag == "Shell")

        if (collision.transform.tag == "Player") // if object has tag of player or shell it proceeds
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Object has tag " + collision.transform.tag);
            }
            #endregion

            TankGameEvents.OnObjectTakeDamageEvent?.Invoke(collision.transform,-20f);

            if (collision.transform.tag == "Shell") // if the colliding object has tag shell
            {
                Destroy(collision.gameObject); // destroy object on collision
            }
        }
    }
}
