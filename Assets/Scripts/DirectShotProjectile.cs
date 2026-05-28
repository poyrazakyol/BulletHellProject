using UnityEngine;
using System.Collections.Generic;

public class DirectShotProjectile : MonoBehaviour
{
    public float speed = 25f;
    public float damage = 25f;
    public float maxRange = 8f; // Yaklaşık 2x yörünge mesafesi
    public float aoeRadius = 2.5f;
    
    [Header("Visuals")]
    public GameObject explosionEffectPrefab;

    private Vector3 startPos;
    private List<GameObject> hitEnemies = new List<GameObject>();

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(startPos, transform.position) >= maxRange)
        {
            Explode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                // Doğrudan isabet hasarı (Delip geçer)
                DamageHelper.ApplyDamage(other.gameObject, damage);
                hitEnemies.Add(other.gameObject);
            }
        }
        else if (other.CompareTag("Wall"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Menzil sonuna gelince veya duvara çarpınca AoE patlama
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") && !hitEnemies.Contains(hit.gameObject))
            {
                // Splash hasarı
                DamageHelper.ApplyDamage(hit.gameObject, damage * 0.5f); 
            }
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
