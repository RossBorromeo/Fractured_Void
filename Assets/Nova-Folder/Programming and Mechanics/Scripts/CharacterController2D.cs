// CharacterController2D
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 400f; // Jump force
    [SerializeField] private float movementSpeed = 10f; // Movement speed
    [SerializeField] private float zMovementSpeed = 10f; // Speed for Z-axis movement
    [SerializeField] private LayerMask whatIsGround; // Define ground layers

    [Header("Physics Settings")]
    [SerializeField] private float fallMultiplier = 2.5f; // Multiplier to make the character fall faster

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider; // Reference to CapsuleCollider
    private bool isGrounded; // Is the character grounded
    private bool wasGrounded; // Track previous grounded state

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
        CheckGrounded(); // Check if the character is on the ground
        ApplyFasterFalling(); // Apply custom falling force to speed up fall
    }

    private void CheckGrounded()
    {
        if (capsuleCollider == null) return;

        // Define ground check point (bottom of CapsuleCollider)
        Vector3 bottomPoint = new Vector3(
            capsuleCollider.bounds.center.x,
            capsuleCollider.bounds.min.y,
            capsuleCollider.bounds.center.z
        );

        // Check if the character is grounded using Physics.CheckSphere
        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, whatIsGround);

        // Trigger OnLandEvent only if landing (transition from not grounded to grounded)
        if (!wasGrounded && isGrounded)
        {
            OnLandEvent.Invoke();
        }

        wasGrounded = isGrounded;
    }

    public void Move(float moveX, float moveZ, bool jump)
    {
        // Horizontal movement (x-axis and z-axis)
        Vector3 velocity = new Vector3(moveX * movementSpeed, rb.velocity.y, moveZ * zMovementSpeed);
        rb.velocity = velocity;

        // Jumping logic (vertical movement)
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void ApplyFasterFalling()
    {
        // If the character is falling (not moving up), apply custom gravity to speed up falling
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
            Gizmos.DrawWireSphere(bottomPoint, 0.1f); // Visualize ground check
        }
    }
}
