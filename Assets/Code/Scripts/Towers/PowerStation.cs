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
        TowerData targetTower = other.GetComponentInParent<TowerData>();
        if (targetTower == null) return;

        if (((1 << targetTower.gameObject.layer) & towerLayer) == 0) return;

        if (buffedTowers.Contains(targetTower)) return;
        if (buffedTowers.Add(targetTower))
        {
            Debug.Log($"New tower registered: {targetTower.name} via collider {other.name}");
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
        TowerData targetTower = other.GetComponentInParent<TowerData>();
    if (targetTower == null) return;

        if (((1 << targetTower.gameObject.layer) & towerLayer) == 0) return;

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
        BuffStation(highestLevel);
    }
    public void BuffStation(int powerStationlevel)
    {
        if (powerStationlevel >= 1)
        {
            towerData.range = baseRange * 1.20f;
            towerData.rangescript.UpdateRange(towerData.range);
            if(powerStationlevel >= 2)
            {
                towerData.upgradeCostMultiplier = 0.8f;
            }
        }
    }

    private void ResetToBaseStats()
    {
        towerData.range = baseRange;
        towerData.rangescript.UpdateRange(towerData.range);
        towerData.upgradeCostMultiplier = 1f;
    }
}
