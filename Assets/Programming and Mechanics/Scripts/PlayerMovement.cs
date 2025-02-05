using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Mabel mabel; // Reference to the Mabel script on the doll

    [Header("Movement Settings")]
    public float walkSpeed = 40f;
    public float runMultiplier = 1.5f;

    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool facingLeft = true;

    [Header("Health Settings")]
    public Health health;
    private bool isDead = false;
    public AudioSource audioSource; //Adams bit

    void Start()
    {
        if (mabel == null)
        {
            Debug.LogError("[PlayerMovement] Mabel reference is missing!");
        }

        if (health == null)
        {
            health = GetComponent<Health>();
            if (health == null)
            {
                Debug.LogError("[PlayerMovement] Missing Health component!");
            }
        }
    }

    void Update()
    {
        if (isDead) return;

        // Handle running
        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * runMultiplier : walkSpeed;

        // Movement input
        float horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;
        moveDirection = new Vector2(horizontalMove, verticalMove);

        // Update animator parameters
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsGrounded", controller.isGrounded);

        // Control Mabel's animations based on movement
        if (moveDirection.magnitude > 0.01f)
        {
            mabel?.TriggerMoveAnimation();
        }
        else
        {
            mabel?.TriggerIdleAnimation();
        }

        // Flip character sprite if direction changes
        if (horizontalMove > 0 && facingLeft)
        {
            Flip();
        }
        else if (horizontalMove < 0 && !facingLeft)
        {
            Flip();
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
            Debug.Log("Jumping!");
            animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // Pass movement and jump parameters to the controller
        controller.Move(moveDirection.x * Time.fixedDeltaTime, moveDirection.y * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health.Damage(damage);
        Debug.Log($"[Player] Took {damage} damage. Current health: {health.GetCurrentHealth()}");

        if (health.GetCurrentHealth() <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("IsDead", true);
        Debug.Log("[Player] Player has died.");

        // Disable player movement and interactions
        controller.enabled = false;
        this.enabled = false;

        Debug.Log("Game Over!");
        Application.Quit(); // Quit the game (temporary)
    }
}
