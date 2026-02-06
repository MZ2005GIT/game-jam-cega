using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across levels
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerLost()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void PlayerWon()
    {
        SceneManager.LoadScene("WinScene");
    }
}
