using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public enum UpgradeType
{
    FireRate,
    MoveSpeed,
    OrbitWeapon,
    PoisonPool,
    MaxHealth
}


[System.Serializable]
public class UpgradeOption
{
    public string upgradeName;
    public string description;
    public UpgradeType type;
    public int maxLevel = 5;
    public int currentLevel = 0;
}

public class LevelUpManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject levelUpPanel;
    public Button[] upgradeButtons; 
    public TextMeshProUGUI[] buttonTexts; 

    [Header("Upgrade Pool")]
    public List<UpgradeOption> allUpgrades; 

    [Header("Player References")]
    public GameObject orbitWeapon;
    public PoisonDropper poisonDropper;
    
    private AutoShooter playerShooter;
    private PlayerMovement playerMove;
    private PlayerHealth playerHealth;

    
    private List<UpgradeOption> currentChoices = new List<UpgradeOption>();

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShooter = player.GetComponent<AutoShooter>();
            playerMove = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    public void ShowLevelUpMenu()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        
        List<UpgradeOption> availableUpgrades = new List<UpgradeOption>();
        foreach (var upgrade in allUpgrades)
        {
            if (upgrade.currentLevel < upgrade.maxLevel)
            {
                availableUpgrades.Add(upgrade);
            }
        }

        currentChoices.Clear();

        
        int choicesCount = Mathf.Min(3, availableUpgrades.Count);
        
        
        foreach (var btn in upgradeButtons) btn.gameObject.SetActive(false);

        for (int i = 0; i < choicesCount; i++)
        {
            
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            UpgradeOption chosen = availableUpgrades[randomIndex];
            
            
            availableUpgrades.RemoveAt(randomIndex); 
            currentChoices.Add(chosen);

            
            upgradeButtons[i].gameObject.SetActive(true);
            
            
            buttonTexts[i].text = $"{chosen.upgradeName} (Seviye {chosen.currentLevel + 1}/{chosen.maxLevel})\n<size=60%>{chosen.description}</size>";
        }

        
        if (choicesCount == 0)
        {
            ResumeGame(); 
        }
    }

    
    public void SelectUpgrade(int buttonIndex)
    {
        UpgradeOption selected = currentChoices[buttonIndex];
        selected.currentLevel++;

        ApplyUpgrade(selected.type);
        ResumeGame();
    }

    
    void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.FireRate:
                playerShooter.fireRate *= 0.85f; 
                break;
            case UpgradeType.MoveSpeed:
                playerMove.moveSpeed += 0.5f;
                break;
            case UpgradeType.OrbitWeapon:
                orbitWeapon.SetActive(true);
                
                break;
            case UpgradeType.PoisonPool:
                poisonDropper.enabled = true;
                
                break;
            case UpgradeType.MaxHealth:
                playerHealth.IncreaseMaxHealth(20f); 
                break;
        }
    }

    void ResumeGame()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}