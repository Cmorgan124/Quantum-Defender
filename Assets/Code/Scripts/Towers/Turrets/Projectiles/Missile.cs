using System;
using UnityEditor;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] float splashRadius;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject explosion;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 tipPosition = transform.position + (transform.up * 0.5f);
        Instantiate(explosion, tipPosition, transform.rotation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashRadius, enemyMask);

        if(hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];
                hit.TryGetComponent(out Health enemy);
                enemy.TakeDamage(bulletDamage,sourceTower);   
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, splashRadius);
    }
}
