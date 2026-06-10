using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

//Main slowmo logic, scans its range for enemies and temporarily reduces there speed.

public class TowerSlow : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //attacks per second
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    //periodically fires
    private void Update()
    {
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                FreezeEnemies();
                timeUntilFire = 0f;
            }
        }
    }

    //slows enemies movement speed who are in range
    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    //gives them back their speed
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    //lemme see yo targeting range
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
