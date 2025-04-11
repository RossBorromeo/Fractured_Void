using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    [Tooltip("Assign the destination portal Transform here.")]
    [SerializeField] private Transform destinationPortal;

    private static CinemachineVirtualCamera cinemachineCam;
    private Collider portalCollider;

    private void Awake()
    {
        portalCollider = GetComponent<Collider>();

        if (cinemachineCam == null)
        {
            GameObject vcamObject = GameObject.Find("Vcam PathToSunflower");
            if (vcamObject != null)
            {
                cinemachineCam = vcamObject.GetComponent<CinemachineVirtualCamera>();
            }

            if (cinemachineCam == null)
            {
                Debug.LogError("Cinemachine Virtual Camera 'Vcam PathToSunflower' not found in the scene!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (destinationPortal == null)
        {
            Debug.LogWarning($"Destination portal not assigned on {gameObject.name}!");
            return;
        }

        // Track player state before teleporting
        bool wasSideProfile = CameraTriggerRotation.IsSideProfileActive();

        // Disable this and destination portal colliders briefly (prevent re-trigger)
        Collider destinationCollider = destinationPortal.GetComponent<Collider>();
        if (portalCollider != null) portalCollider.enabled = false;
        if (destinationCollider != null) destinationCollider.enabled = false;

        // Teleport player
        other.transform.position = destinationPortal.position;

        // Reset velocity to prevent momentum carry
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = Vector3.zero;

        // Immediately re-enable both colliders (no delay)
        if (portalCollider != null) portalCollider.enabled = true;
        if (destinationCollider != null) destinationCollider.enabled = true;

        // Reapply side-profile view if applicable
        if (wasSideProfile)
        {
            CameraTriggerRotation.ForceSideProfile(other.transform);
            Debug.Log("[PortalTeleport] Player was in Side Profile mode before teleporting. Reapplying...");
        }
    }
}
