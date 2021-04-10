using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    #region public variances
    public int statPoint = 0; // the value for the players stat points
    public int playerScore = 0; // the value for the players score
    public int pointThreshold = 10; // the value for the point threshold
    public int upgradeFuelLevel = 0; // the upgrade level of fuel upgrades
    public int upgradeAmmoLevel = 0; // the upgrade level of ammo upgrades
    public int upgradeSpeedLevel = 0; // the upgrade level of turret speed upgrades

    public Resources resources; // reference for resources script
    public TankMovement turret; // reference for our tank movement script
    public UIManager uiManager; // reference for our ui manager

    public static UnityEvent playerPickUp = new UnityEvent(); // event for when a player picks up items
    public static UnityEvent upgradeFuel = new UnityEvent(); // event for upgrading fuel
    public static UnityEvent upgradeAmmo = new UnityEvent(); // event for upgrading ammo
    public static UnityEvent upgradeTurret = new UnityEvent(); // event for upgrading turret

    public bool debuggingEnabled = false; // enables/disables debugging
    #endregion

    #region private variances
    #endregion

    private void OnEnable()
    {
        upgradeFuel.AddListener(UpgradeFuel);
        upgradeAmmo.AddListener(UpgradeAmmo);
        upgradeTurret.AddListener(UpgradeTurretSpeed);
        playerPickUp.AddListener(ItemPickUpBonus);
    }

    private void OnDisable()
    {
        upgradeFuel.RemoveListener(UpgradeFuel);
        upgradeAmmo.RemoveListener(UpgradeAmmo);
        upgradeTurret.RemoveListener(UpgradeTurretSpeed);
        playerPickUp.RemoveListener(ItemPickUpBonus);
    }

    /// <summary>
    /// checks xp points, gives a stat point if xp is over the threshold, doubles threshold
    /// </summary>
    public void CheckStatPoint()
    {
        if (playerScore >= pointThreshold)
        {
            statPoint += 1; // adds a stat point
            uiManager.inGameUI.ShowUpgradeTextUI(true);
            uiManager.skillMenu.UpdateSkillPointUI();
            pointThreshold *= 2; // doubles the point threshold

            if (debuggingEnabled)
            {
                Debug.Log("Stat points = " + statPoint);
                Debug.Log("Point Threshold is " + pointThreshold);
            }
        }
    }

    /// <summary>
    /// gives a point for item pickups e.g. crates
    /// </summary>
    public void ItemPickUpBonus()
    {
        playerScore += 1; // adds a point to the player score
        CheckStatPoint(); // calls check stat point function
        uiManager.inGameUI.UpdateScore();
        uiManager.loseMenu.UpdateLoseMenuScore();

        if (debuggingEnabled)
        {
            Debug.Log("Player score = " + playerScore);
        }
    }

    /// <summary>
    /// upgrade for fuel, increases max fuel by 2 and adds 2 units of fuel
    /// </summary>
    public void UpgradeFuel()
    {
        if(statPoint > 0) // if stat points are greater than 0
        {
            upgradeFuelLevel += 1; // upgrade level increases by 1
            resources.fuel.maxFuel += 2; // increases max fuel by 2
            resources.fuel.AddFuel(2); // adds 2 units of fuel to players tank
            statPoint -= 1; // stat point decreases by 1
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateFuelUI(resources.fuel.CurrentFuel);

            if(statPoint == 0) // if stat points equal zero, disable upgrade available text
            {
                uiManager.inGameUI.ShowUpgradeTextUI(false);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// upgrade for ammo, increases max ammo by 2 and gives 2 ammo
    /// </summary>
    public void UpgradeAmmo()
    {
        if(statPoint > 0) // if stat points are greater than 0
        {
            upgradeAmmoLevel += 1; // upgrade ammo level by 1
            resources.ammo.ammoValue += 2; // adds 2 ammo to players tank
            resources.ammo.maxAmmoValue += 2; // increases max ammo by 2
            statPoint -= 1; // stat point decreases by 1
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateAmmoUI(resources.ammo.ammoValue, resources.ammo.maxAmmoValue);

            if (statPoint == 0) // if stat points equal zero, disable upgrade available text
            {
                uiManager.inGameUI.ShowUpgradeTextUI(false);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// upgrade for the turret speed, increases it by 2.5 degrees a second
    /// </summary>
    public void UpgradeTurretSpeed()
    {
        if (statPoint > 0) // if stat points are greater than 0
        {
            upgradeSpeedLevel += 1; // upgrade turret level by 1
            turret.turretTurnSpeed += 2.5f; // increases turret speed by 2.5 degrees a second
            statPoint -= 1; // stat point decreases by 1
            uiManager.skillMenu.UpdateSkillPointUI();

            if (debuggingEnabled)
            {
                Debug.Log("turret speed is " + turret.turretTurnSpeed);
            }

            if (statPoint == 0) // if stat points equal zero, disable upgrade available text
            {
                uiManager.inGameUI.ShowUpgradeTextUI(false);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// updates the score on the lose menu
    /// </summary>
    public void UpdateScore()
    {
        uiManager.loseMenu.UpdateLoseMenuScore();
    }
}