using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public TimerManager timerManager; // Reference to TimerManager
    public ScoreManager scoreManager; // Reference to ScoreManager
    public TextMeshProUGUI gameOverText; // Reference to Game Over Text UI
    public int BallInThePocket = 0; // Number of balls pocketed

    [Header("Settings")]
    public int maxBallInThePocket = 7; // Maximum number of balls in the pocket to end the game

    private bool isGameOver = false; // Flag to track if the game is over

    void Start()
    {
        gameOverText.gameObject.SetActive(false); // Hide the game over text at start
        StartGame();
    }

    void Update()
    {
        // Check if game over conditions are met
        if (BallInThePocket >= maxBallInThePocket || timerManager.GetCurrentTime() <= 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Starts the game and initializes necessary components.
    /// </summary>
    public void StartGame()
    {
        BallInThePocket = 0; // Reset balls pocketed
        scoreManager.ResetScore(); // Reset score
        timerManager.StartTimer(); // Start the timer
        isGameOver = false; // Game is not over at the start
    }

    /// <summary>
    /// Ends the game when conditions are met (time up or max balls pocketed).
    /// </summary>
    public void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverText.gameObject.SetActive(true); // Show game over text
            gameOverText.text = "Game Over!"; // Display the game over text
            timerManager.StopTimer(); // Stop the timer
        }
    }

    /// <summary>
    /// Increments the number of balls pocketed.
    /// </summary>
    public void IncreaseBallInThePocket()
    {
        if (!isGameOver)
        {
            BallInThePocket++;
        }
    }
}
