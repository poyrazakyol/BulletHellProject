using UnityEngine;
using System.Collections.Generic;

public class GravityVortexShooter : MonoBehaviour
{
    public GameObject vortexPrefab;
    public float fireRate = 10f;
    
    public float pullRadius = 8f;
    public float pullForce = 5f;
    public float tickDamage = 5f;
    public float duration = 5f;

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            SpawnVortex();
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

    void SpawnVortex()
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

        // Rastgele bir düşman seç
        GameObject targetEnemy = validEnemies[Random.Range(0, validEnemies.Count)];
        if (targetEnemy != null && vortexPrefab != null)
        {
            Vector3 spawnPos = targetEnemy.transform.position;
            spawnPos.y = 1.0f; // Yerden biraz yukarıda dursun

            GameObject vortex = Instantiate(vortexPrefab, spawnPos, Quaternion.identity);
            GravityVortex gv = vortex.GetComponent<GravityVortex>();
            if (gv != null)
            {
                gv.pullRadius = this.pullRadius;
                gv.pullForce = this.pullForce;
                gv.tickDamage = this.tickDamage;
                gv.duration = this.duration;
            }
        }
    }
}
