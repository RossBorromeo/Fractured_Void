using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CameraTriggerRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 defaultRotation = new Vector3(0, 0, 0);
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
            RotatePlayer(targetRotation, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            Debug.Log("[CameraTriggerRotation] Player exited side profile zone!");
            RotatePlayer(defaultRotation, false);
        }
    }

    private void RotatePlayer(Vector3 rotation, bool rotated)
    {
        if (playerTransform == null)
        {
            Debug.LogError("[CameraTriggerRotation] PlayerTransform not assigned!");
            return;
        }

        playerTransform.rotation = Quaternion.Euler(rotation);
        isRotated = rotated;

        if (playerMovement != null)
        {
            playerMovement.SetRotationState(isRotated);
        }
    }
}
