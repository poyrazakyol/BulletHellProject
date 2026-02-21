using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f; 

    void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            other.GetComponent<EnemyAI>().Die();
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}