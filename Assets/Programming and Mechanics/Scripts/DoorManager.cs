using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public string requiredKeyID; // Key ID required to open this door
    private bool isNearPlayer = false;
    private Animator animator;
    private Collider solidCollider; // Reference to the solid blocking collider

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
            Debug.Log($"Door {requiredKeyID} opened!");
            animator.SetTrigger("DoorOpen");

            // Disable the solid collider so the player can walk through
            if (solidCollider != null)
            {
                solidCollider.enabled = false;
            }
        }
        else
        {
            Debug.Log($"You need the {requiredKeyID} key to open this door!");
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
