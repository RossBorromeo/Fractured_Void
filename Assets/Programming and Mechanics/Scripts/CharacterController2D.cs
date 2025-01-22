using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float zMovementSpeed = 10f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Physics Settings")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float groundCheckRadius = 0.1f;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    public bool isGrounded { get; private set; } // Public getter for isGrounded
    private bool wasGrounded;

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        // Check grounded state and invoke landing event if needed
        isGrounded = CheckGrounded();
        if (!wasGrounded && isGrounded)
        {
            OnLandEvent.Invoke(); // Trigger OnLanding in PlayerMovement
        }
        wasGrounded = isGrounded;

        ApplyFasterFalling();
    }

    private bool CheckGrounded()
    {
        if (capsuleCollider == null) return false;

        Vector3 bottomPoint = new Vector3(
            capsuleCollider.bounds.center.x,
            capsuleCollider.bounds.min.y - 0.1f, // Lower the check position slightly
            capsuleCollider.bounds.center.z
        );

        return Physics.CheckSphere(bottomPoint, groundCheckRadius, whatIsGround);
    }

    public void Move(float moveX, float moveZ, bool jump)
    {
        // Apply movement on the XZ plane
        Vector3 velocity = new Vector3(moveX * movementSpeed, rb.velocity.y, moveZ * zMovementSpeed);
        rb.velocity = velocity;

        // Handle jumping
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent multiple jumps until the player lands
        }
    }

    private void ApplyFasterFalling()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        if (capsuleCollider != null)
        {
            Vector3 bottomPoint = new Vector3(
                capsuleCollider.bounds.center.x,
                capsuleCollider.bounds.min.y,
                capsuleCollider.bounds.center.z
            );

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(bottomPoint, groundCheckRadius);
        }
    }
}
