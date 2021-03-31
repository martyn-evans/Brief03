using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public int statPoint = 0;
    public int playerScore = 0;
    public int pointThreshold = 10;
    public int upgradeFuelLevel = 0;
    public int upgradeAmmoLevel = 0;
    public int upgradeSpeedLevel = 0;

    public Resources resources;
    public TankMovement turret;
    public UIManager uiManager;

    public static UnityEvent playerPickUp = new UnityEvent();
    public static UnityEvent upgradeFuel = new UnityEvent();
    public static UnityEvent upgradeAmmo = new UnityEvent();
    public static UnityEvent upgradeTurret = new UnityEvent();

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

    public void CheckStatPoint()
    {
        if (playerScore >= pointThreshold)
        {
            statPoint += 1;
            uiManager.inGameUI.ShowUpgradeTextUI(true);
            uiManager.skillMenu.UpdateSkillPointUI();
            pointThreshold *= 2;
        }
    }

    public void ResetGame()
    {
        statPoint = 0;
        playerScore = 0;
    }

    public void ItemPickUpBonus()
    {
        playerScore += 1;
        CheckStatPoint();
        uiManager.inGameUI.UpdateScore();
        uiManager.loseMenu.UpdateLoseMenuScore();
    }

    public void UpgradeFuel()
    {
        if(statPoint > 0)
        {
            upgradeFuelLevel += 1;
            resources.fuel.maxFuel += 2;
            resources.fuel.maxFuel += 2;
            statPoint -= 1;
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateFuelUI(resources.fuel.CurrentFuel);

            if(statPoint == 0)
            {
                uiManager.inGameUI.ShowUpgradeTextUI(false);
            }
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
            resources.ammo.ammoValue += 2;
            resources.ammo.maxAmmoValue += 2;
            statPoint -= 1; 
            uiManager.skillMenu.UpdateSkillPointUI();
            uiManager.inGameUI.UpdateAmmoUI(resources.ammo.ammoValue, resources.ammo.maxAmmoValue);

            if (statPoint == 0)
            {
                uiManager.inGameUI.ShowUpgradeTextUI(false);
            }
        }
        else
        {
            return;
        }
    }

    public void UpgradeTurretSpeed()
    {
        if (statPoint > 0)
        {
            upgradeSpeedLevel += 1;
            turret.turretTurnSpeed += 2.5f;
            Debug.Log("turret speed is " + turret.turretTurnSpeed);
            statPoint -= 1;
            uiManager.skillMenu.UpdateSkillPointUI();

            if (statPoint == 0)
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