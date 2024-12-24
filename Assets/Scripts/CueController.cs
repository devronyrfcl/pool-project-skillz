using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CueController : MonoBehaviour
{
    [Header("References")]
    public Transform cueStick; // The cue stick object
    public Transform stickObject; // The stick object that moves back and forth
    public Slider powerSlider; // The slider to adjust shot power
    public CueBallController cueBall; // Reference to the cue ball controller
    public Camera mainCamera; // Main camera for raycasting
    public AudioClip CueHitSound; //Cue Hit Sound
    public AudioSource audioSource; // Audio source for playing sounds

    [Header("Settings")]
    public float maxPower = 1000f; // Maximum power for the shot
    public float stickMoveDistance = 0.5f; // Maximum distance the stickObject moves backward
    public float springSpeed = 10f; // Speed at which the slider and stickObject reset
    public RectTransform blockArea; // The RectTransform area where raycasting is blocked
    public GameObject[] normalBalls; // Array of normal balls

    private bool isAdjustingPower = false; // Whether the player is adjusting the power
    private Vector3 stickInitialPosition; // Original position of the stickObject
    private Vector3 cueBallStartPos; // Position of cue ball at the time of shot
    public bool AllowStickToRotate;

    void Start()
    {
        // Save the initial position of the stickObject
        stickInitialPosition = stickObject.localPosition;

        // Initialize the power slider
        powerSlider.value = 1; // Start at maximum power (slider reversed)
        powerSlider.onValueChanged.AddListener(OnPowerChanged);

        AllowStickToRotate = true;
        powerSlider.interactable = true; // Make sure the slider is interactable at the start
    }

    void Update()
    {
        HandleCueStickRotation();
        HandlePowerSlider();
        FollowCueBallWhenStopped(); // Call this function every frame
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " cueBall");
    }

    /// <summary>
    /// Rotates the cue stick based on raycast from the camera.
    /// </summary>
    void HandleCueStickRotation()
    {
        if (Input.GetMouseButton(0) && AllowStickToRotate) // Adjust rotation while holding left mouse button
        {
            // Check if the mouse position is inside the blocked area
            if (IsMouseInBlockedArea())
            {
                return; // Skip raycasting if inside the blocked area
            }

            // Perform raycast if not in the blocked area
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Vector3 direction = hit.point - transform.position;
                direction.y = 0; // Keep the rotation on the horizontal plane
                transform.forward = direction.normalized;
            }
        }
    }

    /// <summary>
    /// Determines if the mouse position is inside the blocked area.
    /// </summary>
    /// <returns>True if mouse is inside the blocked area, false otherwise.</returns>
    bool IsMouseInBlockedArea()
    {
        Vector2 localMousePosition = blockArea.InverseTransformPoint(Input.mousePosition);
        return blockArea.rect.Contains(localMousePosition);
    }

    /// <summary>
    /// Handles the power slider adjustment and shooting.
    /// </summary>
    void HandlePowerSlider()
    {
        if (Input.GetMouseButton(0)) // Start adjusting power
        {
            isAdjustingPower = true;
        }

        if (Input.GetMouseButtonUp(0) && isAdjustingPower) // Shoot when releasing the slider
        {
            Shoot();
            isAdjustingPower = false;
        }

        if (!isAdjustingPower && powerSlider.value < 1) // Reset the slider and stickObject
        {
            powerSlider.value = Mathf.MoveTowards(powerSlider.value, 1, springSpeed * Time.deltaTime);
            AllowStickToRotate = false;
        }
    }

    /// <summary>
    /// Adjusts the position of the stickObject based on the slider value.
    /// </summary>
    /// <param name="value">Current slider value.</param>
    void OnPowerChanged(float value)
    {
        AllowStickToRotate = false;
        // Move the stickObject backward based on the reversed slider value
        float adjustedValue = 1 - value; // Reverse the slider value
        stickObject.localPosition = stickInitialPosition - new Vector3(0, 0, adjustedValue * stickMoveDistance);
    }

    /// <summary>
    /// Shoots the cue ball with the power from the slider.
    /// </summary>
    void Shoot()
    {
        // Disable the slider after the shot
        powerSlider.interactable = false;

        float force = (1 - powerSlider.value) * maxPower; // Reverse the slider value for power
        Vector3 shootDirection = transform.forward;

        // Apply force to the cue ball
        cueBall.Shoot(shootDirection, force);

        //Play Cue Hit sound
        audioSource.PlayOneShot(CueHitSound);

        // Store the cue ball's position when the shot is made
        cueBallStartPos = cueBall.transform.position;

        // Reset the stickObject position
        stickObject.localPosition = stickInitialPosition;
    }

    /// <summary>
    /// Makes cue stick follow the cue ball as it stops moving.
    /// </summary>
    private void FollowCueBallWhenStopped()
    {
        bool allNormalBallsStopped = true;

        // Check if all normal balls have stopped moving
        foreach (GameObject normalBall in normalBalls)
        {
            Rigidbody rb = normalBall.GetComponent<Rigidbody>();
            if (rb != null && rb.velocity.magnitude > 0)
            {
                allNormalBallsStopped = false;
                break;
            }
        }

        if (allNormalBallsStopped && cueBall.IsStopped())
        {
            // Once all normal balls and the cue ball stop, follow the cue ball
            cueStick.position = cueBall.transform.position;
            AllowStickToRotate = true; // Allow rotation again
            powerSlider.interactable = true; // Make the slider interactable again
        }
    }
}
