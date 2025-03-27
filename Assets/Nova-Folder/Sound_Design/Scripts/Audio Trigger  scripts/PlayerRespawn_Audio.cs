using UnityEngine;

public class PlayerRespawn_Audio : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private Vector3 originalSpawn;
    
    public AudioClip respawnSound;
    private AudioSource audioSource;

    private void Start()
    {
        originalSpawn = transform.position;
        lastCheckpoint = originalSpawn;
        
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
        Debug.Log($"Checkpoint updated to: {checkpointPosition}");
    }

    public void Respawn(bool resetToOriginal = false)
    {
        transform.position = resetToOriginal ? originalSpawn : lastCheckpoint;

        if (resetToOriginal)
        {
            lastCheckpoint = originalSpawn;
            Debug.Log("Respawning at original spawn point...");
        }
        else
        {
            Debug.Log("Respawning at last checkpoint...");
        }

        PlaySound(respawnSound);

        CreepingVines[] vines = FindObjectsByType<CreepingVines>(FindObjectsSortMode.None);
        foreach (CreepingVines vine in vines)
        {
            vine.ResetVines();
        }
    }

    public bool HasCheckpoint()
    {
        return lastCheckpoint != originalSpawn;
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}