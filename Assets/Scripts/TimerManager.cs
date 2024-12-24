using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI timerText; // Timer UI text
    public Slider timerSlider; // Timer slider
    public Image timerFillImage; // Fill image for the slider

    [Header("Settings")]
    public int timeLimit = 180; // Total time in seconds (180 seconds = 3 minutes)
    private float currentTime; // Current time in seconds

    private bool isTimerRunning = false; // Flag to check if the timer is running

    // Colors for time remaining
    private Color startColor = new Color(0f, 0.698f, 1f); // "00B2FF" - Blue
    private Color endColor = new Color(1f, 0.141f, 0f); // "FF2400" - Red

    void Start()
    {
        currentTime = timeLimit; // Set the current time to the time limit
        timerSlider.maxValue = timeLimit; // Set the slider max value to the time limit
        timerSlider.value = timeLimit; // Initialize the slider value
        timerFillImage.color = startColor; // Set the initial color to blue
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime; // Decrease time by delta time
            UpdateTimerDisplay(); // Update the timer display
            UpdateTimerSlider(); // Update the slider and its color

            if (currentTime <= 0) // If time is up, stop the timer
            {
                currentTime = 0;
                isTimerRunning = false;
                // Trigger game over or any other action
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
    /// Updates the timer UI text.
    /// </summary>
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the timer text
    }

    /// <summary>
    /// Updates the slider value and its fill color.
    /// </summary>
    private void UpdateTimerSlider()
    {
        timerSlider.value = currentTime; // Update the slider value

        // Calculate the normalized time (0 to 1)
        float normalizedTime = currentTime / timeLimit;

        // Interpolate the color based on the remaining time
        timerFillImage.color = Color.Lerp(endColor, startColor, normalizedTime);
    }

    /// <summary>
    /// Returns the current remaining time.
    /// </summary>
    public float GetCurrentTime()
    {
        return currentTime;
    }
}
