using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);
            }
        }
    }
}
