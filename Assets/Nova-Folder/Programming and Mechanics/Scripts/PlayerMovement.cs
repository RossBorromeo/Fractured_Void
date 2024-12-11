using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller; // Reference to the custom CharacterController2D
    public Animator animator;
    public float walkSpeed = 40f;
    public float runMultiplier = 1.5f; // Multiplier for running speed

    private Vector2 moveDirection = Vector2.zero; // Stores movement direction
    private bool jump = false;
    private bool facingRight = true; // Tracks the current facing direction

    void Update()
    {
        // Handle movement input
        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= runMultiplier; // Apply running speed multiplier
        }

        float horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;

        moveDirection = new Vector2(horizontalMove, verticalMove);

        // Update animator parameters
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalMove));
        animator.SetFloat("Speed", moveDirection.magnitude);

        // Flip character sprite if direction changes
        if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }

        // Handle jump input
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true); // Trigger jump animation
        }
    }

    private void FixedUpdate()
    {
        // Pass movement and jump parameters to the controller
        controller.Move(moveDirection.x * Time.fixedDeltaTime, moveDirection.y * Time.fixedDeltaTime, jump);
        jump = false; // Reset jump state after applying
    }

    public void OnLanding()
    {
        // Reset jumping animation upon landing
        animator.SetBool("IsJumping", false);
    }

    private void Flip()
    {
        // Reverse the facing direction
        facingRight = !facingRight;

        // Flip the character's local scale on the X-axis
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}