using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraTriggerRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rotationDelay = 0.5f; // Small delay for better transitions
    private static int sideProfileZoneCount = 0; // Track how many zones player is in

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("[CameraTriggerRotation] PlayerMovement script not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            sideProfileZoneCount++; // Increase count when entering a zone

            if (sideProfileZoneCount == 1) // Only rotate on first entry
            {
                Debug.Log("[CameraTriggerRotation] Switching to Side Profile View!");
                StartCoroutine(RotateWithDelay(new Vector3(0, 90, 0), true));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            sideProfileZoneCount--; // Decrease count when leaving a zone

            if (sideProfileZoneCount == 0) // Only reset rotation if no zones remain
            {
                Debug.Log("[CameraTriggerRotation] Returning to Normal View!");
                StartCoroutine(RotateWithDelay(new Vector3(0, 0, 0), false));
            }
        }
    }

    private IEnumerator RotateWithDelay(Vector3 rotation, bool rotated)
    {
        if (playerTransform == null)
        {
            Debug.LogError("[CameraTriggerRotation] PlayerTransform not assigned!");
            yield break;
        }

        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(false); // Temporarily disable movement
        }

        yield return new WaitForSeconds(rotationDelay); // Small delay for smooth transition

        playerTransform.rotation = Quaternion.Euler(rotation);

        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true); // Re-enable movement
            playerMovement.SetRotationState(rotated);
        }
    }

    // **PUBLIC STATIC METHOD TO FORCE SIDE PROFILE AFTER TELEPORT**
    public static void ForceSideProfile(Transform playerTransform)
    {
        playerTransform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // **Public method to check if side profile is active (for portals)**
    public static bool IsSideProfileActive()
    {
        return sideProfileZoneCount > 0;
    }
}
