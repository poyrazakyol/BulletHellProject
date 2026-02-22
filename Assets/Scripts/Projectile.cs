using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    
    void OnEnable()
    {
        Invoke("Deactivate", 3f); 
    }

    
    void OnDisable()
    {
        CancelInvoke(); 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
    void Deactivate()
    {
        ProjectilePool.Instance.ReturnProjectile(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            EnemyAI meleeEnemy = other.GetComponent<EnemyAI>();
            if (meleeEnemy != null)
            {
                meleeEnemy.TakeDamage(15f);
            }
            else
            {
                
                RangedEnemyAI rangedEnemy = other.GetComponent<RangedEnemyAI>();
                if (rangedEnemy != null)
                {
                    rangedEnemy.TakeDamage(15f);
                }
            }
            
            Deactivate(); 
        }
        else if (other.CompareTag("Wall"))
        {
            Deactivate(); 
        }
    }
}