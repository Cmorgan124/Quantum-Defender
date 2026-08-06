using UnityEngine;

public class HeatSeeking : Missile
{
    void Update()
    {
        if(target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
            RotateTowardsDirection();
            rb.linearVelocity = moveDirection * bulletSpeed;
        }
        

    }
}
