using UnityEngine;

public class PoisonDropper : MonoBehaviour
{
    public GameObject poisonPrefab;
    public float dropRate = 3f; 
    private float dropTimer;

    void Update()
    {
        dropTimer -= Time.deltaTime;

        if (dropTimer <= 0f)
        {
            DropPoison();
            dropTimer = dropRate;
        }
    }

    void DropPoison()
    {
        Vector3 dropPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        Instantiate(poisonPrefab, dropPos, Quaternion.identity);
    }
}