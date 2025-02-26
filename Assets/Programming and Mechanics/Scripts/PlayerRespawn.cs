using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private Vector3 originalSpawn;

    private void Start()
    {
        originalSpawn = transform.position; // Store the original spawn position
        lastCheckpoint = originalSpawn; // Default checkpoint is the original spawn
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        Debug.Log($"Checkpoint updated to: {checkpointPosition}");
    }

    public void Respawn(bool resetToOriginal = false)
    {
        if (resetToOriginal)
        {
            Debug.Log("Respawning at original spawn point...");
            transform.position = originalSpawn; // Reset to the original spawn point
            lastCheckpoint = originalSpawn; // Reset checkpoint as well
        }
        else
        {
            Debug.Log("Respawning at last checkpoint...");
            transform.position = lastCheckpoint; // Respawn at last checkpoint
        }

        // Reset all vines in the scene
        CreepingVines[] vines = FindObjectsByType<CreepingVines>(FindObjectsSortMode.None);
        foreach (CreepingVines vine in vines)
        {
            vine.ResetVines();
        }
    }
}
