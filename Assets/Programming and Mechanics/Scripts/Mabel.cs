using UnityEngine;

public class Mabel : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to Mabel
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("[Mabel] Animator component is missing!");
        }
    }

    // Trigger the Move animation
    public void TriggerMoveAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
        }
    }

    // Trigger the Idle animation
    public void TriggerIdleAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
