using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningStrikeEffect : MonoBehaviour
{
    public float damage = 30f;
    public float aoeRadius = 3f;
    public float destroyDelay = 0.5f;

    void Start()
    {
        // Kapsama alanı (AoE) içindeki tüm düşmanlara hasar ver
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                DamageHelper.ApplyDamage(hit.gameObject, damage);
            }
        }

        // Görsel: LineRenderer ile zikzaklı yıldırım çiz
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.positionCount = 6;
            float height = 25f; // Gökten düşüş yüksekliği
            
            for (int i = 0; i < 6; i++)
            {
                float y = height - (height / 5f) * i;
                
                // İlk ve son nokta hariç biraz sağa sola saptır
                float xOffset = (i == 0 || i == 5) ? 0f : Random.Range(-1.5f, 1.5f);
                float zOffset = (i == 0 || i == 5) ? 0f : Random.Range(-1.5f, 1.5f);
                
                lr.SetPosition(i, transform.position + new Vector3(xOffset, y, zOffset));
            }
        }

        Destroy(gameObject, destroyDelay);
    }
}
