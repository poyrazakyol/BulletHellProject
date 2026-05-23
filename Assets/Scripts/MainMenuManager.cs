using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX(SoundManager.instance.uiClickSFX);

        Time.timeScale = 1f; 
        
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayGameMusic();
        }
        
        SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); 
    }
    
    public void AudioButtonPressed()
    {
        if (SoundManager.instance != null)
        {
           
            SoundManager.instance.PlaySFX(SoundManager.instance.uiClickSFX);
            SoundManager.instance.ToggleMute();
        }
    }
}