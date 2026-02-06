using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Global access
    
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    void Awake()
    {
        // Singleton setup
        if (Instance == null) Instance = this;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore.ToString();
    }
}