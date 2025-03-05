using System.Collections;
using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    [Tooltip("Assign the destination portal Transform here.")]
    [SerializeField] private Transform destinationPortal;

    private static Vector3 teleportOffset = new Vector3(1, 1, 2); // Universal offset for all portals
    private static CinemachineVirtualCamera cinemachineCam;

    private Collider portalCollider;

    private void Awake()
    {
        portalCollider = GetComponent<Collider>();

        // Find the Cinemachine Virtual Camera by name (only once)
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
        if (other.CompareTag("Player")) // Ensure only the player triggers teleportation
        {
            if (destinationPortal != null)
            {
                Collider destinationCollider = destinationPortal.GetComponent<Collider>();

                // **TRACK PLAYER ROTATION STATE BEFORE TELEPORTING**
                bool wasSideProfile = CameraTriggerRotation.IsSideProfileActive();

                // Disable colliders temporarily to prevent getting stuck
                if (portalCollider != null) portalCollider.enabled = false;
                if (destinationCollider != null) destinationCollider.enabled = false;

                // **DO NOT DETACH THE CAMERA!** Just move the player.
                Vector3 targetPosition = destinationPortal.position + teleportOffset;
                other.transform.position = targetPosition;

                // Reset velocity to avoid unwanted momentum
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                }

                // **ENSURE CAMERA & ROTATION STATE PERSIST AFTER TELEPORT**
                StartCoroutine(ReenableColliders(portalCollider, destinationCollider));

                if (wasSideProfile)
                {
                    CameraTriggerRotation.ForceSideProfile(other.transform); // Reapply side profile rotation
                }
            }
            else
            {
                Debug.LogWarning($"Destination portal not assigned on {gameObject.name}!");
            }
        }
    }

    private IEnumerator ReenableColliders(Collider portalCol, Collider destinationCol)
    {
        yield return new WaitForSeconds(4.0f); // Small delay to allow the player to move away
        if (portalCol != null) portalCol.enabled = true;
        if (destinationCol != null) destinationCol.enabled = true;
    }
}
