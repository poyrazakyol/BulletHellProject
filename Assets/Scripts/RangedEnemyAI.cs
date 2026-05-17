using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 20f; 
    public float currentHealth;

    [Header("Attack Settings")]
    public float attackRange = 10f; 
    public float fireRate = 2f;     
    private float fireTimer;
    public GameObject projectilePrefab;
    public Transform firePoint;     

    [Header("Effects & Drops")]
    public GameObject xpGemSmall;  
    public GameObject xpGemMedium; 
    public GameObject xpGemLarge;  
    public GameObject deathEffectPrefab;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        // Modeli Child (alt obje) yaptığımız için GetComponentInChildren kullanıyoruz
        animator = GetComponentInChildren<Animator>();
        
        currentHealth = maxHealth;
        
        // Düşman menzile girince DURSUN diyoruz
        agent.stoppingDistance = attackRange; 
    }

    void Update()
    {
        if (player == null) return;

        // 1. HAREKET VE ANİMASYON MANTIĞI
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position); 
            
            // Düşmanın mevcut hızını Animator'daki "Speed" parametresine gönderiyoruz.
            // Menzile girip durduğunda bu hız 0 olacak ve Animator otomatik olarak "combat idle"a geçecek.
            if (animator != null)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
        }
        
        // 2. SALDIRI MANTIĞI
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange)
        {
            // Düşman dursa bile oyuncuya doğru dönmeye devam etsin (Nişan alsın)
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
            }
            
            // Ateş etme süresi kontrolü
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
        // Ateş etme animasyonunu (Trigger) tetikliyoruz
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

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

        float randomVal = Random.value;
        if (randomVal <= 0.05f) 
        {
            Instantiate(xpGemLarge, dropPos, Quaternion.identity);
        }
        else if (randomVal <= 0.20f) 
        {
            Instantiate(xpGemMedium, dropPos, Quaternion.identity);
        }
        else 
        {
            Instantiate(xpGemSmall, dropPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}