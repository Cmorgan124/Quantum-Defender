using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; //moves body

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f; //speed of enemy

    [SerializeField] private Health healthScript;
    private Lives livesScript;


    private Transform target; //current point that is targeted
    private int pathIndex = 0; //Index of path point in list
    [System.NonSerialized] public Vector2 currentDirection;
    private float baseSpeed;
    private Coroutine activeFreeze;

    //sets movespeed, first target, and links to livesscript
    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.Instance.path[pathIndex]; //Sets first point as target
        livesScript = Object.FindFirstObjectByType<Lives>();
    }

    //checks if close enough to target, then moves on to next
    private void Update()
    {

        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.Instance.path.Length)
            {
                livesScript.lives -= healthScript.hitPoints;
                Destroy(gameObject); 
                return;
            } else
            {
                target = LevelManager.Instance.path[pathIndex]; 
            }
        }
    }

    //movese toward target
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        currentDirection = direction;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void Freeze(float newSpeed, float duration)
    {
        if(activeFreeze != null)
        {
            StopCoroutine(activeFreeze);
        }
        moveSpeed = newSpeed;
        activeFreeze = StartCoroutine(FreezeCountdown(duration));
    }

    private IEnumerator FreezeCountdown(float duration)
    {
        yield return new WaitForSeconds(duration);

        ResetSpeed();
        activeFreeze = null;
    }
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
