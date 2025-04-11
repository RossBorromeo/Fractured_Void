using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn_Audio playerRespawn = other.GetComponent<PlayerRespawn_Audio>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);
            }
        }
    }
}
