using System;
using UnityEngine;

public class SniperUpgrades : MonoBehaviour
{
    [SerializeField] Bullet bulletscript;
    float chance = .75f;
    void Awake()
    {
        if(UnityEngine.Random.value <= chance)
        {
            bulletscript.bulletDamage *= 2;
        }
    }
}
