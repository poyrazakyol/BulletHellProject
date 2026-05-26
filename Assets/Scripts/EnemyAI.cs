using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 30f;
    private float currentHealth;
    
    [Header("Attack Settings")]
    public float damageAmount = 15f; 
    public float attackRange = 2f; // Yakın dövüş menzili
    public float attackRate = 1.5f;    
    private float attackTimer;
    
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator; // Animasyon kontrolcüsü
    
    [Header("Drop and Effect Settings")]
    public GameObject xpGemSmall;  
    public GameObject xpGemMedium; 
    public GameObject xpGemLarge;  
    public GameObject deathEffectPrefab; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        // Modeli (görseli) objenin içine sürükleyeceğin için GetComponentInChildren kullanıyoruz
        animator = GetComponentInChildren<Animator>();
        
        currentHealth = maxHealth; 
        
        // Düşman oyuncuya vurma mesafesine gelince dursun
        agent.stoppingDistance = attackRange;
    }

    void Update()
    {
        if (player == null) return;

        // --- 1. HAREKET VE ANİMASYON ---
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position); 
            
            // Düşmanın hızını Animator'a gönderiyoruz (Koşma/Durma geçişi için)
            if (animator != null)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
        }

        // --- 2. SALDIRI MANTIĞI ---
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange)
        {
            // Vururken oyuncuya dönmeye devam et
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
            }

            // Saldırı süresi kontrolü
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackRate;
            }
        }
    }
    
    void AttackPlayer()
    {
        // 1. Animasyonu Tetikle
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // 2. Oyuncuya Hasar Ver
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Vector3 dropPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, dropPos, Quaternion.identity);
            Destroy(effect, 1f); 
        }

        float randomVal = Random.value; 
        
        if (randomVal <= 0.05f) 
            Instantiate(xpGemLarge, dropPos, Quaternion.identity);
        else if (randomVal <= 0.20f) 
            Instantiate(xpGemMedium, dropPos, Quaternion.identity);
        else 
            Instantiate(xpGemSmall, dropPos, Quaternion.identity);
        
        Destroy(gameObject);
    }
}