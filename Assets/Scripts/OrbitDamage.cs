using UnityEngine;

public class OrbitDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI meleeEnemy = other.GetComponent<EnemyAI>();
            if (meleeEnemy != null) meleeEnemy.TakeDamage(30f);

            RangedEnemyAI rangedEnemy = other.GetComponent<RangedEnemyAI>();
            if (rangedEnemy != null) rangedEnemy.TakeDamage(30f);
        }
    }
}