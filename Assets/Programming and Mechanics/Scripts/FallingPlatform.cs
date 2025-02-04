using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasActivated = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        originalPosition = rb.transform.position;
        originalRotation = rb.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player")) // Ensures it triggers only once
        {
            hasActivated = true;
            StartCoroutine(FallAfterDelay(2f)); // Delay before falling
        }
    }

    IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false; // Enables physics so it falls
        rb.useGravity = true;

        yield return new WaitForSeconds(3f); // Wait before respawning

        ResetPlatform(); // Call function to reset the platform
    }

    void ResetPlatform()
    {
        rb.isKinematic = true; // Disable physics to reset safely
        rb.useGravity = false;
        rb.velocity = Vector3.zero; // Reset velocity
        rb.angularVelocity = Vector3.zero;
        rb.transform.position = originalPosition;
        rb.transform.rotation = originalRotation;

        hasActivated = false; // Allow the platform to be triggered again
    }
}
