using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Helath Settings")]
    public float maxHealth = 20f; 
    public float currentHealth;

    [Header("Attack Settings")]
    public float attackRange = 10f; 
    public float fireRate = 2f;     
    private float fireTimer;
    public GameObject projectilePrefab;
    public Transform firePoint;     

    [Header("Effects")]
    public GameObject xpGemPrefab;
    public GameObject deathEffectPrefab;

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        
        agent.stoppingDistance = attackRange; 
    }

    void Update()
    {
        if (player == null) return;

        
        agent.SetDestination(player.position);

        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= attackRange)
        {
            
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);

            
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            }
        }
    }

    void Shoot()
    {
        
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward;
        Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Vector3 dropPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, dropPos, Quaternion.identity);
            Destroy(effect, 1f);
        }
        Instantiate(xpGemPrefab, dropPos, Quaternion.identity);
        Destroy(gameObject);
    }
}