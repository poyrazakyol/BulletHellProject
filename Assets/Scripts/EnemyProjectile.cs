using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    private Transform player;
    private Vector3 targetDirection;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            
            targetDirection = (player.position - transform.position).normalized;
            targetDirection.y = 0; 
        }
        
        
        Destroy(gameObject, 4f); 
    }

    void Update()
    {
        
        transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}