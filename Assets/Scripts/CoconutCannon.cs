using UnityEngine;
using UnityEngine.EventSystems;

public class CoconutCannon : MonoBehaviour
{
    public GameObject coconutPrefab;
    public float cooldown = 2f;
    private float currentCooldown;
    
    // Upgrades ile artacak özellikler
    public float damage = 40f;
    public float aoeRadius = 4f;

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        // Ekrana tıklanınca ateş et (Sadece bekleme süresi bittiyse)
        if (Input.GetMouseButtonDown(0) && currentCooldown <= 0f)
        {
            // UI üzerine tıklanıp tıklanmadığını kontrol et (Joystick vb. ile karışmasın)
            if (EventSystem.current != null)
            {
                // Mobil cihazlarda Touch kullanılıyorsa ona da bakmak gerekebilir
                // Ama standart Unity EventSystem mobilde de çalışır.
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                
                if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
            }

            FireCannon();
        }
    }

    void FireCannon()
    {
        // Kameradan ekrana tıklanan noktaya bir ışın (Ray) gönder
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Y=0 hizasında sanal bir düzlem (yer) oluştur
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        if (groundPlane.Raycast(ray, out float enterDistance))
        {
            // Işının düzlemle kesiştiği nokta hedef pozisyonumuzdur
            Vector3 targetPos = ray.GetPoint(enterDistance);

            if (coconutPrefab != null)
            {
                Vector3 spawnPos = transform.position + Vector3.up * 1f;
                GameObject coconut = Instantiate(coconutPrefab, spawnPos, Quaternion.identity);
                
                CoconutProjectile cp = coconut.GetComponent<CoconutProjectile>();
                if (cp != null)
                {
                    cp.targetPos = targetPos;
                    cp.damage = this.damage;
                    cp.aoeRadius = this.aoeRadius;
                }
                
                // Bekleme süresini sıfırla
                currentCooldown = cooldown; 
            }
        }
    }
}
