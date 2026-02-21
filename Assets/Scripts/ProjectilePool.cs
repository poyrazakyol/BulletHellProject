using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolSize = 50; 
    
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false); 
            pool.Enqueue(obj);    
        }
    }

    
    public GameObject GetProjectile(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true); 
            return obj;
        }
        else
        {
            
            GameObject obj = Instantiate(projectilePrefab, position, rotation);
            return obj;
        }
    }

    
    public void ReturnProjectile(GameObject obj)
    {
        obj.SetActive(false); 
        pool.Enqueue(obj);    
    }
}