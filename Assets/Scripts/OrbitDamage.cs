using UnityEngine;

public class OrbitDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            
            other.GetComponent<EnemyAI>().Die();
        }
    }
}