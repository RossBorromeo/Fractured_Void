using UnityEngine;

public class CreepingVines : MonoBehaviour
{
    public float damageAmount = 20f; // Damage the player takes upon touching the vines
    private Vector3 initialPosition; // Store the original position for reset
    public float riseSpeed = 1f; // Speed at which the vines rise
    private bool isActive = false; // Control whether the vines should climb

    private void Start()
    {
        initialPosition = transform.position; // Save initial position for reset
        gameObject.SetActive(false); // Start inactive (hidden) until triggered
    }

    private void Update()
    {
        if (isActive)
        {
            // Move the vines upwards over time
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerHealth != null)
            {
                playerHealth.Damage(damageAmount); // Player takes damage
            }

            if (playerRespawn != null)
            {
                Debug.Log("[Vines] Player touched vines, taking damage and resetting to last checkpoint...");
                playerRespawn.Respawn(false); // Respawn the player at the last checkpoint
            }
        }
    }

    public void ActivateVines()
    {
        isActive = true;
        gameObject.SetActive(true); // Show the vines
    }

    public void ResetVines()
    {
        transform.position = initialPosition; // Reset vines to their starting position
        isActive = false;
        gameObject.SetActive(false); // Hide the vines again
    }
}
