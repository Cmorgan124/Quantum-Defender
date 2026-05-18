using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 3;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    private void Start()
    {
    }

    //takes damage and kills enemies
    public void TakeDamage(int dmg, Turret attacker)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.Instance.IncreaseCurrency(currencyWorth);
            if(attacker != null)
            {
                attacker.AddKill();
            }
            isDestroyed = true;
            Destroy(gameObject);
        }
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
