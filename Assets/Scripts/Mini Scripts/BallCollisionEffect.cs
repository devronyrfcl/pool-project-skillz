using UnityEngine;

public class BallCollisionEffect : MonoBehaviour
{
    [Header("References")]
    public AudioClip collisionSound; // Sound for collision between balls
    public AudioClip collisionWithTableSound; // Sound for collision with table border

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Ball" tag
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Ensure the collision has a Rigidbody and Collider (check if it's another ball)
            if (collision.relativeVelocity.magnitude > 0)
            {
                // Play the collision sound for balls
                PlayCollisionSound(collisionSound);
            }
        }

        // Check if the collided object has the "TableBorder" tag
        else if (collision.gameObject.CompareTag("TableBorder"))
        {
            // Play the sound for collision with the table border
            PlayCollisionSound(collisionWithTableSound);
        }
    }

    /// <summary>
    /// Plays the collision sound at the point of collision.
    /// </summary>
    /// <param name="soundToPlay">The sound to play</param>
    private void PlayCollisionSound(AudioClip soundToPlay)
    {
        // Play the selected sound at the point of collision
        if (soundToPlay != null)
        {
            // This plays the sound at the collision point in world space
            AudioSource.PlayClipAtPoint(soundToPlay, transform.position);
        }
    }
}
