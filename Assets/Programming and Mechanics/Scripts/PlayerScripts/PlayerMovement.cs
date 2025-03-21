using UnityEngine;
using Unity.VisualScripting;

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
    private bool isVerticalInverted = false;
    private bool isHorizontalInverted = false;

    [Header("Health System")]
    public PlayerHealth playerHealth;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("[PlayerMovement] Missing PlayerHealth component!");
            }
        }
    }

    private void Update()
    {
        if (!canMove) return;

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * runMultiplier : walkSpeed;
        float horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;

        if (isVerticalInverted) verticalMove *= -1;
        if (isHorizontalInverted) horizontalMove *= -1;

        moveDirection = isRotated
            ? new Vector2(-verticalMove, horizontalMove)
            : new Vector2(horizontalMove, verticalMove);

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

        HandleFlipping();

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        controller.Move(moveDirection.x * Time.fixedDeltaTime, moveDirection.y * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void HandleFlipping()
    {
        if (isRotated)
        {
            if (moveDirection.y < 0 && facingLeft || moveDirection.y > 0 && !facingLeft)
            {
                Flip();
            }
        }
        else
        {
            if (moveDirection.x > 0 && facingLeft || moveDirection.x < 0 && !facingLeft)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // NEW: Heart-based damage system
    public void TakeDamage(float damage)
    {
        if (playerHealth == null) return;

        playerHealth.TakeDamage(); // This will handle hearts, respawn, and bedroom transfer
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
            moveDirection = Vector2.zero;
            controller.Move(0, 0, false);
            animator.SetFloat("Speed", 0);
        }
    }

    public void SetInvertedControls(bool state)
    {
        isVerticalInverted = state;
        isHorizontalInverted = state;
    }

    public void SetRotationState(bool rotated)
    {
        isRotated = rotated;
    }
}
