using UnityEngine;

public class Locomotion : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    [Header("Locomotion Settings")]
    public float walkSpeed = 40f;
    public float runMultiplier = 1.5f;

    //     --Adam Audio Input
    public AudioSource audioSource; // Source of sound


    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool facingLeft = true;

    void Update()
    {
        // Handle running
        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * runMultiplier : walkSpeed;

        // Movement input
        float horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed;

        moveDirection = new Vector2(horizontalMove, verticalMove);

        // Update animator parameters
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsGrounded", controller.isGrounded);

        // Flip character sprite if direction changes
        if (horizontalMove > 0 && facingLeft)
        {
            Flipping(); // Flip to face right
        }
        else if (horizontalMove < 0 && !facingLeft)
        {
            Flipping(); // Flip to face left
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true); // Set IsJumping to true when jumping



        }
    }

    private void FixedUpdate()
    {
        // Pass movement and jump parameters to the controller
        controller.Move(moveDirection.x * Time.fixedDeltaTime, moveDirection.y * Time.fixedDeltaTime, jump);

        jump = false; // Reset jump state after applying
    }

    public void Land()
    {
        // Reset IsJumping animation upon landing
        animator.SetBool("IsJumping", false);
    }

    private void Flipping()
    {
        // Reverse the facing direction
        facingLeft = !facingLeft;

        // Flip the character's local scale on the X-axis
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
