using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//Core logic for the basic turret. Constantly scans for a enemy, rotates towards it, and fires bullets at it. If the turret kills a enemy, it adds it to its stats
public class Turret : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private TowerData towerData;



    [Header("Attributes")]
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] public float bps = 1f; //bullets per second
    [SerializeField] private bool seeInfrared;

    private Transform target;
    private float timeUntilFire;
    
    public List<Transform> Muzzles = new List<Transform>();
    private int muzzleIndex = 0;

    //if target is in range, shoot
    private void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange()) {
            target = null;
        } else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    //spawns a bullet and let it do its thing
    private void Shoot()
    {
        Transform currentMuzzle = Muzzles[muzzleIndex];
    
        muzzleIndex++;
        if(muzzleIndex >= Muzzles.Count)
        {
            muzzleIndex = 0;
        }
        GameObject bulletObj = Instantiate(bulletPrefab, currentMuzzle.position, currentMuzzle.rotation);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        TowerData sourceData = GetComponent<TowerData>();
        bulletScript.SetSource(sourceData, target);
    }

    //Increases kill count
    public void AddKill()
    {
        towerData.kills++;
    }

    //scans for a target
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerData.range, (Vector2) transform.position, 0f, enemyMask);

        if (hits.Length > 0 && seeInfrared)
        {
            target = hits[0].transform;
        }
        else
        {
            for(int i = 0; i < hits.Length; i++)
            {
                Health enemyScript = hits[i].collider.GetComponent<Health>();
                if (enemyScript && !enemyScript.IsInfrared)
                {
                    target = hits[i].transform;
                    break;
                }
            }
        }
    }

    //checks if the scanned target is in range
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerData.range;
    }

    //moves the turrent orientation to face the enemy
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //lets me see turrets targeting range
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, towerData.range);
    }
}
