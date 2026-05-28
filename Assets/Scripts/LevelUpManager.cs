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
    MaxHealth,
    Boomerang,
    LightningStrike,
    DirectShot,
    Shield,
    MeteorStorm,
    CoconutCannon,
    GravityVortex
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
    
    // --- YENİ EKLENEN KISIM ---
    public GameObject mobileTouchZone; // Joystick panelini buraya bağlayacağız
    // --------------------------

    [Header("Upgrade Pool")]
    public List<UpgradeOption> allUpgrades; 

    [Header("Player References")]
    public GameObject orbitWeapon;
    public PoisonDropper poisonDropper;
    
    private AutoShooter playerShooter;
    private PlayerMovement playerMove;
    private PlayerHealth playerHealth;

<<<<<<< Updated upstream
    
=======
    // YENİ SİLAHLAR İÇİN REFERANSLAR
    private BoomerangShooter boomerangWeapon;
    private LightningShooter lightningWeapon;
    private DirectShotShooter directShotWeapon;
    private ShieldWeapon shieldWeapon;
    private MeteorShooter meteorWeapon;
    private CoconutCannon coconutWeapon;
    private GravityVortexShooter gravityWeapon;

>>>>>>> Stashed changes
    private List<UpgradeOption> currentChoices = new List<UpgradeOption>();

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShooter = player.GetComponent<AutoShooter>();
            playerMove = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();

            // Yeni silah bileşenlerini bul (Oyuncunun üstünde disabled olarak durduklarını varsayıyoruz)
            boomerangWeapon = player.GetComponent<BoomerangShooter>();
            lightningWeapon = player.GetComponent<LightningShooter>();
            directShotWeapon = player.GetComponent<DirectShotShooter>();
            shieldWeapon = player.GetComponentInChildren<ShieldWeapon>(true); // (true) ile kapalı olan çocuk objelerdeki componentleri de buluruz!
            meteorWeapon = player.GetComponent<MeteorShooter>();
            coconutWeapon = player.GetComponent<CoconutCannon>();
            gravityWeapon = player.GetComponent<GravityVortexShooter>();
        }
    }

    public void ShowLevelUpMenu()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        // --- YENİ EKLENEN KISIM ---
        // Menü açıldığında görünmez joystick algılayıcısını kapat
        if (mobileTouchZone != null)
        {
            mobileTouchZone.SetActive(false);
        }
        // --------------------------

        
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
            case UpgradeType.Boomerang:
                if (boomerangWeapon != null)
                {
                    if (!boomerangWeapon.enabled) boomerangWeapon.enabled = true;
                    else boomerangWeapon.boomerangCount++;
                }
                break;
            case UpgradeType.LightningStrike:
                if (lightningWeapon != null)
                {
                    if (!lightningWeapon.enabled) lightningWeapon.enabled = true;
                    else { lightningWeapon.targetCount++; lightningWeapon.damage += 10f; }
                }
                break;
            case UpgradeType.DirectShot:
                if (directShotWeapon != null)
                {
                    if (!directShotWeapon.enabled) directShotWeapon.enabled = true;
                    else directShotWeapon.projectileCount++;
                }
                break;
            case UpgradeType.Shield:
                if (shieldWeapon != null)
                {
                    if (!shieldWeapon.gameObject.activeSelf) 
                    {
                        shieldWeapon.gameObject.SetActive(true);
                        shieldWeapon.enabled = true;
                    }
                    else 
                    {
                        shieldWeapon.UpdateStats(shieldWeapon.damageReductionPercent + 0.05f, shieldWeapon.tickDamage + 5f);
                    }
                }
                break;
            case UpgradeType.MeteorStorm:
                if (meteorWeapon != null)
                {
                    if (!meteorWeapon.enabled) meteorWeapon.enabled = true;
                    else { meteorWeapon.meteorCount++; meteorWeapon.damage += 20f; }
                }
                break;
            case UpgradeType.CoconutCannon:
                if (coconutWeapon != null)
                {
                    if (!coconutWeapon.enabled) coconutWeapon.enabled = true;
                    else { coconutWeapon.damage += 20f; coconutWeapon.aoeRadius += 1f; }
                }
                break;
            case UpgradeType.GravityVortex:
                if (gravityWeapon != null)
                {
                    if (!gravityWeapon.enabled) gravityWeapon.enabled = true;
                    else { gravityWeapon.pullRadius += 1f; gravityWeapon.tickDamage += 3f; }
                }
                break;
        }
    }

    void ResumeGame()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;

        // --- YENİ EKLENEN KISIM ---
        // Oyuna geri dönüldüğünde görünmez joystick algılayıcısını tekrar aç
        if (mobileTouchZone != null)
        {
            mobileTouchZone.SetActive(true);
        }
        // --------------------------
    }
}