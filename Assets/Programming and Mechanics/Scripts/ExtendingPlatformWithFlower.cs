using UnityEngine;

public class ExtendingPlatformWithFlower : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public Transform flower;  // Reference to the flower
    public float extendSpeed = 2f;  // Speed of platform extension
    public float maxLength = 20f;  // Maximum platform length
    public float flowerMoveSpeed = 1f;  // Speed at which the flower moves toward the player
    public float flowerMoveDuration = 8f;  // Time before the flower stops moving

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private float elapsedTime = 0f;
    private bool isMovingFlower = false;
    private bool isExtending = false;

    void Start()
    {
        originalScale = transform.localScale;  // Store the original platform size
        originalPosition = transform.position; // Store the original position
    }

    void Update()
    {
        // If the platform should extend, extend in only one direction
        if (isExtending && transform.localScale.z < maxLength)
        {
            float newScaleZ = Mathf.Min(transform.localScale.z + extendSpeed * Time.deltaTime, maxLength);
            float scaleDifference = newScaleZ - transform.localScale.z; // Calculate how much it grows
            transform.localScale = new Vector3(originalScale.x, originalScale.y, newScaleZ);

            // Shift the platform forward so it extends only in one direction
            transform.position += new Vector3(0, 0, scaleDifference / 2);
        }

        // Move the flower backward for a limited time
        if (isMovingFlower)
        {
            flower.position -= new Vector3(0, 0, flowerMoveSpeed * Time.deltaTime); // Moves toward the player
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= flowerMoveDuration)
            {
                isMovingFlower = false; // Stop moving the flower after 8 seconds
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player is the one triggering it
        {
            isExtending = true;  // Start extending when the player steps onto the platform
            isMovingFlower = true;  // Start moving the flower at the same time
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isExtending = false; // Stop extending if the player leaves the platform
        }
    }
}
