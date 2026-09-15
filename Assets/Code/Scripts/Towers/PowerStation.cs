using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerData towerData;
    private List<TowerData> buffedTowers = new List<TowerData>();

    private void Start()
    {
        RemoveBuffs();
        ScanAndBuff();
    }

    private void RemoveBuffs()
    {
        foreach(TowerData tower in buffedTowers)
        {
            if (TryGetComponent<Turret>(out Turret turretscript))
            {
                DebuffTurret(turretscript);
            }
            if (TryGetComponent<TowerSlow>(out TowerSlow slowscript))
            {
                DebuffSlower(slowscript);
            }
            if (TryGetComponent<PowerStation>(out PowerStation stationscript))
            {
                DebuffStation(stationscript);
            }
            
        }
    }

    private void ScanAndBuff()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerData.range, transform.position);
        for(int i = 0; i < hits.Length; i++)
        {  
            if (TryGetComponent<Turret>(out Turret turretscript))
            {
                BuffTurret(turretscript);
            }
            if (TryGetComponent<TowerSlow>(out TowerSlow slowscript))
            {
                BuffSlower(slowscript);
            }
            if (TryGetComponent<PowerStation>(out PowerStation stationscript))
            {
                BuffStation(stationscript);
            }
        }
    }

    //move all of these into the scripts the access instead
    private void BuffTurret(Turret turret)
    {
        turret.bps /= 1.1f;
    }

    private void DebuffTurret(Turret turret)
    {
        turret.bps *= 1.1f;
    }

    private void BuffSlower(TowerSlow slow)
    {
        slow.cooldown /= 1.1f;
    }

    private void DebuffSlower(TowerSlow slow)
    {
        slow.cooldown *= 1.1f;
    }

    private void BuffStation(PowerStation station)
    {
        
    }

    private void DebuffStation(PowerStation station)
    {
        
    }

}
