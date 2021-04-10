using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayLength;
    private float rotationSpeed;
    public bool isNight;

    private Light sun;

    private void Awake()
    {
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        rotationSpeed = Time.deltaTime / dayLength;
        transform.Rotate(0, rotationSpeed, 0);

        if(transform.localRotation.y > 270 && transform.localRotation.y < 90 || isNight)
        {
            isNight = true;
            sun.intensity = 0;
        }
        
        if(transform.localRotation.y > 90 && transform.localRotation.y < 270 || isNight == false)
        {
            isNight = false;
            sun.intensity = 1;
        }
    }
}
