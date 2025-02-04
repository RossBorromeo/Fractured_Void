using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public string requiredKeyID; // Key ID required to open this door
    private bool isNearPlayer = false;
    private Animator animator;
    private Collider solidCollider; // Reference to the blocking collider

    void Start()
    {
        animator = GetComponent<Animator>();
        solidCollider = GetComponent<BoxCollider>(); // Get the blocking collider
    }

    void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        if (PlayerInventory.Instance.HasKey(requiredKeyID))
        {
            Debug.Log($"Door {requiredKeyID} opening...");
            animator.SetTrigger("DoorOpen");

            // Disable the solid collider so the player can pass
            if (solidCollider != null)
            {
                solidCollider.enabled = false;
            }

            // Transition to Door1Stopped after the rising animation completes
            Invoke(nameof(SetDoorIdle), 2f); // Adjust timing based on animation length
        }
        else
        {
            Debug.Log($"You need the {requiredKeyID} key to open this door!");
        }
    }

    private void SetDoorIdle()
    {
        Debug.Log($"Door {requiredKeyID} has reached idle position.");
        animator.SetTrigger("DoorStopped");

        if (solidCollider != null)
        {
            solidCollider.enabled = false; // Ensure collider is fully disabled
        }

        // Just in case, disable all colliders on this object
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
            Debug.Log($"Player is near {requiredKeyID}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            Debug.Log($"Player left {requiredKeyID}");
        }
    }
}
