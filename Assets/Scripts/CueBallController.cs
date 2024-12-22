using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour
{
    // Reference to the Rigidbody component of the cue ball
    private Rigidbody rb;

    // Maximum force that can be applied to the ball
    public float maxForce = 1000f;

    // Boolean to check if the cue ball is stationary
    public bool isStationary => rb.velocity.magnitude < 0.1f;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Applies force to the cue ball in the given direction.
    /// </summary>
    /// <param name="direction">The direction in which to apply the force.</param>
    /// <param name="force">The magnitude of the force to apply (clamped to maxForce).</param>
    public void Shoot(Vector3 direction, float force)
    {
        if (isStationary)
        {
            // Clamp the force to the maximum limit
            force = Mathf.Clamp(force, 0, maxForce);

            // Apply force to the cue ball
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            Debug.Log($"Cue ball shot with force {force} in direction {direction}");
        }
        else
        {
            Debug.LogWarning("Cue ball is still moving. Wait until it stops to shoot again.");
        }
    }

    /// <summary>
    /// Resets the position and velocity of the cue ball.
    /// </summary>
    /// <param name="position">The position to reset the ball to.</param>
    public void ResetBall(Vector3 position)
    {
        // Stop the ball's movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Move the ball to the specified position
        transform.position = position;
    }

    /// <summary>
    /// Checks if the cue ball is moving or stopped.
    /// </summary>
    public bool IsStopped()
    {
        // If the ball's velocity is less than a small threshold, consider it stopped
        return rb.velocity.magnitude < 0.001f;
    }

    public bool IsShot()
    {
        // Check if the ball is moving or the shot has been made (e.g., based on velocity)
        return GetComponent<Rigidbody>().velocity.magnitude > 0.001f;
    }

}
