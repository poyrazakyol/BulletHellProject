using UnityEngine;
using TMPro;

public class StatsMenuManager : MonoBehaviour
{
    public GameObject statsPanel;
    public TextMeshProUGUI statsText;

    private PlayerHealth playerHealth;
    private PlayerMovement playerMove;
    private AutoShooter playerShooter;
    
    private bool isPaused = false;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerMove = player.GetComponent<PlayerMovement>();
            playerShooter = player.GetComponent<AutoShooter>();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStatsMenu();
        }
    }

    void ToggleStatsMenu()
    {
        
        isPaused = !isPaused;

        if (isPaused)
        {
            UpdateStatsUI(); 
            statsPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            statsPanel.SetActive(false);
            Time.timeScale = 1f; 
        }
    }

    void UpdateStatsUI()
    {
        if (playerHealth == null || statsText == null) return;
        
        float attacksPerSecond = 1f / playerShooter.fireRate;
        
        statsText.text = 
            $"<b>Health:</b> {playerHealth.currentHealth} / {playerHealth.maxHealth}\n\n" +
            $"<b>Move Speed:</b> {playerMove.moveSpeed}\n\n" +
            $"<b>Attack Speed:</b> Per Second {attacksPerSecond.ToString("F1")} Bullet\n\n" +
            $"<b>Range:</b> {playerShooter.maxRange} Metre\n\n";
    }
}