using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;
    private Turret myTurret;

    //sets a 3 second life span to the bullet
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void SetOwner(Turret creator)
    {
        myTurret = creator;
    }

    //setter for target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    //moves to target
    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed;
    }

    //deals damage
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage, myTurret);
        }
        Destroy(gameObject);
    }
}
