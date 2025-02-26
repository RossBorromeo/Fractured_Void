using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Mabel mabel;

    [Header("Movement Settings")]
    public float walkSpeed = 40f;
    public float runMultiplier = 1.5f;
    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool facingLeft = true;
    private bool isRotated = false;
    private bool canMove = true;

    [Header("Health Settings")]
    public Health health;
    private bool isDead = false;

    void Start()
    {
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
        if (isDead || !canMove) return;

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * runMultiplier : walkSpeed;
        float horizontalMove = Input.GetAxisRaw("Horizontal") * speed; // A/D
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;     // W/S

        if (isRotated)
        {
            // Reverse W/S movement in side profile mode
            moveDirection = new Vector2(-verticalMove, horizontalMove);
        }
        else
        {
            moveDirection = new Vector2(horizontalMove, verticalMove);
        }

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsGrounded", controller.isGrounded);

        if (moveDirection.magnitude > 0.01f)
        {
            mabel?.TriggerMoveAnimation();
        }
        else
        {
            mabel?.TriggerIdleAnimation();
        }

        // Handle flipping based on movement
        HandleFlipping();

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        controller.Move(moveDirection.x * Time.fixedDeltaTime, moveDirection.y * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void SetRotationState(bool rotated)
    {
        isRotated = rotated;
    }

    private void HandleFlipping()
    {
        if (isRotated)
        {
            // In side profile mode, vertical movement (W/S) determines flipping
            if (moveDirection.y < 0 && facingLeft)  // 'Up' is right in side profile
            {
                Flip();
            }
            else if (moveDirection.y > 0 && !facingLeft) // 'Down' is left in side profile
            {
                Flip();
            }
        }
        else
        {
            // Normal mode flipping (A/D movement)
            if (moveDirection.x > 0 && facingLeft)
            {
                Flip();
            }
            else if (moveDirection.x < 0 && !facingLeft)
            {
                Flip();
            }
        }
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

        // Disable movement and interactions
        controller.enabled = false;
        this.enabled = false;

        Debug.Log("Game Over!");
        Application.Quit(); // Quit the game (temporary)
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void SetMovementEnabled(bool isEnabled)
    {
        canMove = isEnabled;

        if (!isEnabled)
        {
            moveDirection = Vector2.zero; // Stop movement instantly
            controller.Move(0, 0, false); // Ensure no leftover movement
            animator.SetFloat("Speed", 0); // Stop walk/run animations
        }
    }
}
