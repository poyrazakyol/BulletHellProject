using UnityEngine;

public class BoomerangShooter : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public float fireRate = 3f;
    public int boomerangCount = 1;
    public float damage = 20f;
    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireBoomerang();
            fireTimer = fireRate;
        }
    }

    void FireBoomerang()
    {
        if (boomerangPrefab == null) return;
        
        float spreadAngle = 15f;
        float startAngle = -spreadAngle * (boomerangCount - 1) / 2f;

        for (int i = 0; i < boomerangCount; i++)
        {
            float angle = startAngle + i * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
            
            Vector3 spawnPos = transform.position + Vector3.up * 1f;
            
            GameObject proj = Instantiate(boomerangPrefab, spawnPos, rotation);
            BoomerangProjectile boomerang = proj.GetComponent<BoomerangProjectile>();
            if (boomerang != null)
            {
                boomerang.damage = this.damage;
            }
        }
    }
}
