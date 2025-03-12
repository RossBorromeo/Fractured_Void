using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraTriggerRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rotationDelay = 0.5f;
    private static int rotationZoneCount = 0;

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
            rotationZoneCount++; // Track overlapping rotation zones

            if (rotationZoneCount == 1) // Only rotate on first zone entry
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
            rotationZoneCount--; // Decrease count when leaving a zone

            if (rotationZoneCount == 0) // Only reset rotation if no zones remain
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
            playerMovement.SetMovementEnabled(false);
        }

        yield return new WaitForSeconds(rotationDelay);

        playerTransform.rotation = Quaternion.Euler(rotation);

        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true);
            playerMovement.SetRotationState(rotated);
        }
    }

    public static bool IsSideProfileActive()
    {
        return rotationZoneCount > 0;
    }

    public static void ForceSideProfile(Transform playerTransform)
    {
        if (playerTransform == null) return;

        playerTransform.rotation = Quaternion.Euler(0, 90, 0); // Apply side profile rotation

        // Ensure movement restrictions are also applied
        PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetRotationState(true);
        }
    }
}
