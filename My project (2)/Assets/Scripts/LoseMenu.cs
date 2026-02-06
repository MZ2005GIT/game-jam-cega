using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void RetryLevel()
    {
        // Reload the level without creating duplicate managers
        SceneManager.LoadScene("player testing");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
