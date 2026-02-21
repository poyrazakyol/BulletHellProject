using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    [Header("Attack Settings")]
        public float damageAmount = 15f; 
        public float attackRate = 1f;    
        private float attackTimer;
    
    private Transform player;
    private NavMeshAgent agent;
    
    [Header("Drop and Effect Settings")]
    public GameObject xpGemPrefab;
    public GameObject deathEffectPrefab; 

    
    public void Die()
    {
        Vector3 dropPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        
        
        GameObject effect = Instantiate(deathEffectPrefab, dropPos, Quaternion.identity);
        Destroy(effect, 1f); 

        Instantiate(xpGemPrefab, dropPos, Quaternion.identity);
        
        Destroy(gameObject);
    }

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            
            agent.SetDestination(player.position);
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
}