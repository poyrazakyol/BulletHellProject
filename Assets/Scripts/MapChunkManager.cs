using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapChunkManager : MonoBehaviour
{
    public Transform player;
    
    [Header("Kamera Görüş Alanı (Kutu Sistemi)")]
    [Tooltip("16:9 ekranlar için sağ-sol genişliği daha fazla olmalıdır.")]
    public float activeDistanceX = 35f; 
    
    [Tooltip("Yukarı-aşağı görüş mesafesi.")]
    public float activeDistanceZ = 25f; 
    
    [Tooltip("İzometrik kamerada oyuncu ekranın altındayken yukarısı daha çok görünür. Bu değer kontrol merkezini oyuncunun Z ekseninde ilerisine taşır.")]
    public float zOffset = 10f; 

    [Header("Performans")]
    public float checkInterval = 0.5f;

    private List<GameObject> allChunks = new List<GameObject>();

    public void RegisterChunk(GameObject chunk)
    {
        allChunks.Add(chunk);
    }

    public void StartOptimization()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(OptimizeChunksRoutine());
    }

    private IEnumerator OptimizeChunksRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(checkInterval);

        while (true)
        {
            if (player != null)
            {
                
                Vector3 viewCenter = player.position + new Vector3(0, 0, zOffset);

                foreach (GameObject chunk in allChunks)
                {
                    if (chunk == null) continue;

                    
                    float deltaX = Mathf.Abs(chunk.transform.position.x - viewCenter.x);
                    float deltaZ = Mathf.Abs(chunk.transform.position.z - viewCenter.z);

                    
                    bool shouldBeActive = (deltaX <= activeDistanceX) && (deltaZ <= activeDistanceZ);

                    if (chunk.activeSelf != shouldBeActive)
                    {
                        chunk.SetActive(shouldBeActive);
                    }
                }
            }
            yield return wait;
        }
    }
}