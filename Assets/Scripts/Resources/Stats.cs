using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public int statPoint = 0;
    public int pointThreshold = 10;
    public int upgradeFuelLevel = 0;
    public int upgradeAmmoLevel = 0;
    public int upgradeSpeedLevel = 0;

    public Points points;
    public Resources resources;
    public UIManager uiManager;

    public static UnityEvent upgradeFuel = new UnityEvent();
    public static UnityEvent upgradeAmmo = new UnityEvent();
    // public static UnityEvent upgradeTurret = new UnityEvent();

    private void OnEnable()
    {
        upgradeFuel.AddListener(UpgradeFuel);
        upgradeAmmo.AddListener(UpgradeAmmo);
    }

    private void OnDisable()
    {
        upgradeFuel.RemoveListener(UpgradeFuel);
        upgradeAmmo.RemoveListener(UpgradeAmmo);
    }

    public void CheckStatPoint()
    {
        if (points.playerPoints >= pointThreshold)
        {
            statPoint += 1;
            uiManager.skillMenu.UpdateSkillPointUI();
            pointThreshold *= 2;
        }
    }

    public void ResetGame()
    {
        statPoint = 0;
    }

    public void UpgradeFuel()
    {
        if(statPoint > 0)
        {
            upgradeFuelLevel += 1;
            resources.fuel.maxFuel += 2;
            statPoint -= 1;
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateFuelUI(resources.fuel.CurrentFuel);
        }
        else
        {
            return;
        }

    }

    public void UpgradeAmmo()
    {
        if(statPoint > 0)
        {
            upgradeAmmoLevel += 1;
            resources.ammo.maxAmmoValue += 2;
            statPoint -= 1;
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateAmmoUI(resources.ammo.ammoValue, resources.ammo.maxAmmoValue);
        }
        else
        {
            return;
        }

    }

    //public void IncreaseTurretSpeed()
    //{

    //}
}

[System.Serializable]
public class Points
{
    public Stats statPoint;
    public int playerPoints = 0;

    public void AddPointsCheckpoint()
    {
        playerPoints += 5;
    }

    public void AddPointsBuilding()
    {
        playerPoints += 2;
    }

    public void ResetGame()
    {
        playerPoints = 0;
    }
}
