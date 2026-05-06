using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 30f;
    private float currentHealth;
    
    [Header("Attack Settings")]
    public float damageAmount = 15f; 
    public float attackRate = 1f;    
    private float attackTimer;
    
    private Transform player;
    private NavMeshAgent agent;
    
    [Header("Drop and Effect Settings")]
    public GameObject xpGemSmall;  // Mavi XP
    public GameObject xpGemMedium; // Yeşil XP
    public GameObject xpGemLarge;  // Kırmızı XP
    public GameObject deathEffectPrefab; 

    public void Die()
    {
        Vector3 dropPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, dropPos, Quaternion.identity);
            Destroy(effect, 1f); 
        }

        // --- ŞANSA BAĞLI XP DÜŞÜRME SİSTEMİ ---
        float randomVal = Random.value; // 0.0 ile 1.0 arası sayı üretir
        
        if (randomVal <= 0.05f) // %5 İhtimal
        {
            Instantiate(xpGemLarge, dropPos, Quaternion.identity);
        }
        else if (randomVal <= 0.20f) // %15 İhtimal (0.05 ile 0.20 arası)
        {
            Instantiate(xpGemMedium, dropPos, Quaternion.identity);
        }
        else // Kalan %80 İhtimal
        {
            Instantiate(xpGemSmall, dropPos, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        currentHealth = maxHealth; 
    }

    void Update()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position); // (Hedefin adı sende neyse o kalmalı)
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackTimer -= Time.deltaTime;
            
            if (attackTimer <= 0f)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                attackTimer = attackRate;
            }
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackTimer = 0f; 
        }
    }
    
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}