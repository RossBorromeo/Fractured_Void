using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnUI : MonoBehaviour
{
    private Vector3 lastCheckpointUI;
    private Vector3 originalSpawnUI;
    private bool hasCheckpoint = false;// boolean to see if checkpoint exists
    private void Start()
    {
        originalSpawnUI = transform.position; // Store the original spawn position
        lastCheckpointUI = originalSpawnUI; // Default checkpoint is the original spawn
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointUI = checkpointPosition;
        hasCheckpoint = true;
        Debug.Log($"Checkpoint updated to: {checkpointPosition}");
    }

    public void Respawn(bool resetToOriginal = false)
    {
        if (resetToOriginal)
        {
            Debug.Log("Respawning at original spawn point...");
            transform.position = originalSpawnUI; // Reset to the original spawn point
            lastCheckpointUI = originalSpawnUI; // Reset checkpoint as well
        }
        else
        {
            Debug.Log("Respawning at last checkpoint...");
            transform.position = lastCheckpointUI; // Respawn at last checkpoint
        }
    }

    public bool HasCheckpoint()
    {
        return hasCheckpoint;

    }
}
