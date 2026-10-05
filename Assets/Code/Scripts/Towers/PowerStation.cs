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

    private void Awake()
    {
        rangeCollider.radius = towerData.range;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name} on Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (((1 << other.gameObject.layer) & towerLayer) == 0) return;

        TowerData targetTower = other.GetComponentInParent<TowerData>();

        if (targetTower != null)
        {
            if (buffedTowers.Add(targetTower))
            {
                if (targetTower.TryGetComponent<Turret>(out Turret turretscript))
                {
                    turretscript.BuffTurret(towerData.currentUpgradeLevel);
                }
                if (targetTower.TryGetComponent<TowerSlow>(out TowerSlow slowscript))
                {
                    slowscript.BuffSlower(towerData.currentUpgradeLevel);
                }
                if (targetTower.TryGetComponent<PowerStation>(out PowerStation stationscript))
                {
                    stationscript.BuffStation(towerData.currentUpgradeLevel);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & towerLayer) == 0) return;

        TowerData targetTower = other.GetComponentInParent<TowerData>();

        if (targetTower != null && buffedTowers.Contains(targetTower))
        {
            buffedTowers.Remove(targetTower);
            TryDebuff(targetTower);
        }
        buffedTowers.RemoveWhere(t => t == null);
    }
    
    private void OnDisable()
    {
        // Safety net: Clear all active buffs if the PowerStation is sold or destroyed
        foreach (TowerData tower in buffedTowers)
        {
            if (tower != null)
            {
                TryDebuff(tower);
            }
        }
        buffedTowers.Clear();
    }

    private void TryDebuff(TowerData targetTower)
    {
            if (targetTower.TryGetComponent<Turret>(out Turret turretscript))
            {
                turretscript.DebuffTurret(towerData.currentUpgradeLevel);
            }
            if (targetTower.TryGetComponent<TowerSlow>(out TowerSlow slowscript))
            {
                slowscript.DebuffSlower(towerData.currentUpgradeLevel);
            }
            if (targetTower.TryGetComponent<PowerStation>(out PowerStation stationscript))
            {
                stationscript.DebuffStation(towerData.currentUpgradeLevel);
            }
    }

    public void BuffStation(int powerStationlevel)
    {
        if(powerStationlevel >= 1)
        {
            towerData.range *= 1.25f;
        }
    }

    public void DebuffStation(int powerStationlevel)
    {
        if(powerStationlevel >= 1)
        {
            towerData.range /= 1.25f;
        }
    }

}
