using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    #region public variances
    public float dayLength; // a value for the length of the day
    #endregion

    #region private variables
    private float rotationSpeed; // a value of the rotation speed of the light object
    #endregion


    // Update is called once per frame
    void Update()
    {
        rotationSpeed = Time.deltaTime / dayLength; // is a value that changes each second
        transform.Rotate(0, rotationSpeed, 0); // rotates the transform on the z axis by the value above
    }
}
