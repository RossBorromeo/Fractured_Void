using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform pointA; // First patrol point
    [SerializeField] private Transform pointB; // Second patrol point
    [SerializeField] private float speed = 2f; // Movement speed
    [SerializeField] private float stopTime = 2f; // Time to stop at each patrol point
    private Transform currentTarget; // Current destination point
    private bool isMoving = true; // Is the enemy currently moving?

    [Header("Physics Settings")]
    [SerializeField] private Rigidbody rb; // Reference to Rigidbody
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private LayerMask groundLayer; // Layer for ground detection
    private bool isGrounded; // Is the enemy on the ground?

    [Header("Animation Settings")]
    [SerializeField] private Animator animator; // Reference to Animator component

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 fixedRotation = Vector3.forward; // Default facing direction

    private void Start()
    {
        // Ensure Rigidbody is assigned
        rb = GetComponent<Rigidbody>();

        // Set the initial target to pointA
        currentTarget = pointA;

        // Freeze unnecessary Rigidbody rotations
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Ensure Animator is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        if (isGrounded && isMoving)
        {
            MoveTowardsTarget();
        }

        UpdateAnimator();
    }

    private void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        // Calculate direction to the current target
        Vector3 direction = (currentTarget.position - transform.position);
        direction.y = 0; // Ignore Y-axis to prevent tilting

        // Check if the enemy is close to the target
        if (direction.magnitude < 0.5f)
        {
            rb.velocity = Vector3.zero; // Stop the enemy completely

            if (currentTarget == pointB)
            {
                Debug.Log("Fully stopped at point B.");
                transform.rotation = Quaternion.LookRotation(fixedRotation); // Face the specified direction
            }

            StartCoroutine(SwitchTargetWithDelay());
            return;
        }

        // Normalize direction for consistent movement
        direction.Normalize();

        // Apply velocity to the Rigidbody
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
    }

    private System.Collections.IEnumerator SwitchTargetWithDelay()
    {
        isMoving = false; // Stop movement during the delay
        Debug.Log($"Stopping at {currentTarget.name} for {stopTime} seconds.");
        yield return new WaitForSeconds(stopTime); // Wait at the patrol point

        // Switch to the next target
        currentTarget = currentTarget == pointA ? pointB : pointA;
        Debug.Log($"Switching target to: {currentTarget.name}");
        isMoving = true; // Resume movement
    }

    private void UpdateAnimator()
    {
        // Calculate current movement speed (magnitude of velocity)
        float currentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        // Clamp small values to zero for clean transitions
        if (currentSpeed < 0.01f)
        {
            currentSpeed = 0f;
        }

        // Update the Speed parameter in the Animator
        animator.SetFloat("Speed", currentSpeed);
    }

    private void CheckGrounded()
    {
        // Use Physics.CheckSphere to detect ground
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            Vector3 groundCheckPosition = new Vector3(
                capsuleCollider.bounds.center.x,
                capsuleCollider.bounds.min.y - 0.1f,
                capsuleCollider.bounds.center.z
            );
            isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayer);
        }
        else
        {
            Debug.LogWarning("CapsuleCollider not found on the enemy!");
            isGrounded = true; // Assume grounded if no collider
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw lines between the enemy and its patrol points
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }

        // Draw ground check sphere
        Gizmos.color = Color.blue;
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            Vector3 groundCheckPosition = new Vector3(
                capsuleCollider.bounds.center.x,
                capsuleCollider.bounds.min.y - 0.1f,
                capsuleCollider.bounds.center.z
            );
            Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
        }
    }
}
