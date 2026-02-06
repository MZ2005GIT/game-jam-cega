using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioClip coinSound;
    public AudioClip loseHealthSound;
    public AudioClip loseSound;
    public AudioClip potionSound;
    public AudioClip bulletSound;
    public AudioClip winSound;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCoinSound()
    {
        sfxSource.PlayOneShot(coinSound);
    }
    public void PlayLoseHealthSound()
    {
        sfxSource.PlayOneShot(loseHealthSound);
    }
    public void PlayLostSound()
    {
        sfxSource.PlayOneShot(loseSound);
    }
    public void PlayWonSound()
    {
        sfxSource.PlayOneShot(winSound);
    }
    public void PlayPotionSound()
    {
        sfxSource.PlayOneShot(potionSound);
    }
    public void PlayBulletSound()
    {
        sfxSource.PlayOneShot(bulletSound);
    }
}
