using UnityEngine;
using System.Collections.Generic;

public class BoomerangProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float deceleration = 15f;
    public float damage = 20f;
    
    private Transform player;
    private Vector3 flyDirection;
    private float currentSpeed;
    private bool returning = false;
    private List<GameObject> hitEnemies = new List<GameObject>();

    [Header("Visuals")]
    public Transform rotatingMesh; // Opsiyonel: dönen child obje

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        
        flyDirection = transform.forward;
        currentSpeed = speed;
        Destroy(gameObject, 8f); 
    }

    void Update()
    {
        if (!returning)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            
            if (currentSpeed <= 0f)
            {
                returning = true;
                hitEnemies.Clear(); 
            }
            
            transform.position += flyDirection * currentSpeed * Time.deltaTime;
        }
        else
        {
            if (player != null)
            {
                Vector3 targetPos = player.position + Vector3.up * 1f;
                Vector3 returnDir = (targetPos - transform.position).normalized;
                
                // Sabit hızla veya artan hızla geri dön
                currentSpeed = Mathf.MoveTowards(currentSpeed, speed * 1.5f, deceleration * Time.deltaTime);
                transform.position += returnDir * currentSpeed * Time.deltaTime;
                
                if (Vector3.Distance(transform.position, targetPos) < 1.5f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (rotatingMesh != null)
        {
            rotatingMesh.Rotate(0, 1000f * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, 1000f * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                DamageHelper.ApplyDamage(other.gameObject, damage);
                hitEnemies.Add(other.gameObject);
            }
        }
    }
}
