using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI timerText; // Reference to the timer UI text
    public int timeLimit = 180; // Total time in seconds (180 seconds = 3 minutes)
    private float currentTime; // Current time in seconds

    private bool isTimerRunning = false; // Flag to check if the timer is running

    void Start()
    {
        currentTime = timeLimit; // Set the current time to the time limit
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime; // Decrease time by delta time
            UpdateTimerDisplay(); // Update the timer display

            if (currentTime <= 0) // If time is up, trigger game over
            {
                currentTime = 0;
                isTimerRunning = false;
                // You can call a method on the GameManager to handle game over
                // Example: gameManager.EndGame();
            }
        }
    }

    /// <summary>
    /// Starts the timer countdown.
    /// </summary>
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    /// <summary>
    /// Returns the current remaining time.
    /// </summary>
    public float GetCurrentTime()
    {
        return currentTime;
    }

    /// <summary>
    /// Updates the timer UI text.
    /// </summary>
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the timer display
    }
}
