using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 4;
    [SerializeField] private float enemiesPerSecond = 0.5f;    [SerializeField] private float timeBetweenWaves = 5f; 
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float eniemesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public int currentWave = 1; 
    private float timeSinceLastSpawn;    
    private int enemiesAlive; 
    private int enemiesLeftToSpawn;
    private float eps; 
    private bool isSpawning = false;

     //listens for enemy death events
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

     //starts first wave
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    //spawns enemies
    private void Update()
    {
        if (!isSpawning) return; 

        timeSinceLastSpawn += Time.deltaTime; 

        
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0){
            SpawnEnemy();

            
            enemiesLeftToSpawn--;
            enemiesAlive++;

            timeSinceLastSpawn = 0f;
        }

        
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave(); //ends wave
        } 
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    //Coroutine that starts a wave after a delay
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave(); 
        eps = EnemiesPerSecond();
    }

    //ends current wave and starts the next
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    //Spawns one enemy
    private void SpawnEnemy() 
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index]; 
        GameObject v = Instantiate(prefabToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity); 

        Health h = v.GetComponent<Health>();

        if (h != null)
        {
            h.healthIncrease(currentWave);
        }
    }

    //Calculates how many enemies should spawn in a wave
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); 
    }

    //Calculates how fast enemies should spawn in a wave
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, eniemesPerSecondCap); 
    }

}
