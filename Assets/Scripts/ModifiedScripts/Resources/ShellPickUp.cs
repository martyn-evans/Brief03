using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPickUp : MonoBehaviour
{
    #region public variables
    public ShellScriptableObject singleShellType;
    public ShellScriptableObject doubleShellType;
    public ShellScriptableObject tripleShellType;
    public ShellScriptableObject shotgunShellType;
    public Tank playerTank;
    public int chance;

    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variables
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.GetComponent<Tank>())
        {
            playerTank = collision.transform.root.GetComponent<Tank>();
            chance = Random.Range(0, 6);

            if (chance <=2)
            {
                playerTank.tankMainGun.shellType = singleShellType;

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Single Shell has been picked up");
                }
                #endregion
            }
            
            if (chance == 3)
            {
                playerTank.tankMainGun.shellType = doubleShellType;

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Double Shell has been picked up");
                }
                #endregion
            }

            if (chance == 4)
            {
                playerTank.tankMainGun.shellType = tripleShellType;

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Triple Shell has been picked up");
                }
                #endregion
            }

            if (chance == 6)
            {
                playerTank.tankMainGun.shellType = shotgunShellType;

                #region debugging
                if (debuggingEnabled)
                {
                    Debug.Log("Shotgun Shell has been picked up");
                }
                #endregion
            }
        }
        Destroy(gameObject);
    }
}
