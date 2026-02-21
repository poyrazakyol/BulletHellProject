using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [Header("Unlockable Weapons")]
    public GameObject orbitWeapon;
    
    [Header("User Interface")]
    public GameObject levelUpPanel;

    private AutoShooter playerShooter;
    private PlayerMovement playerMove;

    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerShooter = player.GetComponent<AutoShooter>();
        playerMove = player.GetComponent<PlayerMovement>();
    }

    public void ShowLevelUpMenu()
    {
        levelUpPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    
    public void UpgradeFireRate()
    {
        
        playerShooter.fireRate *= 0.8f; 
        ResumeGame();
    }

    
    public void UpgradeMoveSpeed()
    {
        playerMove.moveSpeed += 1f; 
        ResumeGame();
    }

    
    public void UpgradeRange()
    {
        playerShooter.maxRange += 2f; 
        ResumeGame();
    }
    
    
    public void UnlockOrbitWeapon()
    {
        if (orbitWeapon != null)
        {
            orbitWeapon.SetActive(true); 
        }
        ResumeGame();
    }

    
    void ResumeGame()
    {
        levelUpPanel.SetActive(false); 
        Time.timeScale = 1f; 
    }
}