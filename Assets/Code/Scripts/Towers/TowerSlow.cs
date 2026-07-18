using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

//Main slowmo logic, scans its range for enemies and temporarily reduces there speed.

public class TowerSlow : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private TowerData towerData;
    private Transform target;

    [Header("Attributes")]
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float freezeTime = 2f;
    [SerializeField] private float freezeStrength = .5f;

    private float timeUntilFire = 0f;

    private void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        if(!CheckTargetIsInRange()) {
            target = null;
            timeUntilFire = 0f;
        } else
        {
            timeUntilFire += Time.deltaTime;
            Debug.Log(timeUntilFire);
            if (timeUntilFire >= cooldown)
            {
                Debug.Log("fire");
                FreezeEnemies();
                timeUntilFire = 0f;
            }
        }
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, towerData.range, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    //slows enemies movement speed who are in range
    private void FreezeEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, towerData.range, enemyMask);

        if (hits.Length > 0)
        {
            Debug.Log("Freezing " + hits.Length + " targets" );
            for(int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.Freeze(freezeStrength, freezeTime);
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerData.range + .1f;
    }

    //range gizmo
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, towerData.range);
    }
}
