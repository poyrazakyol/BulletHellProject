using UnityEngine;

public class CoconutProjectile : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPos;
    [HideInInspector] public float damage = 40f;
    [HideInInspector] public float aoeRadius = 4f;
    
    public float speed = 15f;
    public float arcHeight = 3f;
    
    [Header("Visuals")]
    public GameObject explosionEffectPrefab;
    
    private Vector3 startPos;
    private float progress = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Yüksek kavisli parabol hareketi hesaplaması
        float distance = Vector3.Distance(startPos, targetPos);
        if (distance == 0) return;

        progress += (speed / distance) * Time.deltaTime;
        progress = Mathf.Clamp01(progress);

        // Doğrusal pozisyon
        Vector3 currentPos = Vector3.Lerp(startPos, targetPos, progress);
        
        // Kavis ekleme (Parabol Formülü: 4 * h * x * (1-x))
        float heightOffset = 4f * arcHeight * progress * (1f - progress);
        currentPos.y += heightOffset;

        transform.position = currentPos;
        
        // Kendi etrafında takla atma efekti (Hindistan cevizi spin atsın)
        transform.Rotate(360f * Time.deltaTime, 0, 0);

        if (progress >= 1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                DamageHelper.ApplyDamage(hit.gameObject, damage);
            }
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
