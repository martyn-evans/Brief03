using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    #region public variances
    public Ammo ammo; // a reference to the ammo class
    public Fuel fuel; // a reference to the fuel class

    public bool debuggingEnable = false; // enables/disables debugging
    #endregion

    #region private variances
    private UIManager uiManager;  // a reference to the UIManager script
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        ammo.SetUp(uiManager);
        fuel.SetUp(uiManager);
    }
    public void Update()
    {
        if(fuel.CurrentFuel <= 0) // if fuel hits 0, player loses and displays lose menu
        {
            uiManager.loseMenu.ShowWinLoseMenu(enabled,1);

            if (debuggingEnable)
            {
                Debug.Log("Player ran out of fuel");
            }
        }

        if(debuggingEnable) // enables/disables debugging for fuel/ammo
        {
            ammo.debuggingEnable = true;
            fuel.debuggingEnable = true;
        }
        else
        {
            ammo.debuggingEnable = false;
            fuel.debuggingEnable = false;
        }
    }
}

/// <summary>
/// This region is used for ammo
/// </summary>
[System.Serializable]
public class Ammo
{
    #region public variances
    public int ammoValue;
    public int maxAmmoValue;

    public bool debuggingEnable = false; // enables/disables debugging
    #endregion

    #region private variances
    private UIManager uiManager;  // a reference to the UIManager script
    #endregion

    /// <summary>
    /// sets up ammo value for the game
    /// </summary>
    public void SetUp(UIManager current)
    {
        uiManager = current;
        ammoValue = 10;
        maxAmmoValue = 10;

        if(debuggingEnable)
        {
            Debug.Log("Bullets left " + ammoValue);
        }
        uiManager.inGameUI.UpdateAmmoUI(ammoValue, maxAmmoValue);
    }

    /// <summary>
    /// adds ammo of a certain value
    /// </summary>
    /// <param name="amount"></param>
    public void AddAmmo(int amount)
    {
        ammoValue += amount;
        ammoValue = Mathf.Clamp(ammoValue, 0, maxAmmoValue);
        uiManager.inGameUI.UpdateAmmoUI(ammoValue, maxAmmoValue);

        if (debuggingEnable)
        {
            Debug.Log("Ammo added");
        }
    }

    /// <summary>
    /// takes a single ammo away
    /// </summary>
    public void MinusAmmo()
    {
        ammoValue -= 1;
        uiManager.inGameUI.UpdateAmmoUI(ammoValue, maxAmmoValue);

        if (debuggingEnable)
        {
            Debug.Log("Ammo decreased");
        }
    }
}

/// <summary>
/// This region is used for fuel
/// </summary>
[System.Serializable]
public class Fuel
{
    #region public variables
    private float currentFuel = 15; // the value of the current fuel
    public float maxFuel = 20; // the value of the max fuel

    public bool debuggingEnable = false; // enables/disables debugging
    #endregion

    #region private variances
    private UIManager uiManager;  // a reference to the UIManager script
    #endregion

    /// <summary>
    /// sets up ammo value for the game
    /// </summary>
    public void SetUp(UIManager current)
    {
        uiManager = current;
        CurrentFuel = currentFuel;
    }

    /// <summary>
    /// adds fuel of a certain value
    /// </summary>
    /// <param name="amount"></param>
    public void AddFuel(float amount)
    {
        CurrentFuel += amount;

        if (debuggingEnable)
        {
            Debug.Log("Fuel added");
        }
    }

    /// <summary>
    /// minus fuel of a certain value
    /// </summary>
    /// <param name="amount"></param>
    public void UseFuel(float value)
    {
        CurrentFuel -= value;

        if (debuggingEnable)
        {
            Debug.Log("Player is using fuel");
        }
    }

    /// <summary>
    /// is our current fuel value
    /// </summary>
    public float CurrentFuel
    {
        get
        {
            return currentFuel;
        }
        set
        {
            currentFuel = Mathf.Clamp(value, 0, maxFuel);
            uiManager.inGameUI.UpdateFuelUI(CurrentFuel / maxFuel); // normalise our fuel value
        }
    }
}
