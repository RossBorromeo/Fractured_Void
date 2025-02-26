using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CameraTriggerRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 defaultRotation = new Vector3(0, 0, 0);
    [SerializeField] private float rotationDelay = 2.5f; // Time to disable controls
    private bool isRotated = false;

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
            Debug.Log("[CameraTriggerRotation] Player entered side profile zone!");
            StartCoroutine(RotateWithDelay(targetRotation, true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CameraTriggerRotation] Player exited side profile zone!");
            StartCoroutine(RotateWithDelay(defaultRotation, false));
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
            playerMovement.SetMovementEnabled(false); // Disable movement and reset speed
        }

        yield return new WaitForSeconds(rotationDelay); // Wait for 1.5 seconds

        playerTransform.rotation = Quaternion.Euler(rotation);
        isRotated = rotated;

        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true); // Re-enable movement
            playerMovement.SetRotationState(isRotated);
        }
    }
}
