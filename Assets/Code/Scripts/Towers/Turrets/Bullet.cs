using UnityEngine;
using UnityEngine.EventSystems;

//Main projectile logic. Moves gameobject and does damage to enemies. Also helps with the kill stat logic.

public class Bullet : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] protected float bulletSpeed = 7f;
    public int bulletDamage = 1;
    
    //Data injectors
    protected TowerData sourceTower;
    protected Vector2 moveDirection;

    //Specific turret stuff that i dont want throw in a subclass 
    private string family;
    private int tier;
    float sniperChance = .67f;


    //sets a 2 second life span to the bullet
    protected virtual void Start()
    {        

        Destroy(gameObject, 2f);
    }

    //Sets injection of data from the tower and the target
    public void SetSource(TowerData source, Transform firstTarget)
    {
        sourceTower = source;
        moveDirection = (firstTarget.position - transform.position).normalized;
        family = sourceTower.towerFamily;
        tier = sourceTower.currentUpgradeLevel;
        //Crit chance for Sniper lvl 2 and beyond
        if((Random.value >= sniperChance) && (family == "Sniper Turret") &&  (tier >= 1))
        {
            Debug.Log("crit");
            bulletDamage *= 2;
        }
        RotateTowardsDirection();
        rb.linearVelocity = moveDirection * bulletSpeed;
    }
    protected void RotateTowardsDirection()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    //deals damage
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health enemy))
        {
            enemy.TakeDamage(bulletDamage, sourceTower);
        }
        Destroy(gameObject);
    }
}
