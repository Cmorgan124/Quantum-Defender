using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Refrences")] 
    [SerializeField] private SpriteRenderer render;

    [Header("Attributes")]
    [SerializeField] public int hitPoints = 3;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private Color damageColor;

    public bool IsInfrared { get; set; } = false;
    public bool canFrostbite = true;
    private WaitForSeconds damageDisplaytime = new WaitForSeconds(.1f);
    private bool isRunning = false;
    private Color basecolor;
    private bool isDestroyed = false;

    void Start()
    {
        basecolor = render.color;
    }


    //takes damage and kills enemies
    public void TakeDamage(int dmg, TowerData attackerData)
    {
        if (isRunning)
        {
            StopCoroutine(displayhit());
        }
        StartCoroutine(displayhit());
        
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

    private IEnumerator displayhit()
    {
        isRunning = true;
        render.color = damageColor;
        yield return damageDisplaytime;
        isRunning = false;
        render.color = basecolor;
    }
}
