using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Damage(20); // Apply damage when falling

                if (!playerHealth.IsDead())
                {
                    other.GetComponent<PlayerRespawn>().Respawn();
                }
            }
        }
    }
}
