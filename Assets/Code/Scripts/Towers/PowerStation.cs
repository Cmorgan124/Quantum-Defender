using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerData towerData;

    private Transform target;

    void ScanNearbyTowers()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerData.range, transform.position);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        else
        {
            for(int i = 0; i < hits.Length; i++)
            {
                //buff functions
            }
        }
    }

}
