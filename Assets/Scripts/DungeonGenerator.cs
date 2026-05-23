using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class DungeonGenerator : MonoBehaviour
{
    [HideInInspector] 
    public List<Vector3> floorPositions = new List<Vector3>(); 
    
    [HideInInspector]
    public List<Vector3> treePositions = new List<Vector3>(); 
    
    [Header("Player References")]
    public Transform player;
    
    
    [Header("Dungeon Settings")]
    public int width = 50; 
    public int height = 50; 
    
    [Range(0, 100)]
    public int randomFillPercent = 45; 

    public string seed; 
    public bool useRandomSeed = true;

    [Header("Environment Prefabs & Materials")]
    public GameObject wallPrefab;     
    public GameObject[] treePrefabs;
    public float minTreeDistance = 2.5f;
    
    [Range(0f, 1f)]
    public float treeSpawnChance = 0.05f; 

    public GameObject floorPrefab;   
    public Material[] floorMaterials; 

    [Header("Optimization")]
    public int chunkSize = 10; 
    private MapChunkManager chunkManager; 

    int[,] map; 

    void Start()
    {
        chunkManager = GetComponent<MapChunkManager>();
        if (chunkManager == null) chunkManager = gameObject.AddComponent<MapChunkManager>();

        GenerateDungeon();
    }
    
    void GenerateDungeon()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        CleanIsolatedRooms(); 

        DrawDungeon(); 
        PlacePlayer(); 
        
        
        GetComponent<NavMeshSurface>().BuildNavMesh(); 

        
        BatchAllChunks();

        
        chunkManager.StartOptimization();
    }

    
    void BatchAllChunks()
    {
        int chunkCountX = Mathf.CeilToInt((float)width / chunkSize);
        int chunkCountY = Mathf.CeilToInt((float)height / chunkSize);

        for (int cx = 0; cx < chunkCountX; cx++)
        {
            for (int cy = 0; cy < chunkCountY; cy++)
            {
                Transform chunk = transform.Find($"Chunk_{cx}_{cy}");
                if (chunk != null)
                {
                    StaticBatchingUtility.Combine(chunk.gameObject);
                }
            }
        }
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.Ticks.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }
    
    void SmoothMap()
    {
        int[,] newMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    newMap[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    newMap[x, y] = 0;
                else
                    newMap[x, y] = map[x, y]; 
            }
        }
        map = newMap;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++; 
                }
            }
        }
        return wallCount;
    }
    
void DrawDungeon()
    {
        floorPositions.Clear(); 
        treePositions.Clear(); 

        if (map != null)
        {
            int chunkCountX = Mathf.CeilToInt((float)width / chunkSize);
            int chunkCountY = Mathf.CeilToInt((float)height / chunkSize);
            GameObject[,] chunkParents = new GameObject[chunkCountX, chunkCountY];

            for (int cx = 0; cx < chunkCountX; cx++)
            {
                for (int cy = 0; cy < chunkCountY; cy++)
                {
                    chunkParents[cx, cy] = new GameObject($"Chunk_{cx}_{cy}");
                    chunkParents[cx, cy].transform.SetParent(this.transform);
                    
                    float centerX = -width / 2 + (cx * chunkSize) + (chunkSize / 2f);
                    float centerZ = -height / 2 + (cy * chunkSize) + (chunkSize / 2f);
                    chunkParents[cx, cy].transform.position = new Vector3(centerX, 0, centerZ);

                    chunkManager.RegisterChunk(chunkParents[cx, cy]);
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector3(-width / 2 + x, 0, -height / 2 + y);
                    
                    int currentChunkX = x / chunkSize;
                    int currentChunkY = y / chunkSize;
                    Transform currentChunkParent = chunkParents[currentChunkX, currentChunkY].transform;
                    
                    if (map[x, y] == 1) 
                    {
                        if (wallPrefab != null) 
                        {
                            GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, currentChunkParent);

                            float randomHeight = UnityEngine.Random.Range(1.2f, 3.5f);
                            wall.transform.localScale = new Vector3(1f, randomHeight, 1f);
                            wall.transform.position = new Vector3(pos.x, (randomHeight / 2f) - 0.5f, pos.z);
                        }
                    }
                    else
                    {
                        GameObject floor = Instantiate(floorPrefab, pos, Quaternion.identity, currentChunkParent);
                        
                        if (floorMaterials.Length > 0)
                        {
                            int randomMatIndex = UnityEngine.Random.Range(0, floorMaterials.Length);
                            Renderer floorRenderer = floor.GetComponentInChildren<Renderer>();
                            if (floorRenderer != null)
                            {
                                floorRenderer.material = floorMaterials[randomMatIndex];
                            }
                        }
                        
                        Vector3 floorPos = new Vector3(pos.x, 1f, pos.z);
                        floorPositions.Add(floorPos);
                        
                        if (treePrefabs.Length > 0 && UnityEngine.Random.value <= treeSpawnChance)
                        {
                            Vector3 treePos = new Vector3(pos.x, 0f, pos.z); 
                            
                            if (CanSpawnTreeAt(treePos))
                            {
                                int randomIndex = UnityEngine.Random.Range(0, treePrefabs.Length);
                                GameObject tree = Instantiate(treePrefabs[randomIndex], treePos, Quaternion.identity, currentChunkParent);
                                
                                tree.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0);
                                
                                
                                treePositions.Add(treePos); 
                            }
                        }
                    }
                }
            }
        }
    }

    
    bool CanSpawnTreeAt(Vector3 targetPos)
    {
        foreach (Vector3 existingPos in treePositions)
        {
            
            if (Vector3.Distance(targetPos, existingPos) < minTreeDistance)
            {
                return false; 
            }
        }
        return true; 
    }
    
    void CleanIsolatedRooms()
    {
        int startX = width / 2;
        int startY = height / 2;
        bool foundStart = false;

        for (int x = startX; x < width; x++)
        {
            for (int y = startY; y < height; y++)
            {
                if (map[x, y] == 0)
                {
                    startX = x;
                    startY = y;
                    foundStart = true;
                    break;
                }
            }
            if (foundStart) break;
        }

        bool[,] visited = new bool[width, height];
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        
        queue.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            
            foreach (Vector2Int dir in directions)
            {
                int nx = current.x + dir.x;
                int ny = current.y + dir.y;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    if (map[nx, ny] == 0 && !visited[nx, ny])
                    {
                        visited[nx, ny] = true; 
                        queue.Enqueue(new Vector2Int(nx, ny));
                    }
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0 && !visited[x, y])
                {
                    map[x, y] = 1; 
                }
            }
        }
    }
    
    void PlacePlayer()
    {
        if (player == null) return;

        int startX = width / 2;
        int startY = height / 2;

        for (int x = startX; x < width; x++)
        {
            for (int y = startY; y < height; y++)
            {
                
                if (map[x, y] == 0) 
                {
                    Vector3 potentialPosition = new Vector3(-width / 2 + x, 1f, -height / 2 + y);
                    
                    
                    if (!treePositions.Contains(potentialPosition))
                    {
                        player.position = potentialPosition;
                        return; 
                    }
                }
            }
        }
    }
}