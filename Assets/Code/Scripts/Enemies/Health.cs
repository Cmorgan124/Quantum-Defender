using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 3;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    //takes damage and kills enemies
    public void TakeDamage(int dmg, TowerData attackerData)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            Die(attackerData);
        }
    }

    private void Die(TowerData killerData)
    {
        if(killerData != null)
        {
            killerData.kills++;
        }
        Destroy(gameObject);
    }

    //increases health based on wave
    public void healthIncrease(int wave)
    {
        if (wave < 4)
        {
            hitPoints = 3;
        }
        else if (wave >= 4 && wave < 7)
        {
            hitPoints = 4;
        }
        else if (wave >= 7 && wave < 10)
        {
            hitPoints = 5;
        }
        else
        {
            hitPoints = 6;
        }
    }
}
