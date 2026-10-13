using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    [SerializeField] private TowerData towerData; 
    [SerializeField] private LayerMask towerLayer;

    [SerializeField] private CircleCollider2D rangeCollider;
    private HashSet<TowerData> buffedTowers = new HashSet<TowerData>();
    private Dictionary<PowerStation, int> activePowerStationBuffs = new Dictionary<PowerStation, int>();
    private float baseRange;

    private void Awake()
    {
        rangeCollider.radius = towerData.range;
        baseRange = towerData.range;
        AddOrUpdatePowerStationBuff(this, towerData.currentUpgradeLevel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & towerLayer) == 0) return;

        TowerData targetTower = other.GetComponentInParent<TowerData>();

        if (buffedTowers.Add(targetTower))
        {
            if (targetTower.TryGetComponent<Turret>(out Turret turretScript))
            {
                turretScript.AddOrUpdatePowerStationBuff(this, towerData.currentUpgradeLevel);
            }
            else if (targetTower.TryGetComponent<TowerSlow>(out TowerSlow slowScript))
            {
                slowScript.AddOrUpdatePowerStationBuff(this, towerData.currentUpgradeLevel);
            }
            else
            {
                PowerStation stationScript = targetTower.GetComponentInChildren<PowerStation>();
                if (stationScript != null)
                {
                    stationScript.AddOrUpdatePowerStationBuff(this, towerData.currentUpgradeLevel);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & towerLayer) == 0) return;

        TowerData targetTower = other.GetComponentInParent<TowerData>();

        if (buffedTowers.Contains(targetTower))
        {
            buffedTowers.Remove(targetTower);

            if (targetTower.TryGetComponent<Turret>(out Turret turretScript))
            {
                turretScript.RemovePowerStationBuff(this);
            }
            else if (targetTower.TryGetComponent<TowerSlow>(out TowerSlow slowScript))
            {
                slowScript.RemovePowerStationBuff(this);
            }
            else
            {
                PowerStation stationScript = targetTower.GetComponentInChildren<PowerStation>();
                if (stationScript != null)
                {
                    stationScript.RemovePowerStationBuff(this);
                }
            }
        }
    }
    
    private void OnDisable()
    {
        // Safety cleanup: If this PowerStation is destroyed, sold, or disabled, 
        // instantly remove its buff from every single tower it was currently affecting
        foreach (TowerData targetTower in buffedTowers)
        {
            if (targetTower != null)
            {
                if (targetTower.TryGetComponent<Turret>(out Turret turretScript))
                {
                    turretScript.RemovePowerStationBuff(this);
                }
                else if (targetTower.TryGetComponent<TowerSlow>(out TowerSlow slowScript))
                {
                    slowScript.RemovePowerStationBuff(this);
                }
                else
                {
                    PowerStation stationScript = targetTower.GetComponentInChildren<PowerStation>();
                    if (stationScript != null)
                    {
                        stationScript.RemovePowerStationBuff(this);
                    }
                }
            }
        }
        buffedTowers.Clear();
    }

    public void AddOrUpdatePowerStationBuff(PowerStation station, int level)
    {
        activePowerStationBuffs[station] = level;
        ApplyHighestBuff();
    }
    public void RemovePowerStationBuff(PowerStation station)
    {
        if (activePowerStationBuffs.ContainsKey(station))
        {
            activePowerStationBuffs.Remove(station);
        }
        ApplyHighestBuff();
    }

    private void ApplyHighestBuff()
    {
        if (activePowerStationBuffs.Count == 0)
        {
            ResetToBaseStats();
            return;
        }

        int highestLevel = 0;
        foreach (var kvp in activePowerStationBuffs)
        {
            if (kvp.Value > highestLevel)
            {
                highestLevel = kvp.Value;
            }
        }
        ResetToBaseStats();

        BuffTurret(highestLevel);
    }
    public void BuffTurret(int powerStationlevel)
    {
        switch (powerStationlevel)
        {
            case 1:
                break;
            case 2:
                towerData.range = baseRange * 1.20f;
                towerData.rangescript.UpdateRange(towerData.range);
                break;
        }
    }

    private void ResetToBaseStats()
    {
        towerData.range = baseRange;
        towerData.rangescript.UpdateRange(towerData.range);
    }

}
