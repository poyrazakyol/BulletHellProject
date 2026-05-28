using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorShooter : MonoBehaviour
{
    public GameObject meteorProjectilePrefab;
    public GameObject meteorIndicatorPrefab; // Yere çıkacak kırmızı uyarı çemberi
    public float fireRate = 8f;
    public int meteorCount = 1;
    public float damage = 50f;
    public float aoeRadius = 5f;

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireMeteors();
            fireTimer = fireRate;
        }
    }

    bool IsInCameraView(Vector3 worldPos)
    {
        if (Camera.main == null) return true;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
        return viewportPos.x >= 0f && viewportPos.x <= 1f && 
               viewportPos.y >= 0f && viewportPos.y <= 1f && 
               viewportPos.z > 0f;
    }

    void FireMeteors()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> validEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (IsInCameraView(enemy.transform.position))
            {
                validEnemies.Add(enemy);
            }
        }

        if (validEnemies.Count == 0) return;

        for (int i = 0; i < meteorCount; i++)
        {
            GameObject randomEnemy = validEnemies[Random.Range(0, validEnemies.Count)];
            if (randomEnemy != null)
            {
                // Hedef pozisyonunu yere sabitle
                Vector3 targetPos = randomEnemy.transform.position;
                targetPos.y = 0.1f; 

                // Uyarı göstergesini yarat
                if (meteorIndicatorPrefab != null)
                {
                    // Çember genelde yere paralel olması için x=90 derece döndürülür
                    GameObject indicator = Instantiate(meteorIndicatorPrefab, targetPos, Quaternion.Euler(90, 0, 0));
                    Destroy(indicator, 1.2f); // Meteor düşene kadar kalsın
                }

                StartCoroutine(DropMeteorRoutine(targetPos));
            }
        }
    }

    IEnumerator DropMeteorRoutine(Vector3 targetPos)
    {
        // 1 saniye bekle, meteor sonra düşmeye başlasın
        yield return new WaitForSeconds(1.0f); 

        if (meteorProjectilePrefab != null)
        {
            Vector3 spawnPos = targetPos + Vector3.up * 25f; // 25 birim yukarıdan düşsün
            GameObject meteor = Instantiate(meteorProjectilePrefab, spawnPos, Quaternion.identity);
            
            MeteorProjectile mp = meteor.GetComponent<MeteorProjectile>();
            if (mp != null)
            {
                mp.targetPos = targetPos;
                mp.damage = this.damage;
                mp.aoeRadius = this.aoeRadius;
            }
        }
    }
}
