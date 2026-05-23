using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource; 
    public AudioSource sfxSource; 

    [Header("Müzik Dosyaları")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    [Header("Ses Efektleri")] 
    public AudioClip uiClickSFX;
    public AudioClip levelUpSFX;
    public AudioClip playerAttackSFX;
    public AudioClip playerHitSFX;

    [Range(0f, 1f)]
    public float musicVolume = 0.5f; 
    [Range(0f, 1f)]
    public float sfxVolume = 0.7f;   

    private bool isMuted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        bgmSource.volume = musicVolume;
        sfxSource.volume = sfxVolume; 
        
        PlayMenuMusic();
    }

    
    public void PlayMenuMusic()
    {
        if (bgmSource.clip == menuMusic) return; 
        bgmSource.clip = menuMusic;
        bgmSource.Play();
    }

    public void PlayGameMusic()
    {
        if (bgmSource.clip == gameMusic) return;
        bgmSource.clip = gameMusic;
        bgmSource.Play();
    }

    
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    
    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}