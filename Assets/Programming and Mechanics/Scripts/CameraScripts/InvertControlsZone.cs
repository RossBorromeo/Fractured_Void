using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InvertControlsZone : MonoBehaviour
{
    [SerializeField] private float controlDelay = 0.5f; // Delay before inverting controls
    private PlayerMovement playerMovement;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                Debug.Log("[InvertControlsZone] Stopping player movement before inversion!");
                playerMovement.SetMovementEnabled(false); // Stop player movement

                isPlayerInside = true;
                StartCoroutine(ApplyInvertedControlsAfterDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is SphereCollider)
        {
            if (playerMovement != null)
            {
                Debug.Log("[InvertControlsZone] Restoring normal controls!");
                playerMovement.SetInvertedControls(false);
                playerMovement.SetMovementEnabled(true); // Allow movement again
                isPlayerInside = false;
            }
        }
    }

    private IEnumerator ApplyInvertedControlsAfterDelay()
    {
        yield return new WaitForSeconds(controlDelay); // Wait before inverting controls

        if (playerMovement != null && isPlayerInside)
        {
            Debug.Log("[InvertControlsZone] Inverting Controls!");
            playerMovement.SetInvertedControls(true);
            playerMovement.SetMovementEnabled(true); // Re-enable movement after inversion
        }
    }
}
