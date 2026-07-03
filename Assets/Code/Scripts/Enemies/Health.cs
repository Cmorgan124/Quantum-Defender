using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 3;
    [SerializeField] private int currencyWorth = 50;
    public bool IsInfrared { get; set; } = false;

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
        LevelManager.Instance.currency += currencyWorth;
    }
}
