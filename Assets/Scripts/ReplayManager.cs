using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject[] balls; // Balls to track
    private List<Vector3>[] recordedPositions; // Positions recorded for each ball
    private List<Quaternion>[] recordedRotations; // Rotations recorded for each ball
    private List<Vector3>[] recordedVelocities; // Velocities recorded for each ball

    private bool isRecording = false;
    private bool isReplaying = false;
    private float replayTime = 0f;
    private float replaySpeed = 1f;

    void Start()
    {
        recordedPositions = new List<Vector3>[balls.Length];
        recordedRotations = new List<Quaternion>[balls.Length];
        recordedVelocities = new List<Vector3>[balls.Length];

        for (int i = 0; i < balls.Length; i++)
        {
            recordedPositions[i] = new List<Vector3>();
            recordedRotations[i] = new List<Quaternion>();
            recordedVelocities[i] = new List<Vector3>();
        }
    }

    void Update()
    {
        if (isRecording)
        {
            RecordMovements();
        }
        else if (isReplaying)
        {
            ReplayMovements();
        }
    }

    // Start recording the ball movements
    public void StartRecording()
    {
        isRecording = true;
        isReplaying = false;
        replayTime = 0f;

        // Clear previous recordings
        for (int i = 0; i < balls.Length; i++)
        {
            recordedPositions[i].Clear();
            recordedRotations[i].Clear();
            recordedVelocities[i].Clear();
        }
    }

    // Stop recording the ball movements
    public void StopRecording()
    {
        isRecording = false;
    }

    // Start replaying the recorded movements
    public void Replay()
    {
        if (recordedPositions[0].Count > 0)
        {
            isReplaying = true;
            replayTime = 0f;
        }
    }

    // Record the current ball positions, rotations, and velocities
    private void RecordMovements()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] != null)
            {
                Rigidbody rb = balls[i].GetComponent<Rigidbody>();

                if (rb != null)
                {
                    recordedPositions[i].Add(balls[i].transform.position);
                    recordedRotations[i].Add(balls[i].transform.rotation);
                    recordedVelocities[i].Add(rb.velocity);
                }
            }
        }
    }

    // Replay the recorded movements
    private void ReplayMovements()
    {
        replayTime += Time.deltaTime * replaySpeed;

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] != null && recordedPositions[i].Count > 0)
            {
                int frame = Mathf.FloorToInt(replayTime);

                if (frame < recordedPositions[i].Count)
                {
                    balls[i].transform.position = recordedPositions[i][frame];
                    balls[i].transform.rotation = recordedRotations[i][frame];

                    Rigidbody rb = balls[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = recordedVelocities[i][frame];
                    }
                }
                else
                {
                    // Stop replaying when the last frame is reached
                    isReplaying = false;
                }
            }
        }
    }
}
