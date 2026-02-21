using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : MonoBehaviour
{
    [Header("UI")]
    public Image xpBarFill;
    
    [Header("Level Settings")]
    public int level = 1;
    public float currentXP = 0f;
    public float maxXP = 100f; 
    public LevelUpManager levelUpManager; 

    
    public void AddXP(float amount)
    {
        currentXP += amount;
        
        
        if (xpBarFill != null)
        {
            xpBarFill.fillAmount = currentXP / maxXP;
        }
        
        
        if (currentXP >= maxXP)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentXP -= maxXP; 
        maxXP += 50f; 
        
        Debug.Log("Level Up! New Level: " + level);
        
        if (xpBarFill != null)
        {
            xpBarFill.fillAmount = currentXP / maxXP;
        }
        levelUpManager.ShowLevelUpMenu();
    }
}