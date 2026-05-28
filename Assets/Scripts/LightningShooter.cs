using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningShooter : MonoBehaviour
{
    public GameObject lightningStrikePrefab;
    public float fireRate = 4f;
    public int targetCount = 1;
    public float damage = 30f;
    public float aoeRadius = 3f;
    public float maxRange = 15f;
    public float warningDuration = 1.5f;

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireLightning();
            fireTimer = fireRate;
        }
    }

    bool IsInCameraView(Vector3 worldPos)
    {
        if (Camera.main == null) return true; // Eğer kamera yoksa (hata durumu) geçersiz sayma
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
        return viewportPos.x >= 0f && viewportPos.x <= 1f && 
               viewportPos.y >= 0f && viewportPos.y <= 1f && 
               viewportPos.z > 0f;
    }

    void FireLightning()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in allEnemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= maxRange)
            {
                // Kameranın görüş açısındaysa ve henüz hedeflenmemişse ekle
                if (IsInCameraView(enemy.transform.position) && enemy.GetComponent<LightningTarget>() == null)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }

        // Yakından uzağa sırala
        enemiesInRange.Sort((a, b) => 
            Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position))
        );

        int count = Mathf.Min(targetCount, enemiesInRange.Count);
        for (int i = 0; i < count; i++)
        {
            float randomStagger = Random.Range(0f, 0.5f);
            StartCoroutine(StrikeRoutine(enemiesInRange[i], randomStagger));
        }
    }

    IEnumerator StrikeRoutine(GameObject enemy, float staggerDelay)
    {
        float totalWarningTime = warningDuration + staggerDelay;

        LightningTarget target = enemy.AddComponent<LightningTarget>();
        target.duration = totalWarningTime;

        yield return new WaitForSeconds(totalWarningTime);

        if (enemy != null)
        {
            Vector3 hitPos = enemy.transform.position;
            if (lightningStrikePrefab != null)
            {
                GameObject strike = Instantiate(lightningStrikePrefab, hitPos, Quaternion.identity);
                LightningStrikeEffect effect = strike.GetComponent<LightningStrikeEffect>();
                if (effect != null)
                {
                    effect.damage = this.damage;
                    effect.aoeRadius = this.aoeRadius;
                    // Doğrudan hedefi de vuralım
                    DamageHelper.ApplyDamage(enemy, this.damage);
                }
            }
        }
    }
}
