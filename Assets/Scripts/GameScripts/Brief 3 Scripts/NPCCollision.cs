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

    /// <summary>
    /// is called if the NPC tank collides with objects with certain tags
    /// </summary>
    /// <param name="collision"></param>
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

        if (collision.transform.tag == "Player") // if object has tag of player or shell it proceeds
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Object has tag " + collision.transform.tag);
            }
            #endregion
            TankGameEvents.OnObjectTakeDamageEvent?.Invoke(collision.transform,-100f);
        }
    }
}
