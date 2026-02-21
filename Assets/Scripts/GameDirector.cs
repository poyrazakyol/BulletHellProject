using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [Header("Time and Difficulty")]
    public float timePassed = 0f;
    public float difficultyIncreaseInterval = 30f; 
    private float nextDifficultySpike;

    [Header("Enemy Status")]
    public float currentEnemyHealth = 30f;
    public float currentEnemySpeed = 3.5f;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    private EnemySpawner spawner;

    void Start()
    {
        spawner = GetComponent<EnemySpawner>();
        nextDifficultySpike = difficultyIncreaseInterval;
    }

    void Update()
    {
        
        if (Time.timeScale > 0)
        {
            timePassed += Time.deltaTime;
            UpdateTimerUI();

            
            if (timePassed >= nextDifficultySpike)
            {
                IncreaseDifficulty();
                nextDifficultySpike += difficultyIncreaseInterval; 
            }
        }
    }

    void IncreaseDifficulty()
    {
        
        currentEnemyHealth += 10f; 
        currentEnemySpeed += 0.2f;
        
        if (spawner.spawnRate > 0.2f)
        {
            spawner.spawnRate -= 0.1f;
        }

        Debug.Log($"Difficulty Increased! New Health: {currentEnemyHealth} | New Speed: {currentEnemySpeed} | New Spawn Rate: {spawner.spawnRate}");
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            
            int minutes = Mathf.FloorToInt(timePassed / 60F);
            int seconds = Mathf.FloorToInt(timePassed - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}