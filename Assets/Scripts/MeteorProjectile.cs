using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPos;
    [HideInInspector] public float damage = 50f;
    [HideInInspector] public float aoeRadius = 5f;
    
    public float speed = 40f;
    
    [Header("Assets")]
    public GameObject explosionEffectPrefab;
    public GameObject poisonPoolPrefab; // Normal PoisonPool.prefab atanmalı

    void Update()
    {
        // Hızlıca aşağıya düş
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Hedefe ulaştıysa patla
        if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
        {
            Explode();
        }
    }

    void Explode()
    {
        // 1. AoE Hasar
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                DamageHelper.ApplyDamage(hit.gameObject, damage);
            }
        }

        // 2. Patlama Efekti ve Kamera Sarsıntısı
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        
        if (Camera.main != null)
        {
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null) camFollow.TriggerShake(0.3f);
        }

        // 3. Kırmızı Zehir Alanı (Red Poison Area) Oluştur
        if (poisonPoolPrefab != null)
        {
            Vector3 poolPos = new Vector3(targetPos.x, 0.1f, targetPos.z);
            GameObject pool = Instantiate(poisonPoolPrefab, poolPos, Quaternion.identity);
            
            // Havuzun rengini programatik olarak kırmızıya çevir
            Renderer[] renderers = pool.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in renderers)
            {
                foreach(Material m in r.materials)
                {
                    if (m.HasProperty("_Color"))
                        m.color = Color.red;
                    else if (m.HasProperty("_BaseColor"))
                        m.SetColor("_BaseColor", Color.red);
                }
            }
            
            // Zehir hasarını/süresini artır (Meteorun bıraktığı havuz daha güçlüdür)
            PoisonDamage pd = pool.GetComponent<PoisonDamage>();
            if (pd != null)
            {
                pd.damagePerSecond = 25f; 
                pd.lifetime = 4f;
            }
        }

        Destroy(gameObject);
    }
}
