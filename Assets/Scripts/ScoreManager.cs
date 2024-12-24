using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component
    public int score = 0; // The current score

    [Header("Settings")]
    public int pointsPerAction = 10; // Points added for each action (e.g., ball shot)

    void Start()
    {
        // Initialize the score display at the start
        UpdateScoreDisplay();
    }

    void Update()
    {
        // You can manually test score increase here
        if (Input.GetKeyDown(KeyCode.Space)) // Spacebar to add points for testing
        {
            AddScore(pointsPerAction);
        }
    }

    /// <summary>
    /// Adds points to the score and updates the display.
    /// </summary>
    public void AddScore(int points)
    {
        score += points; // Add points to the score
        UpdateScoreDisplay(); // Update the score display
    }

    /// <summary>
    /// Updates the score text display.
    /// </summary>
    void UpdateScoreDisplay()
    {
        scoreText.text = "" + score.ToString(); // Update the UI with the current score
    }

    /// <summary>
    /// Resets the score to zero and updates the display.
    /// </summary>
    public void ResetScore()
    {
        score = 0; // Reset the score
        UpdateScoreDisplay(); // Update the score display
    }
}
