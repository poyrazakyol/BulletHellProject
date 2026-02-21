using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBarFill; 
    public GameObject gameOverPanel; 

    void Start()
    {
        
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        
        Camera.main.GetComponent<CameraFollow>().TriggerShake(0.2f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("OYUN BİTTİ! ÖLDÜN.");
        
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        
        Time.timeScale = 0f; 
    }

    
    public void RestartGame()
    {
        
        Time.timeScale = 1f; 
        
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}