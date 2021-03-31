using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public Ammo ammo; // a reference to the ammo class
    public Fuel fuel; // a reference to the fuel class
    private UIManager uiManager;  // a reference to the UIManager script

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        ammo.SetUp(uiManager);
        fuel.SetUp(uiManager);
    }
    public void Update()
    {
        if(fuel.CurrentFuel <= 0)
        {
            uiManager.loseMenu.ShowLoseMenu(enabled);
        }
    }
}

/// <summary>
/// This region is used for ammo
/// </summary>
[System.Serializable]
public class Ammo
{
    public int ammoValue;
    public int maxAmmoValue;

    private UIManager uiManager; // a reference to the UIManager script

    /// <summary>
    /// sets up ammo value for the game
    /// </summary>
    public void SetUp(UIManager current)
    {
        uiManager = current;
        ammoValue = 10;
        maxAmmoValue = 10;
        Debug.Log("Bullets left " + ammoValue);
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
    }

    /// <summary>
    /// takes a single ammo away
    /// </summary>
    public void MinusAmmo()
    {
        ammoValue -= 1;
        uiManager.inGameUI.UpdateAmmoUI(ammoValue, maxAmmoValue);
    }
}

/// <summary>
/// This region is used for fuel
/// </summary>
[System.Serializable]
public class Fuel
{
    private float currentFuel = 15;
    public float maxFuel = 20;

    private UIManager uiManager; // a reference to the UIManager script

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
    }

    /// <summary>
    /// minus fuel of a certain value
    /// </summary>
    /// <param name="amount"></param>
    public void UseFuel()
    {
        CurrentFuel -= 0.01f;
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
