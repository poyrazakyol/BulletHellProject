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
    public RectTransform healthBarBackground;
    
    
    private float initialBarWidth;
    private float initialMaxHealth;

    void Start()
    {
        currentHealth = maxHealth;
        
        if (healthBarBackground != null)
        {
            initialBarWidth = healthBarBackground.sizeDelta.x;
            initialMaxHealth = maxHealth;
        }
        
        UpdateHealthBar();
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX(SoundManager.instance.playerHitSFX);
        
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
        
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX(SoundManager.instance.uiClickSFX);
        
        Time.timeScale = 1f; 
        
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void IncreaseMaxHealth(float bonusAmount)
    {
        maxHealth += bonusAmount;
        currentHealth += bonusAmount; 
        
        UpdateHealthBarSize(); 
        UpdateHealthBar();     
    }
    void UpdateHealthBarSize()
    {
        if (healthBarBackground != null)
        {
            float newWidth = (maxHealth / initialMaxHealth) * initialBarWidth;
            healthBarBackground.sizeDelta = new Vector2(newWidth, healthBarBackground.sizeDelta.y);
        }
    }
}