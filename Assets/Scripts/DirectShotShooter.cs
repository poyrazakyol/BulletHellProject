using UnityEngine;

public class DirectShotShooter : MonoBehaviour
{
    public GameObject directShotPrefab;
    public float fireRate = 1.5f;
    public int projectileCount = 1;
    public float damage = 25f;
    public float projectileScale = 3f; // Atışın büyüklüğünü artırmak için

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireDirectShot();
            fireTimer = fireRate;
        }
    }

    void FireDirectShot()
    {
        if (directShotPrefab == null) return;

        float spreadAngle = 10f;
        float startAngle = -spreadAngle * (projectileCount - 1) / 2f;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + i * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
            
            Vector3 spawnPos = transform.position + Vector3.up * 1f;
            
            GameObject proj = Instantiate(directShotPrefab, spawnPos, rotation);
            proj.transform.localScale = Vector3.one * projectileScale; // Boyutunu büyüt
            
            DirectShotProjectile ds = proj.GetComponent<DirectShotProjectile>();
            if (ds != null)
            {
                ds.damage = this.damage;
            }
        }
    }
}
