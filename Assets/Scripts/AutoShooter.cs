using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [Header("Magic Settings")]
    public GameObject projectilePrefab; 
    public Transform firePoint;
    public float fireRate = 0.5f; 
    public float maxRange = 8f; 
    
    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireMagic();
            fireTimer = fireRate; 
        }
    }

    void FireMagic()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity; 

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= maxRange)
        {
            Vector3 directionToEnemy = (nearestEnemy.transform.position - firePoint.position).normalized;
            Quaternion projectileRotation = Quaternion.LookRotation(directionToEnemy);
            
            // --- ATEŞ ETME SESİ BURADA TETİKLENİYOR ---
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.playerAttackSFX);
            }
            
            ProjectilePool.Instance.GetProjectile(firePoint.position, projectileRotation);
        }
    }
}