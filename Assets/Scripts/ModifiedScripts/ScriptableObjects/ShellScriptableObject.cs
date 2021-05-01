using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Type", menuName = "ScriptableObjects/DefaultShell", order = 1)]

public class ShellScriptableObject : ScriptableObject
{
    #region public variables
    public enum ShellType {Single, Double, Triple, Shotgun }; // enum of shell types
    public ShellType currentShellType; // current shell type
    public GameObject shellPrefab; // a refernce to th shell prefab
    #endregion

    #region private variables
    private float shellForce; // force of shell out of main gun
    #endregion

    /// <summary>
    /// Fires Single, Double, Triple shells out of main gun
    /// </summary>
    /// <param name="SpawnPoint"></param>
    /// <param name="ShellForce"></param>
    public void Fire(Transform SpawnPoint, float ShellForce)
    {
        shellForce = ShellForce;

        switch(currentShellType)
        {
            case ShellType.Single:
                {
                    Single(SpawnPoint, Vector3.zero);
                    break;
                }
            case ShellType.Double:
                {
                    Double(SpawnPoint);
                    break;
                }
            case ShellType.Triple:
                {
                    Triple(SpawnPoint);
                    break;
                }
            case ShellType.Shotgun:
                {
                    Shotgun(SpawnPoint);
                    break;
                }
        }
    }

    /// <summary>
    /// one shell is shot out of main gun
    /// </summary>
    /// <param name="SpawnPoint"></param>
    /// <param name="OffSet"></param>
    void Single(Transform SpawnPoint, Vector3 OffSet)
    {
        // spawns in a tank shell at the main gun transform and matches the rotation of the main gun and stores it in the clone GameObject variable
        GameObject clone = Object.Instantiate(shellPrefab, SpawnPoint.position + OffSet, SpawnPoint.rotation);

        // If the clone has a rigidbody, we want to add some velocity to it to make it fire!
        if (clone.GetComponent<Rigidbody>())
        {
            clone.GetComponent<Rigidbody>().velocity = shellForce * SpawnPoint.forward; // make the velocity of our bullet go in the direction of our gun at the launch force
        }
        Object.Destroy(clone, 5f);
    }

    /// <summary>
    /// two shells are shot out of main gun
    /// </summary>
    /// <param name="SpawnPoint"></param>
    void Double(Transform SpawnPoint)
    {
        Single(SpawnPoint, new Vector3(1f, 0, 0));
        Single(SpawnPoint, new Vector3(-1f, 0, 0));
    }

    /// <summary>
    /// three shells are shot out of main gun
    /// </summary>
    /// <param name="SpawnPoint"></param>
    void Triple(Transform SpawnPoint)
    {
        Single(SpawnPoint, Vector3.zero);
        Single(SpawnPoint, new Vector3(2, 0, 0));
        Single(SpawnPoint, new Vector3(-2, 0, 0));
    }

    void SlugSingle(Transform SpawnPoint, float angle)
    {
        // spawns in a tank shell at the main gun transform and matches the rotation of the main gun and stores it in the clone GameObject variable
        GameObject clone = Object.Instantiate(shellPrefab, SpawnPoint.position, Quaternion.Euler(SpawnPoint.rotation.x, angle, SpawnPoint.rotation.z));

        // If the clone has a rigidbody, we want to add some velocity to it to make it fire!
        if (clone.GetComponent<Rigidbody>())
        {
            clone.GetComponent<Rigidbody>().velocity = shellForce * SpawnPoint.forward; // make the velocity of our bullet go in the direction of our gun at the launch force
        }
        Object.Destroy(clone, 5f);
    }

    void Shotgun(Transform SpawnPoint)
    {
        SlugSingle(SpawnPoint, 0);
        SlugSingle(SpawnPoint, 90);
        SlugSingle(SpawnPoint, 180);
        SlugSingle(SpawnPoint, 270);
    }
}
