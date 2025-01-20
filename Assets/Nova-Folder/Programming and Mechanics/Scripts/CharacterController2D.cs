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

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1f; // Range of the attack
    [SerializeField] private float attackDamage = 20f; // Damage dealt by the attack
    [SerializeField] private LayerMask attackableLayers; // Layers that can be damaged

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool isGrounded;
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
        isGrounded = CheckGrounded();
        if (!wasGrounded && isGrounded)
        {
            OnLandEvent.Invoke();
        }
        wasGrounded = isGrounded;

        ApplyFasterFalling();
    }

    private bool CheckGrounded()
    {
        if (capsuleCollider == null) return false;

        Vector3 bottomPoint = new Vector3(
            capsuleCollider.bounds.center.x,
            capsuleCollider.bounds.min.y,
            capsuleCollider.bounds.center.z
        );

        return Physics.CheckSphere(bottomPoint, groundCheckRadius, whatIsGround);
    }

    public void Move(float moveX, float moveZ, bool jump)
    {
        Vector3 velocity = new Vector3(moveX * movementSpeed, rb.velocity.y, moveZ * zMovementSpeed);
        rb.velocity = velocity;

        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void ApplyFasterFalling()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Attack()
    {
        // Check for enemies within the attack range
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, attackRange, attackableLayers);
        foreach (Collider hit in hitObjects)
        {
            Health targetHealth = hit.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.Damage(attackDamage);
                Debug.Log($"Hit {hit.name} for {attackDamage} damage.");
            }
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

        // Visualize attack range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
