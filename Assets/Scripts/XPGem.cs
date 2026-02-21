using UnityEngine;

public class XPGem : MonoBehaviour
{
    public float xpAmount = 10f; 
    public float magnetRadius = 3f; 
    public float moveSpeed = 8f; 

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            
            
            if (distance <= magnetRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AddXP(xpAmount);
            Destroy(gameObject);
        }
    }
}