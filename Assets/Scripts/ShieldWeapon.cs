using UnityEngine;
using System.Collections.Generic;

public class ShieldWeapon : MonoBehaviour
{
    public float damageReductionPercent = 0.20f;
    public float tickDamage = 10f;
    public float tickRate = 0.5f;

    private PlayerHealth playerHealth;
    private List<GameObject> enemiesInside = new List<GameObject>();
    private float tickTimer;

    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.shieldDamageReductionPercent = damageReductionPercent;
        }
    }

    public void UpdateStats(float newReduction, float newDamage)
    {
        damageReductionPercent = newReduction;
        tickDamage = newDamage;
        if (playerHealth != null)
        {
            playerHealth.shieldDamageReductionPercent = damageReductionPercent;
        }
    }

    void Update()
    {
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            // Ölü düşmanları listeden temizle
            enemiesInside.RemoveAll(e => e == null);

            foreach (GameObject enemy in enemiesInside)
            {
                DamageHelper.ApplyDamage(enemy, tickDamage);
            }
            tickTimer = tickRate;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInside.Contains(other.gameObject))
        {
            enemiesInside.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside.Remove(other.gameObject);
        }
    }

    void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.shieldDamageReductionPercent = 0f;
        }
        enemiesInside.Clear();
    }
}
