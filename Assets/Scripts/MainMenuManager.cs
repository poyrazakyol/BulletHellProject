using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        
        Time.timeScale = 1f; 
        
        
        SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        
        Application.Quit(); 
    }
}