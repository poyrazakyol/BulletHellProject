using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class DungeonGenerator : MonoBehaviour
{
    [HideInInspector] 
    public List<Vector3> floorPositions = new List<Vector3>(); 
    
    [Header("Player References")]
    public Transform player; 
    
    [Header("Dungeon Settings")]
    public int width = 50; 
    public int height = 50; 
    
    [Range(0, 100)]
    public int randomFillPercent = 45; 

    public string seed; 
    public bool useRandomSeed = true;

    [Header("Prefabs")]
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    int[,] map; 

    void Start()
    {
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

        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector3(-width / 2 + x, 0, -height / 2 + y);
                    
                    if (map[x, y] == 1)
                    {
                        Instantiate(wallPrefab, pos, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        Instantiate(floorPrefab, pos, Quaternion.identity, this.transform);
                        
                        
                        floorPositions.Add(new Vector3(pos.x, 1f, pos.z));
                    }
                }
            }
        }
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
                    
                    Vector3 safePosition = new Vector3(-width / 2 + x, 1f, -height / 2 + y);
                    player.position = safePosition;
                    return; 
                }
            }
        }
    }
}