using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab; 
    public float spawnRate = 1f;   
    
    private float spawnTimer;
    private DungeonGenerator dungeonGen;

    void Start()
    {
        
        dungeonGen = GetComponent<DungeonGenerator>();
    }

    void Update()
    {
        
        if (dungeonGen == null || dungeonGen.floorPositions.Count == 0) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnRate; 
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, dungeonGen.floorPositions.Count);
        Vector3 spawnPos = dungeonGen.floorPositions[randomIndex];
        
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        EnemyAI ai = newEnemy.GetComponent<EnemyAI>();
        GameDirector director = GetComponent<GameDirector>();
        
        if (ai != null && director != null)
        {
            ai.maxHealth = director.currentEnemyHealth;
            ai.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = director.currentEnemySpeed;
        }
    }
}