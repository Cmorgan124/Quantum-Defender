using UnityEngine;
using UnityEngine.EventSystems;

public class ChainShot : Bullet
{
    [Header("Settings")]
    [SerializeField] private int bounces = 1;
    private int bounceIndex = 0;
    [SerializeField] float radius;
    private float thirdBouncechance = .75f;
    private Transform lastHitEnemy;

    protected override void Start()
    {
        base.Start();
        if(Random.value >= thirdBouncechance)
        {
            Debug.Log("3");
            bounces += 1;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Health enemy))
        {
            enemy.TakeDamage(bulletDamage, sourceTower);

            if(bounceIndex < bounces)
            {
                Transform nextTarget = FindNextTarget(collision.transform);

                if(nextTarget != null)
                {
                   bounceIndex++;
                   moveDirection = (nextTarget.position - transform.position).normalized;
                   RotateTowardsDirection();
                    
                   if(TryGetComponent(out Rigidbody2D bulletrb))
                    {
                        bulletrb.linearVelocity = moveDirection * bulletSpeed; 
                    }
                   return;
                }
            }
        }
        Destroy(gameObject);
    }

    private Transform FindNextTarget(Transform currentEnemy)
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position, radius);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var col in collidersHit)
        {
            if (col.transform == currentEnemy) continue;

            if(col.TryGetComponent(out Health potentialEnemy))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);

                if(distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = col.transform;
                }
            }
        }
        return closestEnemy;
    }
}
