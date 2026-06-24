using UnityEngine;

//Main projectile logic. Moves gameobject and does damage to enemies. Also helps with the kill stat logic.

public class Bullet : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 7f;
    public int bulletDamage = 1;
    
    private Transform target;
    private TowerData sourceTower;
    private string family;
    private int tier;
    float sniperChance = .75f;


    //sets a 2 second life span to the bullet and fires it in the direction of the enemy
    private void Start()
    {        
        //Crit chance for Sniper lvl 2 and beyond
        if((Random.value >= sniperChance) && (family == "Sniper Turret") &&  (tier >= 1))
        {
            Debug.Log("crit");
            bulletDamage *= 2;
        }
        Destroy(gameObject, 2f);

        if (!target) return;
    }

    private void SetCourse(Vector3 targetPosition)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    //Setter for kill logic
    public void SetSource(TowerData source, Transform firstTarget)
    {
        sourceTower = source;
        family = sourceTower.towerFamily;
        tier = sourceTower.currentUpgradeLevel;
        SetCourse(firstTarget.position);
    }

    //setter for target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    //deals damage
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage, sourceTower);
        }
        Destroy(gameObject);
    }
}
