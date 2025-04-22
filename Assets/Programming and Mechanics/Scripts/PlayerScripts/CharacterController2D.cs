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
    [SerializeField] private float fallMultiplier = 5f; // Increased for faster falling
    [SerializeField] private float lowJumpMultiplier = 3f; // Makes short jumps more responsive
    [SerializeField] private float groundCheckRadius = 0.1f;


    [Header("Step Settings")]
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepCheckDistance = 0.2f;
    [SerializeField] private LayerMask stepLayer;
    private float ledgeStuckTimer = 0f;
    [SerializeField] private float ledgeStuckDuration = 1f;
    [SerializeField] private float ledgeCheckHeight = 0.4f;
    private bool isBlockedForward = false;


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

        TryStepUp();


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
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity before jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent multiple jumps until the player lands
        }
    }

    private void ApplyFasterFalling()
    {
        if (rb.velocity.y < 0)
        {
            // Increase gravity force when falling
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // If player releases jump button early, apply stronger downward force
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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


    private void TryStepUp()
    {
        Vector3 moveDir = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;

        // Debug current position and grounded status
        Debug.Log($"[TryStepUp] Position: {rb.position}, Grounded: {isGrounded}");

        if (moveDir == Vector3.zero)
        {
            ledgeStuckTimer = 0f;
            isBlockedForward = false;
            Debug.Log("[TryStepUp] Not moving - no ledge logic applied.");
            return;
        }

        // Capsule for forward blocking detection
        CapsuleCollider col = capsuleCollider;
        Vector3 point1 = col.bounds.center + Vector3.up * (col.height / 2 - col.radius);
        Vector3 point2 = col.bounds.center + Vector3.down * (col.height / 2 - col.radius);

        bool isBlocked = Physics.CapsuleCast(point1, point2, col.radius, moveDir, out RaycastHit hit, stepCheckDistance, stepLayer);

        if (!isGrounded && isBlocked && rb.velocity.y <= 0.1f)
        {
            ledgeStuckTimer += Time.fixedDeltaTime;
            isBlockedForward = true;

            Debug.Log($"[TryStepUp] Blocked against ledge. Timer: {ledgeStuckTimer:F2}");

            if (ledgeStuckTimer >= ledgeStuckDuration)
            {
                // Apply the unstick nudge
                Vector3 pushDirection = (-moveDir * 0.15f) + Vector3.down * 0.2f;
                rb.MovePosition(rb.position + pushDirection);
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

                Debug.Log("[TryStepUp] Player nudged off ledge!");

                ledgeStuckTimer = 0f;
            }
        }
        else
        {
            if (isBlocked)
                Debug.Log("[TryStepUp] Blocked, but grounded or moving upward – not nudging.");
            else
                Debug.Log("[TryStepUp] Not blocked – reset timer.");

            ledgeStuckTimer = 0f;
            isBlockedForward = false;
        }
    }




}
