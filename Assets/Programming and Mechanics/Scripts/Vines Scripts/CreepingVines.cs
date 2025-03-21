using UnityEngine;

public class CreepingVines : MonoBehaviour
{
    public float riseSpeed = 1f; // Speed at which the vines rise
    private Vector3 initialPosition; // Store the original position for reset
    private bool isActive = false; // Control whether the vines should climb

    private void Start()
    {
        initialPosition = transform.position;
        gameObject.SetActive(false); // Start hidden
    }

    private void Update()
    {
        if (isActive)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("[Vines] Player touched vines, taking damage...");
                playerHealth.TakeDamage(); // Handles hearts and scene change if needed
            }
        }
    }

    public void ActivateVines()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void ResetVines()
    {
        transform.position = initialPosition;
        isActive = false;
        gameObject.SetActive(false);
    }
}
