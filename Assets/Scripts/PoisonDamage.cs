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
            other.GetComponent<EnemyAI>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}