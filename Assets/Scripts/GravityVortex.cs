using UnityEngine;
using UnityEngine.AI;

public class GravityVortex : MonoBehaviour
{
    [HideInInspector] public float pullRadius = 8f;
    [HideInInspector] public float pullForce = 5f;
    [HideInInspector] public float tickDamage = 5f;
    [HideInInspector] public float duration = 5f;

    private float tickTimer;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        // 1. Çekim Alanı (Kara delik etkisi)
        Collider[] hits = Physics.OverlapSphere(transform.position, pullRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Vector3 directionToCenter = (transform.position - hit.transform.position).normalized;
                
                NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
                if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
                {
                    // NavMesh üzerinde güvenli bir şekilde kaydır
                    agent.Move(directionToCenter * pullForce * Time.deltaTime);
                }
                else
                {
                    // Eğer fizik tabanlı veya normal hareket eden bir düşmansa doğrudan kaydır
                    hit.transform.position += directionToCenter * pullForce * Time.deltaTime;
                }
            }
        }

        // 2. Zamanla Hasar (Tick Damage)
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    DamageHelper.ApplyDamage(hit.gameObject, tickDamage);
                }
            }
            tickTimer = 0.5f; // Yarım saniyede bir hasar tiklesin
        }

        // Görsel efekt (Kendi etrafında yavaşça dönsün)
        transform.Rotate(0, 200f * Time.deltaTime, 0);
    }
}
