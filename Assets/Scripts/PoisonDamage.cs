using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public float damagePerSecond = 10f; 
    public float lifetime = 5f; 

    void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI meleeEnemy = other.GetComponent<EnemyAI>();
            if (meleeEnemy != null) meleeEnemy.TakeDamage(damagePerSecond * Time.deltaTime);

            RangedEnemyAI rangedEnemy = other.GetComponent<RangedEnemyAI>();
            if (rangedEnemy != null) rangedEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}