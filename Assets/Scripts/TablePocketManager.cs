using UnityEngine;
using System.Collections;

public class TablePocketManager : MonoBehaviour
{
    [Header("References")]
    public ScoreManager scoreManager; // Reference to the ScoreManager to update score
    public CueBallController cueBallController; // Reference to the CueBallController to reset the cue ball

    // Function that gets called when an object enters the pocket (trigger zone)
    void OnTriggerEnter(Collider other)
    {
        // Check if the object tagged as "Ball" enters the pocket
        if (other.CompareTag("Ball"))
        {
            // Call the function to add score
            BallInPocket(other);
        }
        // Check if the object tagged as "CueBall" enters the pocket
        else if (other.CompareTag("CueBall"))
        {
            // Call the function for cue ball (empty for now)
            CueBallInPocket(other);
        }
    }

    /// <summary>
    /// This function is called when a ball enters the pocket.
    /// </summary>
    /// <param name="ball">The ball collider that entered the pocket</param>
    void BallInPocket(Collider ball)
    {
        // Add points to the score when a ball falls into the pocket
        scoreManager.AddScore(10);

        // Optionally, you can destroy the ball or deactivate it after scoring
        //Destroy(ball.gameObject); // Destroy the ball (if needed)
        // Or you can deactivate the ball (if you want to reuse it later)
        // ball.gameObject.SetActive(false); 
    }

    /// <summary>
    /// This function is called when the cue ball enters the pocket.
    /// </summary>
    /// <param name="cueBall">The cue ball collider that entered the pocket</param>
    void CueBallInPocket(Collider cueBall)
    {
        // For now, this function is empty. You can implement future logic here.
        Debug.Log("Cue ball entered the pocket!");

        // Call the ResetBall function after 2 seconds delay
        StartCoroutine(ResetCueBallAfterDelay(2f));
    }

    /// <summary>
    /// Resets the cue ball position after a delay.
    /// </summary>
    /// <param name="delay">The delay before resetting the cue ball position.</param>
    /// <returns></returns>
    IEnumerator ResetCueBallAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        // Reset the cue ball position (X, Y, Z = 0.04, 0.8, 0.03)
        cueBallController.ResetBall(new Vector3(0.04f, 0.8f, 0.03f));
    }
}
