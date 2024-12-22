using UnityEngine;
using Cinemachine;

public class CinematicCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera topDownCamera; // Top-down camera for the regular view
    public CinemachineVirtualCamera cinematicCamera; // Camera for cinematic shots
    public CueBallController cueBall; // Reference to the cue ball controller
    public float cinematicDuration = 2f; // Duration of the cinematic shot follow

    private bool isCinematicActive = false; // Whether the cinematic mode is active
    private float cinematicTimer = 0f; // Timer for cinematic duration

    void Update()
    {
        // If a shot is taken, start the cinematic sequence
        if (cueBall.IsShot() && !isCinematicActive)
        {
            StartCinematic();
        }

        // If cinematic mode is active, track the cue ball
        if (isCinematicActive)
        {
            cinematicTimer += Time.deltaTime;
            // When the cinematic time is over, return to top-down view
            if (cinematicTimer >= cinematicDuration)
            {
                EndCinematic();
            }
        }
    }

    /// <summary>
    /// Starts the cinematic sequence.
    /// </summary>
    void StartCinematic()
    {
        // Switch to the cinematic camera
        cinematicCamera.gameObject.SetActive(true);
        topDownCamera.gameObject.SetActive(false);

        // Make sure the virtual camera follows the cue ball
        cinematicCamera.m_Follow = cueBall.transform;
        cinematicCamera.m_LookAt = cueBall.transform;

        // Set cinematic active flag
        isCinematicActive = true;
        cinematicTimer = 0f; // Reset the timer
    }

    /// <summary>
    /// Ends the cinematic sequence and returns to the top-down view.
    /// </summary>
    void EndCinematic()
    {
        // Switch back to the top-down camera
        cinematicCamera.gameObject.SetActive(false);
        topDownCamera.gameObject.SetActive(true);

        // Reset the virtual camera follow and lookAt to null
        topDownCamera.m_Follow = null;
        topDownCamera.m_LookAt = null;

        // Set cinematic mode inactive
        isCinematicActive = false;
        cinematicTimer = 0f; // Reset the timer
    }
}
